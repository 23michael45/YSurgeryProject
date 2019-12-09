#define USE_TEMPLATE_REF

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Dummiesman;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[Serializable]
public class RoleJson
{
    public Vector3[] vertices;
    public Vector2[] uv;
    //public int[] triangles;
    //public BoneWeight[] boneWeights;

    //public Matrix4x4[] bindposes;

    public Vector3[] bonelocalpositions;
    public Quaternion[] bonelocalrotation;
    public Vector3[] bonelocalscale;

    public static string Save(Mesh mesh, Transform[] bones)
    {
        RoleJson data = new RoleJson();
        data.vertices = mesh.vertices;
        data.uv = mesh.uv;
        //data.triangles = mesh.triangles;
        //data.boneWeights = mesh.boneWeights;

        if (bones != null)
        {
            //data.bindposes = mesh.bindposes;

            data.bonelocalpositions = new Vector3[bones.Length];
            data.bonelocalrotation = new Quaternion[bones.Length];
            data.bonelocalscale = new Vector3[bones.Length];


            for (int i = 0; i < bones.Length; i++)
            {
                data.bonelocalpositions[i] = bones[i].localPosition;
                data.bonelocalrotation[i] = bones[i].localRotation;
                data.bonelocalscale[i] = bones[i].localScale;

            }

        }

        string jsonStr = JsonUtility.ToJson(data);
        return jsonStr;
    }

    public static string Save(SkinnedMeshRenderer skinnedMeshRenderer)
    {
        Mesh skinnedMesh = skinnedMeshRenderer.sharedMesh;
        Transform[] bones = skinnedMeshRenderer.bones;
        return Save(skinnedMesh, bones);

    }
    public static string Save(MeshFilter meshFilter)
    {
        Mesh mesh = meshFilter.sharedMesh;
        return Save(mesh, null);

    }

    public static bool Load(string json, ref SkinnedMeshRenderer skinnedMesh,ref MeshFilter meshFilter)
    {
        RoleJson data = JsonUtility.FromJson<RoleJson>(json);



        Mesh mesh = new Mesh();

        mesh.vertices = data.vertices;
        mesh.uv = data.uv;
        mesh.triangles = skinnedMesh.sharedMesh.triangles;

        if (skinnedMesh.bones.Length != data.bonelocalpositions.Length)
        {
        }
        else
        {
            Transform[] bones = skinnedMesh.bones;
            for (int i = 0; i < data.bonelocalpositions.Length; i++)
            {


                bones[i].localPosition = data.bonelocalpositions[i];
                bones[i].localRotation = data.bonelocalrotation[i];
                bones[i].localScale = data.bonelocalscale[i];

            }
            skinnedMesh.bones = bones;



            Matrix4x4[] bindposes = new Matrix4x4[skinnedMesh.bones.Length];
            for (int i = 0; i < skinnedMesh.bones.Length; i++)
            {
                bindposes[i] = skinnedMesh.bones[i].worldToLocalMatrix * skinnedMesh.transform.localToWorldMatrix;
            }
            mesh.bindposes = bindposes;

            //mesh.boneWeights = data.boneWeights;
            mesh.boneWeights = skinnedMesh.sharedMesh.boneWeights;
        }


        skinnedMesh.sharedMesh = mesh;
        if (meshFilter)
        {
            meshFilter.sharedMesh = mesh;
        }
        return true;
    }

}
public class DeformJson
{
    public Vector3[] bonelocalpositions;
    public Quaternion[] bonelocalrotation;
    public Vector3[] bonelocalscale;
    

    public static string Save(Transform[] bones)
    {
        DeformJson data = new DeformJson();

        if (bones != null)
        {

            data.bonelocalpositions = new Vector3[bones.Length];
            data.bonelocalrotation = new Quaternion[bones.Length];
            data.bonelocalscale = new Vector3[bones.Length];


            for (int i = 0; i < bones.Length; i++)
            {
                data.bonelocalpositions[i] = bones[i].localPosition;
                data.bonelocalrotation[i] = bones[i].localRotation;
                data.bonelocalscale[i] = bones[i].localScale;

            }

        }

        string jsonStr = JsonUtility.ToJson(data);
        return jsonStr;
    }

    public static string Save(SkinnedMeshRenderer skinnedMeshRenderer)
    {
        Transform[] bones = skinnedMeshRenderer.bones;
        return Save(bones);

    }
    public static bool Load(string json, ref SkinnedMeshRenderer skinnedMesh)
    {
        DeformJson data = JsonUtility.FromJson<DeformJson>(json);

        

        if (skinnedMesh.bones.Length != data.bonelocalpositions.Length)
        {
        }
        else
        {
            Transform[] bones = skinnedMesh.bones;
            for (int i = 0; i < data.bonelocalpositions.Length; i++)
            {


                bones[i].localPosition = data.bonelocalpositions[i];
                bones[i].localRotation = data.bonelocalrotation[i];
                bones[i].localScale = data.bonelocalscale[i];

            }
            skinnedMesh.bones = bones;
            
        }
        
        return true;
    }

}

public class ModelDataManager : MonoBehaviour
{

    public static ModelDataManager Instance;

#if !USE_TEMPLATE_REF
    string lowMeshTempalteModelName = "LowMeshTemplateModelHead";
#else
    public AssetReference mTempalteModelRef;
#endif

    GameObject mLowMeshTemplate;
    string correspondingHDLDIndicesJson;
    string boneIndexMapJson;

    SkinnedMeshRenderer mSkinnedMeshRenderer;
    public MeshFilter mDebugMeshFilter;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {

        correspondingHDLDIndicesJson = BetterStreamingAssets.ReadAllText("Json/correspondingHDLDIndices.json");
        boneIndexMapJson = BetterStreamingAssets.ReadAllText("Json/boneIndexMap.json");
#if !USE_TEMPLATE_REF
        var opGo = Addressables.LoadAssetAsync<GameObject>(lowMeshTempalteModelName);
        opGo.Completed += OnLoadTemplateMeshDone;
#else
        var opGo = mTempalteModelRef.LoadAssetAsync<GameObject>();
        opGo.Completed += OnLoadTemplateMeshDone;
#endif


    }
    void OnLoadTemplateMeshDone(AsyncOperationHandle<GameObject> obj)
    {
        mLowMeshTemplate = GameObject.Instantiate(obj.Result);


        Transform orgHighModel = mLowMeshTemplate.transform.Find("BaselFaceModel2017");
        if (orgHighModel)
        {
            GameObject.Destroy(orgHighModel.gameObject);
        }

        mSkinnedMeshRenderer = mLowMeshTemplate.transform.Find("head001").GetComponent<SkinnedMeshRenderer>();

        
    }
    public void SaveLoadJsonTest(bool skinned)
    {
        string json = "";
        if (skinned)
        {
             json = RoleJson.Save(mSkinnedMeshRenderer.sharedMesh, mSkinnedMeshRenderer.bones);

        }
        else
        {
            json = RoleJson.Save(mDebugMeshFilter);

        }
        RoleJson.Load(json, ref mSkinnedMeshRenderer, ref mDebugMeshFilter);
    }
    public void RebindBone()
    {

        Mesh mesh = Instantiate(mSkinnedMeshRenderer.sharedMesh);

        var bindposes = mesh.bindposes;
        for (int i = 0; i < mSkinnedMeshRenderer.bones.Length; i++)
        {
            bindposes[i] = mSkinnedMeshRenderer.bones[i].worldToLocalMatrix * mSkinnedMeshRenderer.transform.localToWorldMatrix;
        }
        mesh.bindposes = bindposes;

        mSkinnedMeshRenderer.sharedMesh = mesh;

    }



    void CloneBoneHierarchy(Transform parentBoneTransform,Transform rootBoneTransform, Transform[] bones,out Transform[] newBones,out Transform newParentBoneTransform, out Transform newRootBoneTransform)
    {
        newParentBoneTransform = null;
        newRootBoneTransform = null;


        Transform[] srcBones = parentBoneTransform.GetComponentsInChildren<Transform>();


        Dictionary<string, Transform> SrcBoneDict = new Dictionary<string, Transform>();
        Dictionary<string, Transform> DstBoneDict = new Dictionary<string, Transform>();

        for (int i = 0; i < srcBones.Length; i++)
        {
            var srcBone = srcBones[i];
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
                // DstBoneDict[kv.Key].parent = mShowLDSkinMesh.transform;
                DstBoneDict[kv.Key].parent = null;
                newParentBoneTransform = DstBoneDict[kv.Key];
            }
            DstBoneDict[kv.Key].position = SrcBoneDict[kv.Key].position;
            DstBoneDict[kv.Key].rotation = SrcBoneDict[kv.Key].rotation;
            DstBoneDict[kv.Key].localScale = SrcBoneDict[kv.Key].localScale;


            if (kv.Key == rootBoneTransform.name)
            {
                newRootBoneTransform = DstBoneDict[kv.Key];
            }
        }

        newBones = new Transform[bones.Length];
        for (int i = 0; i < bones.Length; i++)
        {
            string boneName = bones[i].name;
            if (DstBoneDict.ContainsKey(boneName))
            {
                newBones[i] = DstBoneDict[boneName];

            }
            else
            {
                Debug.Log("Bone Not Exist In DstDoneDict : " + boneName);
            }


        }
    }

    public string CalculateLowPolyFace(byte[] hdObjData)
    {
        if (mLowMeshTemplate == null)
        {
            return null;
        }

        Debug.Log("CalculateLowPolyFace Start");

        mLowMeshTemplate.name = "LoadedAssetTemplateModel";
        Transform skinTransform = mSkinnedMeshRenderer.transform;
        Transform parentBoneTransform = mLowMeshTemplate.transform.Find("Reference");


        Stream stream = new MemoryStream(hdObjData);
        GameObject deformedMeshObject = new OBJLoader().Load(stream);


        
        SimplifyFaceModel sf = new SimplifyFaceModel();
        MeshFilter defromedMeshFilter = deformedMeshObject.GetComponentInChildren<MeshFilter>();
        Mesh hdDeformedMesh = Instantiate(defromedMeshFilter.sharedMesh);
        GameObject.Destroy(deformedMeshObject);

        Vector3[] vertices;
        Vector2[] uvs;
        Vector2[] uvInRegion;
        int[] indices;


        Debug.Log("CalculateLowPolyFace CalculateDeformedMesh");


        sf.CalculateDeformedMesh(correspondingHDLDIndicesJson, hdDeformedMesh, defromedMeshFilter.transform, mSkinnedMeshRenderer.sharedMesh, skinTransform, out vertices, out uvs, out uvInRegion, out indices);



        Transform[] newBones;
        Transform newParentBoneTransform;
        Transform newRootBoneTransform;

        Debug.Log("CalculateLowPolyFace CloneBoneHierarchy");
        CloneBoneHierarchy(parentBoneTransform, mSkinnedMeshRenderer.rootBone, mSkinnedMeshRenderer.bones, out newBones, out newParentBoneTransform, out newRootBoneTransform);

        

        Matrix4x4[] bindposes;
        BoneWeight[] weights;

        Debug.Log("CalculateLowPolyFace RebindBones");
        sf.RebindBones(boneIndexMapJson, hdDeformedMesh, mSkinnedMeshRenderer.transform, newBones, newParentBoneTransform, out bindposes);
        
        Mesh lowDeformedSkinMesh = new Mesh();
        lowDeformedSkinMesh.vertices = vertices;

        //uv分两通道 区域内用计算出的，区域外用低模自身的
        lowDeformedSkinMesh.uv = uvs;


        lowDeformedSkinMesh.triangles = indices;
        
        lowDeformedSkinMesh.bindposes = bindposes;
        lowDeformedSkinMesh.boneWeights = mSkinnedMeshRenderer.sharedMesh.boneWeights;






        //计算时不改变原 SkinnedMeshRenderer ,读取时再改变
        //mSkinnedMeshRenderer.bones = newBones;
        //mSkinnedMeshRenderer.rootBone = newRootBoneTransform;
        //mSkinnedMeshRenderer.sharedMesh = lowDeformedSkinMesh;

        //newParentBoneTransform.name = "RootBoneFitted";
        //newParentBoneTransform.parent = rootBoneTransform.parent;
        //GameObject.Destroy(rootBoneTransform.gameObject);

        if (mDebugMeshFilter)
        {
            mDebugMeshFilter.sharedMesh = lowDeformedSkinMesh;
        }

        Debug.Log("CalculateLowPolyFace Start RoleJson Save");
        string roleJson = RoleJson.Save(lowDeformedSkinMesh, newBones);
        
        GameObject.Destroy(newParentBoneTransform.gameObject);

        Debug.Log("CalculateLowPolyFace Return RoleJson");
        return roleJson;
    }


    public bool LoadLowPolyFace(string roleJson, Texture2D tex)
    {
        if (tex != null)
        {
            Material material = new Material(Shader.Find("Unlit/Texture"));
            material.SetTexture("_MainTex", tex);
            mSkinnedMeshRenderer.sharedMaterial = material;

        }
        return RoleJson.Load(roleJson, ref mSkinnedMeshRenderer,ref mDebugMeshFilter);
    }


    public string SaveDeform()
    {
        return DeformJson.Save(mSkinnedMeshRenderer);
    }

    public bool LoadDeform(string deformJson)
    {
        return DeformJson.Load(deformJson, ref mSkinnedMeshRenderer);
    }

    public string SaveAvatar()
    {
        return DeformJson.Save(mSkinnedMeshRenderer);
    }

    public bool LoadAvatar(string avatarJson)
    {
        return DeformJson.Load(avatarJson, ref mSkinnedMeshRenderer);
    }
}
