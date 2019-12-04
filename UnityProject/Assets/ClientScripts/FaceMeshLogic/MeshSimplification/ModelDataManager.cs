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
    public int[] triangles;
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
        data.triangles = mesh.triangles;
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
        mesh.triangles = data.triangles;

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

public class ModelDataManager : MonoBehaviour
{

    public static ModelDataManager Instance;


    string lowMeshTempalteName = "LowMeshTemplate";

    GameObject mLowMeshTemplate;
    string jsonL2HPath;
    string jsonBoneMapPath;

    SkinnedMeshRenderer mSkinnedMeshRenderer;
    public MeshFilter mDebugMeshFilter;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {

        jsonL2HPath = Path.Combine(Application.streamingAssetsPath, "Json/correspondingHDLDIndices.json");
        jsonBoneMapPath = Path.Combine(Application.streamingAssetsPath, "Json/boneIndexMap.json");

        var opGo = Addressables.LoadAssetAsync<GameObject>(lowMeshTempalteName);
        opGo.Completed += OnLoadTemplateMeshDone;

    }
    void OnLoadTemplateMeshDone(AsyncOperationHandle<GameObject> obj)
    {
        mLowMeshTemplate = GameObject.Instantiate(obj.Result);

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


    public string CalculateLowPolyFace(string hdObjPath)
    {
        if (mLowMeshTemplate == null)
        {
            return null;
        }


        mLowMeshTemplate.name = "LoadedAssetTemplateModel";
        Transform skinTransform = mSkinnedMeshRenderer.transform;
        Transform rootBoneTransform = mLowMeshTemplate.transform.Find("Reference");

        GameObject deformedMeshObject = new OBJLoader().Load(hdObjPath);

        SimplifyFaceModel sf = new SimplifyFaceModel();
        Mesh hdDeformedMesh = deformedMeshObject.GetComponentInChildren<MeshFilter>().sharedMesh;


        Vector3[] vertices;
        Vector2[] uvs;
        Vector2[] uvInRegion;
        int[] indices;
        sf.CalculateDeformedMesh(jsonL2HPath, hdDeformedMesh, mSkinnedMeshRenderer.sharedMesh, skinTransform, out vertices, out uvs, out uvInRegion, out indices);

        Matrix4x4[] bindposes;
        BoneWeight[] weights;

        sf.RebindBones(jsonBoneMapPath, hdDeformedMesh, mSkinnedMeshRenderer, out bindposes, out weights);



        Mesh lowDeformedSkinMesh = new Mesh();
        lowDeformedSkinMesh.vertices = vertices;

        //uv分两通道 区域内用计算出的，区域外用低模自身的
        lowDeformedSkinMesh.uv = uvs;
        // lowDeformedSkinMesh.uv2 = uvInRegion;

        //uv服务器已经算好了，不用再算了
        //lowDeformedSkinMesh.uv = mSkinnedMeshRenderer.sharedMesh.uv;


        lowDeformedSkinMesh.triangles = indices;

        lowDeformedSkinMesh.bindposes = bindposes;
        lowDeformedSkinMesh.boneWeights = weights;



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
                // DstBoneDict[kv.Key].parent = mShowLDSkinMesh.transform;
                DstBoneDict[kv.Key].parent = null;
            }
            DstBoneDict[kv.Key].position = SrcBoneDict[kv.Key].position;
            DstBoneDict[kv.Key].rotation = SrcBoneDict[kv.Key].rotation;
            DstBoneDict[kv.Key].localScale = SrcBoneDict[kv.Key].localScale;
        }

        Transform[] newBones = new Transform[mSkinnedMeshRenderer.bones.Length];
        for (int i = 0; i < mSkinnedMeshRenderer.bones.Length; i++)
        {
            string boneName = mSkinnedMeshRenderer.bones[i].name;
            if (DstBoneDict.ContainsKey(boneName))
            {
                newBones[i] = DstBoneDict[boneName];

            }
            else
            {
                Debug.Log("Bone Not Exist In DstDoneDict : " + boneName);
            }


        }



        mSkinnedMeshRenderer.bones = newBones;
        mSkinnedMeshRenderer.sharedMesh = lowDeformedSkinMesh;
        //mSkinnedMeshRenderer.sharedMaterial.SetTexture("_MainTex", tex);
        if(mDebugMeshFilter)
        {
            mDebugMeshFilter.sharedMesh = lowDeformedSkinMesh;
        }
        return RoleJson.Save(lowDeformedSkinMesh, newBones);
    }


    public bool LoadLowPolyFace(string roleJson, string texturePath)
    {
        bool ret = RoleJson.Load(roleJson, ref mSkinnedMeshRenderer,ref mDebugMeshFilter);
        if (!ret)
        {
            return false;
        }

        byte[] byteArray = File.ReadAllBytes(texturePath);
        Texture2D tex = new Texture2D(2, 2);
        bool isLoaded = tex.LoadImage(byteArray);
        if (!isLoaded)
        {
            return false;
        }

        Material material = new Material(Shader.Find("Unlit/Texture"));
        material.SetTexture("_MainTex", tex);
        mSkinnedMeshRenderer.sharedMaterial = material;
        return ret;
    }
}
