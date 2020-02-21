using Dummiesman;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[SerializeField]
public class ShareJson
{
    [SerializeField]
    public string headMeshObj;//raw obj string , parse is directly use obj file parser
    [SerializeField]
    public string headTextureJpg;//base64 string ,convert to byte array,is a jpg file

    [SerializeField]
    public string bodyMeshObj;//raw obj string , parse is directly use obj file parser
    [SerializeField]
    public string bodyTextureJpg;//base64 string ,convert to byte array,is a jpg file
    [SerializeField]
    public List<string> avatorMeshObjList; //raw obj string , parse is directly use obj file parser 
    [SerializeField]
    public List<string> avatorMeshTextureList; //base64 string ,convert to byte array,is a jpg file
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
    
    public string ToJson(Mesh headMesh,Texture2D headTexture,Mesh bodyMesh,Texture2D bodyTexture,List<Mesh> avatarMeshes,List<Texture2D> avatarTextures)
    {
        ShareJson shareJson = new ShareJson();
        if (headMesh != null && headTexture != null)
        {
            shareJson.headMeshObj = RuntimeObjExporter.MeshToString("headMesh", headMesh, null);

            byte[] headTextureJpgData = headTexture.EncodeToJPG();
            shareJson.headTextureJpg = Convert.ToBase64String(headTextureJpgData);

        }
        
        if (bodyMesh != null && bodyTexture != null)
        {
            shareJson.bodyMeshObj = RuntimeObjExporter.MeshToString("bodyMesh", bodyMesh, null);

            byte[] bodyTextureJpgData = bodyTexture.EncodeToJPG();
            shareJson.bodyTextureJpg = Convert.ToBase64String(bodyTextureJpgData);
        }

        if (avatarMeshes != null && avatarTextures != null && avatarMeshes.Count == avatarTextures.Count)
        {
            shareJson.avatorMeshObjList = new List<string>();
            shareJson.avatorMeshTextureList = new List<string>();
            for (int i = 0; i < avatarMeshes.Count;i++)
            {
                shareJson.avatorMeshObjList.Add(RuntimeObjExporter.MeshToString(string.Format("avatar_{0}",i), avatarMeshes[i], null));

                byte[] textureJpgData = avatarTextures[i].EncodeToJPG();
                shareJson.avatorMeshTextureList.Add(Convert.ToBase64String(textureJpgData));

            }
        }

        
        string json = JsonUtility.ToJson(shareJson);
        return json;
    }

    
    IEnumerator Upload()
    {
        Mesh headMesh;
        Texture2D headTexture;
        Mesh bodyMesh;
        Texture2D bodyTexture;
        List<Mesh> avatarMeshes;
        List<Texture2D> avatarTextures;

        ModelDataManager.Instance.BakeSkinnedMesh(out headMesh, out headTexture, out bodyMesh, out bodyTexture, out avatarMeshes, out avatarTextures);
        string jsonData = ShareManager.Instance.ToJson(headMesh, headTexture, bodyMesh, bodyTexture, avatarMeshes, avatarTextures);



        string serverURL = "https://m.yujishishi.com/fac/com/upCommImg";


        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection(string.Format("type={0}",99)));
        formData.Add(new MultipartFormFileSection(jsonData, "file"));
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
            string shareAddress = request.GetResponseHeader("address");
            Debug.Log(shareAddress);
        }
    }

    void ReadShareTest(string inputJson)
    {
        ShareJson shareJson = JsonUtility.FromJson<ShareJson>(inputJson);


        Stream stream = new MemoryStream(Convert.FromBase64String(shareJson.headMeshObj));
        GameObject deformedMeshObject = new OBJLoader().Load(stream);


        MeshFilter defromedMeshFilter = deformedMeshObject.GetComponentInChildren<MeshFilter>();
        Mesh loadMesh = Instantiate(defromedMeshFilter.sharedMesh);

        Debug.Log("Load Mesh Json:" + loadMesh.vertices.Length);
    }


}
