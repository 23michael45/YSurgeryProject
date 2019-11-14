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

    public MeshFilter mShowLDMesh;

    void Start()
    {
        var op = Addressables.LoadAssetAsync<GameObject>(mAssetaddress);
        op.Completed += OnLoadDone;

        var refOp = mAssetRef.InstantiateAsync();
        refOp.Completed += OnInstantiateDone;

    }

    private void OnLoadDone(AsyncOperationHandle<GameObject> obj)
    {
        mAssetLoaded = GameObject.Instantiate(obj.Result);
    }
    private void OnInstantiateDone(AsyncOperationHandle<GameObject> obj)
    {
        mAssetRefLoaded = obj.Result;

        Mesh ldMesh = mAssetRefLoaded.GetComponent<MeshFilter>().sharedMesh;

        LoadHDMeshDefromedAndGenLowMesh(Application.dataPath + "/../Model/obamaMesh.obj", Application.dataPath + "/../Model/obamaTexture.jpg", ldMesh, mAssetRefLoaded.transform);
    }


    void LoadHDMeshDefromedAndGenLowMesh(string modelPath,string texturePath,Mesh ldMesh,Transform ldTransform)
    {

        GameObject gomesh = new OBJLoader().Load(modelPath);

        byte[] byteArray = File.ReadAllBytes(texturePath);
        Texture2D tex = new Texture2D(2, 2);
        bool isLoaded = tex.LoadImage(byteArray);

        gomesh.transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_MainTex", tex);



        SimplifyFaceModel sf = new SimplifyFaceModel();
        string jsonL2HPath = Path.Combine(Application.dataPath, "../correspondingHDLDIndices.json");

        Mesh hdDeformedMesh = gomesh.GetComponentInChildren<MeshFilter>().sharedMesh;
        Mesh lowDeformedMesh = sf.CalculateDeformedMesh(jsonL2HPath, hdDeformedMesh, ldMesh, ldTransform);

        mShowLDMesh.sharedMesh = lowDeformedMesh;

    }
}

