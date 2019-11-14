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
        //var op = Addressables.LoadAssetAsync<GameObject>(mAssetaddress);
        //op.Completed += OnLoadDone;

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

        LoadHDMeshDefromedAndGenLowMesh(Application.dataPath + "/../Model/obama53149.obj", Application.dataPath + "/../Model/obamaTexture.jpg", ldMesh, mAssetRefLoaded.transform);
    }


    void LoadHDMeshDefromedAndGenLowMesh(string modelPath,string texturePath,Mesh ldMesh,Transform ldTransform)
    {

        GameObject deformedMeshObject = new OBJLoader().Load(modelPath);

        byte[] byteArray = File.ReadAllBytes(texturePath);
        Texture2D tex = new Texture2D(2, 2);
        bool isLoaded = tex.LoadImage(byteArray);

        deformedMeshObject.transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_MainTex", tex);



        SimplifyFaceModel sf = new SimplifyFaceModel();
        string jsonL2HPath = Path.Combine(Application.dataPath, "../correspondingHDLDIndices.json");

        Mesh hdDeformedMesh = deformedMeshObject.GetComponentInChildren<MeshFilter>().sharedMesh;

        Vector3 testPos = hdDeformedMesh.vertices[0];
        var p1 = deformedMeshObject.transform.TransformPoint(testPos);
        var p2 = deformedMeshObject.transform.localToWorldMatrix.MultiplyPoint(testPos);


        Mesh lowDeformedMesh = sf.CalculateDeformedMesh(jsonL2HPath, hdDeformedMesh, ldMesh, ldTransform);

        mShowLDMesh.sharedMesh = lowDeformedMesh;

    }
}

