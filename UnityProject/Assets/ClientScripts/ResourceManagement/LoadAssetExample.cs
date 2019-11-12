using Dummiesman;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadAssetExample : MonoBehaviour
{
    public string mAssetaddress;
    public AssetReference mAssetRef;
    

    GameObject mAssetLoaded;
    GameObject mAssetRefLoaded;

    void Start()
    {
        var op = Addressables.LoadAssetAsync<GameObject>(mAssetaddress);
        op.Completed += OnLoadDone;

        var refOp = mAssetRef.InstantiateAsync();
        refOp.Completed += OnInstantiateDone;

        LoadFromFile(Application.dataPath + "/../Model/obamaMesh.obj", Application.dataPath + "/../Model/obamaTexture.jpg");
    }

    private void OnLoadDone(AsyncOperationHandle<GameObject> obj)
    {
        mAssetLoaded = GameObject.Instantiate(obj.Result);
    }
    private void OnInstantiateDone(AsyncOperationHandle<GameObject> obj)
    {
        mAssetRefLoaded = obj.Result;
    }


    void LoadFromFile(string modelPath,string texturePath)
    {

        GameObject gomesh = new OBJLoader().Load(modelPath);

        byte[] byteArray = File.ReadAllBytes(texturePath);
        Texture2D tex = new Texture2D(2, 2);
        bool isLoaded = tex.LoadImage(byteArray);

        gomesh.transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_MainTex", tex);


    }
}

