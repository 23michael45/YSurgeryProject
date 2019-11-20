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
    public SkinnedMeshRenderer mShowLDSkinMesh;

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

        mAssetRefLoaded.name = "LoadedAssetTemplateModel";
        Transform skinTransform = mAssetRefLoaded.transform.Find("head001");
        if (skinTransform)
        {
            SkinnedMeshRenderer ldSkinMesh = skinTransform.gameObject.GetComponent<SkinnedMeshRenderer>();
            LoadHDMeshDefromedAndGenLowMesh(Application.dataPath + "/../Model/obama53149.obj", Application.dataPath + "/../Model/obamaTexture.jpg", ldSkinMesh, skinTransform);

        }
        else
        {
            Debug.LogError("No Skin Mesh Head Found");
        }
        mAssetRefLoaded.SetActive(false);

    }


    void LoadHDMeshDefromedAndGenLowMesh(string modelPath, string texturePath, SkinnedMeshRenderer ldSkinMesh, Transform ldTransform)
    {

        GameObject deformedMeshObject = new OBJLoader().Load(modelPath);

        byte[] byteArray = File.ReadAllBytes(texturePath);
        Texture2D tex = new Texture2D(2, 2);
        bool isLoaded = tex.LoadImage(byteArray);

        deformedMeshObject.transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_MainTex", tex);
        deformedMeshObject.SetActive(false);


        SimplifyFaceModel sf = new SimplifyFaceModel();
        string jsonL2HPath = Path.Combine(Application.dataPath, "../correspondingHDLDIndices.json");
        string jsonBoneMapPath = Path.Combine(Application.dataPath, "../boneIndexMap.json");

        Mesh hdDeformedMesh = deformedMeshObject.GetComponentInChildren<MeshFilter>().sharedMesh;

        Vector3 testPos = hdDeformedMesh.vertices[0];
        var p1 = deformedMeshObject.transform.TransformPoint(testPos);
        var p2 = deformedMeshObject.transform.localToWorldMatrix.MultiplyPoint(testPos);



        Vector3[] vertices;
        Vector2[] uvs;
        int[] indices;
        sf.CalculateDeformedMesh(jsonL2HPath, hdDeformedMesh, ldSkinMesh.sharedMesh, ldTransform, out vertices, out uvs, out indices);

        Matrix4x4[] bindposes;
        BoneWeight[] weights;

        sf.RebindBones(jsonBoneMapPath, hdDeformedMesh, ldSkinMesh, out bindposes, out weights);



        Mesh lowDeformedSkinMesh = new Mesh();
        lowDeformedSkinMesh.vertices = vertices;
        lowDeformedSkinMesh.uv = uvs;
        lowDeformedSkinMesh.triangles = indices;

        lowDeformedSkinMesh.bindposes = bindposes;
        lowDeformedSkinMesh.boneWeights = weights;


        // Mesh tempmesh = lowDeformedSkinMesh;
        // for (int i = 0; i < tempmesh.boneWeights.Length; i++)
        // {
        //     Debug.Log("boneWeights:" + tempmesh.boneWeights[i].weight0);
        //     Debug.Log("boneIndex:" + tempmesh.boneWeights[i].boneIndex0);

        //     if (i > 100) break;
        // }


        // Dictionary<string, Transform> tempDict = new Dictionary<string, Transform>();

        // Transform[] newBones = new Transform[ldSkinMesh.bones.Length];
        // for (int i = 0; i < ldSkinMesh.bones.Length; i++)
        // {

        //     var srcBone = ldSkinMesh.bones[i];

        //     Transform bone = new GameObject(srcBone.name).transform;
        //     tempDict[srcBone.name] = bone;

        //     newBones[i] = bone;
        // }
        // for (int i = 0; i < newBones.Length; i++)
        // {
        //     var srcBone = ldSkinMesh.bones[i];

        //     if (tempDict.ContainsKey(srcBone.parent.name))
        //     {

        //         newBones[i].parent = tempDict[srcBone.parent.name];
        //     }
        //     else
        //     {
        //         newBones[i].parent = mShowLDSkinMesh.transform;
        //     }


        //     newBones[i].position = srcBone.position;
        //     newBones[i].rotation = srcBone.rotation;
        // }


        mShowLDSkinMesh.bones = ldSkinMesh.bones;
        mShowLDSkinMesh.sharedMesh = lowDeformedSkinMesh;


        mShowLDMesh.sharedMesh = lowDeformedSkinMesh;

    }
}

