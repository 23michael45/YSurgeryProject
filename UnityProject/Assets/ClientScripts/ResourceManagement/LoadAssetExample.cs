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

    public string mModelName = "/../Model/obama53149.obj";

    public string mTextureName = "/../Model/obamaTexture.jpg";
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
        Transform rootBoneTransform = mAssetRefLoaded.transform.Find("Reference");
        if (skinTransform)
        {
            SkinnedMeshRenderer ldSkinMesh = skinTransform.gameObject.GetComponent<SkinnedMeshRenderer>();
            LoadHDMeshDefromedAndGenLowMesh(Path.Combine(Application.dataPath,mModelName) , Path.Combine(Application.dataPath,mTextureName), ldSkinMesh, skinTransform, rootBoneTransform);

        }
        else
        {
            Debug.LogError("No Skin Mesh Head Found");
        }
        mAssetRefLoaded.SetActive(false);

    }


    void LoadHDMeshDefromedAndGenLowMesh(string modelPath, string texturePath, SkinnedMeshRenderer ldSkinMesh, Transform ldTransform,Transform rootBoneTransform)
    {

        GameObject deformedMeshObject = new OBJLoader().Load(modelPath,false);

        byte[] byteArray = File.ReadAllBytes(texturePath);
        Texture2D tex = new Texture2D(2, 2);
        bool isLoaded = tex.LoadImage(byteArray);

        deformedMeshObject.SetActive(false);


        SimplifyFaceModel sf = new SimplifyFaceModel();
        string jsonL2HPath = Path.Combine(Application.dataPath, "../correspondingHDLDIndices.json");
        string jsonBoneMapPath = Path.Combine(Application.dataPath, "../boneIndexMap.json");

        MeshFilter deformedMeshFilter = deformedMeshObject.GetComponentInChildren<MeshFilter>();
        Mesh hdDeformedMesh = deformedMeshFilter.sharedMesh;
        

        Vector3[] vertices;
        Vector2[] uvs;
        Vector2[] uvInRegion;
        int[] indices;
        sf.CalculateDeformedMesh(jsonL2HPath, hdDeformedMesh, deformedMeshFilter.transform, ldSkinMesh.sharedMesh, ldTransform, out vertices, out uvs, out uvInRegion,out indices);

        Matrix4x4[] bindposes;
        BoneWeight[] weights;

        sf.RebindBones(jsonBoneMapPath, hdDeformedMesh, ldSkinMesh.transform, ldSkinMesh.bones, ldSkinMesh.rootBone,out bindposes);



        Mesh lowDeformedSkinMesh = new Mesh();
        lowDeformedSkinMesh.vertices = vertices;

        //uv分两通道 区域内用计算出的，区域外用低模自身的
        lowDeformedSkinMesh.uv = uvs;
        lowDeformedSkinMesh.uv2 = uvInRegion;

        lowDeformedSkinMesh.triangles = indices;

        lowDeformedSkinMesh.bindposes = bindposes;
        lowDeformedSkinMesh.boneWeights = ldSkinMesh.sharedMesh.boneWeights;


        
        Transform[] ldBonesHierarchy = rootBoneTransform.GetComponentsInChildren<Transform>();

        Dictionary<string, Transform> SrcBoneDict = new Dictionary<string, Transform>();
        Dictionary<string, Transform> DstBoneDict = new Dictionary<string, Transform>();

        for (int i = 0; i < ldBonesHierarchy.Length; i++)
        {
            var srcBone = ldBonesHierarchy[i];
            Transform bone = new GameObject(srcBone.name).transform;

            SrcBoneDict[srcBone.name] = srcBone;
            DstBoneDict[srcBone.name] = bone;

        }


        foreach (var kv in DstBoneDict)
        {

            string parentName = SrcBoneDict[kv.Key].parent.name;
            if (DstBoneDict.ContainsKey(parentName))
            {
                DstBoneDict[kv.Key].parent = DstBoneDict[parentName];
            }
            else
            {
                DstBoneDict[kv.Key].parent = mShowLDSkinMesh.transform;
            }
            DstBoneDict[kv.Key].position = SrcBoneDict[kv.Key].position;
            DstBoneDict[kv.Key].rotation = SrcBoneDict[kv.Key].rotation;
            DstBoneDict[kv.Key].localScale = SrcBoneDict[kv.Key].localScale;
        }

        Transform[] newBones = new Transform[ldSkinMesh.bones.Length];
        for (int i = 0; i < ldSkinMesh.bones.Length; i++)
        {
            string boneName = ldSkinMesh.bones[i].name;
            if (DstBoneDict.ContainsKey(boneName))
            {
                newBones[i] = DstBoneDict[boneName];

            }
            else
            {
                Debug.Log("Bone Not Exist In DstDoneDict : " + boneName);
            }


        }

        mShowLDSkinMesh.bones = newBones;
        mShowLDSkinMesh.sharedMesh = lowDeformedSkinMesh;

        
        mShowLDSkinMesh.sharedMaterial.SetTexture("_MainTex", tex);


        mShowLDMesh.sharedMesh = lowDeformedSkinMesh;

    }
}

