using Dummiesman;
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
    public string textureJpgRGB;//base64 string ,convert to byte array,is a jpg file

    [SerializeField]
    public string textureJpgAlpha;//base64 string ,convert to byte array,is a jpg 1 channel file

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
[Serializable]
public class JsonRet
{
    [Serializable]
    public class JsonRetImage
    {
        public int id;
        public int coid;
        public int type;
        public int rid;
        public int seqno;
        public string url;
        public string fileName;
        public string param;
        public string param1;
        public string lastUrlTm;
        public bool allowOne;
    }
    public int ret;
    public JsonRetImage img;
    public string retMsg;
    public List<int> info;

    
}

public class ShareManager : MonoBehaviour
{
    public static ShareManager Instance;

    public Button mShareBtn;

    public RawImage m_QRCodeImage;
    public RawImage m_HumanImage;
    public Vector2Int mQRSize;

    public Camera mMainCamera;
    public RenderTextureSaver mShareCameraSaver;

    QRCodeTool mQRCodeTool;
    
    public Button mCloseFinalWindowBtn;


    private void Awake()
    {
        Instance = this;

        mShareBtn.onClick.AddListener(OnShareBtn);
        mQRCodeTool = new QRCodeTool();

        mShareCameraSaver.gameObject.SetActive(false);

        mCloseFinalWindowBtn.onClick.AddListener(OnCloseFinalWindow);
    }
    private void OnDestroy()
    {
        mShareBtn.onClick.RemoveListener(OnShareBtn);
        mCloseFinalWindowBtn.onClick.RemoveListener(OnCloseFinalWindow);

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

    private void TransferAlpha(Texture2D tex, /*out Texture2D rgb,*/out Texture2D alpha)
    {
        //rgb = new Texture2D(tex.width, tex.height, TextureFormat.RGB24, false);
        alpha = new Texture2D(tex.width, tex.height, TextureFormat.RGB24, false);
        
        for (int i = 0; i < tex.width; i++)
        {
            for (int j = 0; j < tex.height; j++)
            {
                // Read out pixel value at that location in both textures
                Color pixel = tex.GetPixel(i, j);
                //Color rgbpixel = rgb.GetPixel(i, j);

                //rgbpixel.r = pixel.a;
                //rgbpixel.g = pixel.a;
                //rgbpixel.b = pixel.a;
                //rgb.SetPixel(i, j, rgbpixel);


                Color alphapixel = alpha.GetPixel(i, j);
                alphapixel.r = pixel.a;
                alphapixel.g = pixel.a;
                alphapixel.b = pixel.a;
                alphapixel.a = pixel.a;
                alpha.SetPixel(i, j, alphapixel);

            }
        }
        //rgb.Apply();
        alpha.Apply();
    }

    void AddMeshObject(SkinnedMeshRenderer smr,ref ShareJson jsonObject)
    {
        Mesh mesh;
        Material[] materials;
        Texture2D[] textures;

        BakeOneMesh(smr, out mesh, out materials, out textures);


        ShareMeshItem item = new ShareMeshItem();

        item.name = smr.name;
        item.meshObj = RuntimeObjExporter.MeshToString(name, mesh, null);

        if (materials.Length == textures.Length)
        {
            for (int i = 0; i < textures.Length; i++)
            {
                ShareTextureItem texItem = new ShareTextureItem();
                texItem.name = materials[i].name;
                if(textures[i] != null)
                {
                    if(textures[i].format == TextureFormat.RGB24)
                    {
                        texItem.textureJpgRGB = Convert.ToBase64String(textures[i].EncodeToJPG());

                        Debug.Log(string.Format("RGB24: {0} {1} ", smr.name, texItem.textureJpgRGB.Length));
                    }
                    else if (textures[i].format == TextureFormat.RGBA32)
                    {
                        Texture2D rgb, alpha;
                        TransferAlpha(textures[i],/* out rgb,*/ out alpha);
                        
                        texItem.textureJpgRGB = Convert.ToBase64String(textures[i].EncodeToJPG());
                        texItem.textureJpgAlpha = Convert.ToBase64String(alpha.EncodeToJPG());

                        //File.WriteAllBytes("d:/" + smr.name + ".jpg", textures[i].EncodeToJPG());
                        //File.WriteAllBytes("d:/" + smr.name + ".png", textures[i].EncodeToPNG());
                        //File.WriteAllBytes("d:/" + smr.name + "_a.jpg", alpha.EncodeToJPG());
                        

                        //Debug.Log(string.Format("RGBA32: {0} {1} {2}", smr.name, texItem.textureJpgRGB.Length, texItem.textureJpgAlpha.Length));
                    }


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


        mShareCameraSaver.gameObject.SetActive(true);


        int oldMask = mMainCamera.cullingMask;
        string[] mask = { "Default" };
        mMainCamera.cullingMask = LayerMask.GetMask(mask);

        var humanSaver = mMainCamera.GetComponent<RenderTextureSaver>();
        yield return humanSaver.TakePhoto(TextureFormat.RGB24);
        mMainCamera.cullingMask = oldMask;

        m_HumanImage.texture = humanSaver.GetTexture();



        m_QRCodeImage.texture = null;


        yield return new WaitForEndOfFrame();


        yield return new WaitForEndOfFrame();

        //-----------------------------------------------------------------------------------------------------------
        //below is  generate qrcode;
        List<SkinnedMeshRenderer> list = new List<SkinnedMeshRenderer>();
        list.AddRange(ModelDataManager.Instance.GetAllSkinnedMeshRenderer());
        list.AddRange(AvatarManager.Instance.GetAllSkinnedMeshRenderer());


        string jsonData = ShareManager.Instance.ToJson(list);

        File.WriteAllText("d:/test.json", jsonData);

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
            string retJson = request.downloadHandler.text;

            JsonRet ret = JsonUtility.FromJson<JsonRet>(retJson);


            string shareUrl = string.Format("https://m.yujishishi.com/pages/share/index.html?id={0}", ret.img.id);
            m_QRCodeImage.texture = mQRCodeTool.NormalEncodeQRCode(shareUrl, mQRSize);
            Debug.Log(shareUrl);



        }



        //----------------------------------------------------------
        //if use a share final texture
        //yield return mShareCameraSaver.TakePhoto(TextureFormat.RGB24);
        //Texture2D final = mShareCameraSaver.GetTexture();
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
    void OnCloseFinalWindow()
    {
        mShareCameraSaver.gameObject.SetActive(false);
    }

}
