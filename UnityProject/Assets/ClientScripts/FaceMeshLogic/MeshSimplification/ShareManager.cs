﻿using Dummiesman;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


[Serializable]
public class ShareTextureItem
{
    [SerializeField]
    public string name;    //name of the texture
    [SerializeField]
    public string textureJpg;//base64 string ,convert to byte array,is a jpg file

}

[Serializable]
public class ShareMeshItem
{
    public string name; //name of the mesh object
    [SerializeField]
    public string meshObj;//raw obj string , parse is directly use obj file parser
    [SerializeField]
    public List<ShareTextureItem> textures = new List<ShareTextureItem>();

}
[Serializable]
public class ShareJson
{
    [SerializeField]
    public List<ShareMeshItem> meshes = new List<ShareMeshItem>();
}
public static class TextureExt
{
    public static Texture2D ToTexture2D(this Texture self)
    {
        if (self != null)
        {
            var sw = self.width;
            var sh = self.height;
            var format = TextureFormat.RGBA32;
            var result = new Texture2D(sw, sh, format, false);
            var currentRT = RenderTexture.active;
            var rt = new RenderTexture(sw, sh, 32);
            Graphics.Blit(self, rt);
            RenderTexture.active = rt;
            var source = new Rect(0, 0, rt.width, rt.height);
            result.ReadPixels(source, 0, 0);
            result.Apply();
            RenderTexture.active = currentRT;
            return result;
        }
        else
        {
            return null;
        }
    }
}
public class ShareManager : MonoBehaviour
{
    public static ShareManager Instance;

    public Button mShareBtn;

    private void Awake()
    {
        Instance = this;

        mShareBtn.onClick.AddListener(OnShareBtn);
    }
    private void OnDestroy()
    {
        mShareBtn.onClick.RemoveListener(OnShareBtn);

    }
    void OnShareBtn()
    {
        StartCoroutine(Upload());
    }



    void BakeOneMesh(SkinnedMeshRenderer smr, out Mesh mesh, out Material[] mats, out Texture2D[] textures)
    {

        mesh = new Mesh();
        smr.BakeMesh(mesh);

        Vector3[] Vertices = new Vector3[mesh.vertices.Length];
        for (int i = 0; i < Vertices.Length; i++)
        {
            Vector3 wpos = smr.transform.localToWorldMatrix * mesh.vertices[i];
            Vertices[i] = wpos;
        }
        mesh.vertices = Vertices;

        mats = smr.materials;

        textures = new Texture2D[mats.Length];
        for (int i = 0; i < textures.Length; i++)
        {

            Texture rawTexture = mats[i].GetTexture("_MainTex");
            textures[i] = rawTexture.ToTexture2D();
        }
    }

    void AddMeshObject(SkinnedMeshRenderer smr,ref ShareJson jsonObject)
    {
        Mesh mesh;
        Material[] materials;
        Texture2D[] textures;

        BakeOneMesh(smr, out mesh, out materials, out textures);


        ShareMeshItem item = new ShareMeshItem();

        item.name = smr.name;
        item.meshObj = RuntimeObjExporter.MeshToString(name, mesh, materials);

        if (materials.Length == textures.Length)
        {
            for (int i = 0; i < textures.Length; i++)
            {
                ShareTextureItem texItem = new ShareTextureItem();
                texItem.name = materials[i].name;
                if(textures[i] != null)
                {
                    byte[] textureJpgData = textures[i].EncodeToJPG();
                    texItem.textureJpg = Convert.ToBase64String(textureJpgData);

                }

                item.textures.Add(texItem);
            }
        }
        jsonObject.meshes.Add(item);
    }
    
    public string ToJson(List<SkinnedMeshRenderer> list)
    {
        ShareJson shareJson = new ShareJson();

        foreach(var smr in list)
        {
            AddMeshObject(smr, ref shareJson);
        }
        
        string json = JsonUtility.ToJson(shareJson);
        return json;
    }

    
    IEnumerator Upload()
    {

        string jsonData = ShareManager.Instance.ToJson(ModelDataManager.Instance.GetAllSkinnedMeshRenderer());

        //File.WriteAllText("d:/t.obj", jsonData);

        string serverURL = "https://m.yujishishi.com/fac/com/upCommImg";


        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("type","99"));
        //formData.Add(new MultipartFormFileSection("file", Encoding.ASCII.GetBytes(jsonData)));

        string boundary = "----" + DateTime.Now.Ticks.ToString("x");
        string contentType = string.Format("multipart/form-data; boundary={0}", boundary);
        formData.Add(new MultipartFormFileSection("file", Encoding.ASCII.GetBytes(jsonData), "file",contentType));

        UnityWebRequest request = UnityWebRequest.Post(serverURL, formData);

        yield return request.SendWebRequest();
        while (!request.isDone)
        {

            Debug.Log(request.uploadProgress);

         
        }


        if (request.isHttpError || request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            string shareAddress = request.downloadHandler.text;
            Debug.Log(shareAddress);
        }
    }

    void ReadShareTest(string inputJson)
    {
        ShareJson shareJson = JsonUtility.FromJson<ShareJson>(inputJson);


        Stream stream = new MemoryStream(Convert.FromBase64String(shareJson.meshes[0].meshObj));
        GameObject deformedMeshObject = new OBJLoader().Load(stream);


        MeshFilter defromedMeshFilter = deformedMeshObject.GetComponentInChildren<MeshFilter>();
        Mesh loadMesh = Instantiate(defromedMeshFilter.sharedMesh);

        Debug.Log("Load Mesh Json:" + loadMesh.vertices.Length);
    }


}
