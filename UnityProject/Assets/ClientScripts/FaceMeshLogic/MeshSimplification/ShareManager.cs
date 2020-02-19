using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

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
    private void Awake()
    {
        Instance = this;
    }
   
    public void WriteJsonFile(string path,string json)
    {
        File.WriteAllText(path, json);
    }

    public string ToJson(Mesh headMesh,Texture2D headTexture)
    {
        ShareJson shareJson = new ShareJson();
        shareJson.headMeshObj = RuntimeObjExporter.MeshToString("headMesh", headMesh, null);

        if(headTexture != null)
        {

            byte[] headTextureJpgData = headTexture.EncodeToJPG();
            shareJson.headTextureJpg = Convert.ToBase64String(headTextureJpgData);
        }

        string json = JsonUtility.ToJson(shareJson);
        return json;
    }

}
