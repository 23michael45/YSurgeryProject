﻿#define USE_TEMPLATE_REF

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Dummiesman;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;




[Serializable]
public class CalculateResultDataJson
{
    public int ret;
    public string retMsg;


    [Serializable]
    public class JCalcRet
    {
        [Serializable]
        public class JHSVOffset
        {
            public float h;
            public float s;
            public float v;

        }
        public JHSVOffset hsv_offset;

    }

    [Serializable]
    public class JInfo
    {
        public string meshFile;
        public JCalcRet calcRet;
        public string TextureFile;
    };

    public JInfo info;
}




[Serializable]
public class RoleJson
{
    public int gender;
    public float height;
    public float weight;

    public Vector3[] vertices;
    public Vector2[] uv;
    //public int[] triangles;
    //public BoneWeight[] boneWeights;

    //public Matrix4x4[] bindposes;

    public Vector3[] bonelocalpositions;
    public Quaternion[] bonelocalrotation;
    public Vector3[] bonelocalscale;

    public static string Save(Mesh mesh, Transform[] bones, int gender, float height, float weight)
    {
        RoleJson data = new RoleJson();

        data.gender = gender;
        data.height = height;
        data.weight = weight;

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

    public static string Save(SkinnedMeshRenderer skinnedMeshRenderer, int gender, float height, float weight)
    {
        Mesh skinnedMesh = skinnedMeshRenderer.sharedMesh;
        Transform[] bones = skinnedMeshRenderer.bones;
        return Save(skinnedMesh, bones, gender, height, weight);

    }
    public static string Save(MeshFilter meshFilter, int gender, float height, float weight)
    {
        Mesh mesh = meshFilter.sharedMesh;
        return Save(mesh, null, gender, height, weight);

    }

    public static RoleJson Load(string json, ref SkinnedMeshRenderer skinnedMesh, ref MeshFilter meshFilter)
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
        return data;
    }

}

public class DeformJson_Lengacy
{
    public Vector3[] bonelocalpositions;
    public Quaternion[] bonelocalrotation;
    public Vector3[] bonelocalscale;


    public static string Save(Transform[] bones)
    {
        DeformJson_Lengacy data = new DeformJson_Lengacy();

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
        DeformJson_Lengacy data = JsonUtility.FromJson<DeformJson_Lengacy>(json);



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



[Serializable]
public class DeformJson
{
    public DeformJson()
    {
        shape = new Shape();
        face = new Face();
        eyebrow = new Eyebrow();
        eye = new Eye();
        nose = new Nose();
        mouth = new Mouth();
        chest = new Chest();
        body = new Body();
    }


    [Serializable]
    public class Shape
    {
        public Vector4 ForeheadSwitch, TempleSwitch, BISjawSwitch, ChinSwitch, TopHeadSwitch;
    }
    [Serializable]
    public class Face
    {
        public Vector4 ApplemuscleSwitch, CheekbonesSwitch, FacialpartSwitch, MasseterMuscle;
    }
    [Serializable]
    public class Eyebrow
    {
        public Vector4 BrowbowSwitch, BrowHeadSwitch, BrowMiddleSwitch, BrowTailSwitch;
    }
    [Serializable]
    public class Eye
    {
        public Vector4 EyeZeroSwitch, EyecornerSwitch, UppereyelidSwitch, DoublefoldEyelidsSwitch,
                        lowereyelidSwitch, EyebagSwitch, EyetailSwitch;
    }
    [Serializable]
    public class Nose
    {
        public Vector4 NoseZeroSwitch, UpperbridgeSwitch, InferiorbridgeSwitch, NoseheadSwitch,
                        ColumellaNasiSwitch, NasalBaseSwitch, NoseWingSwitch;
    }
    [Serializable]
    public class Mouth
    {
        public Vector4 MouthZeroSwitch, UplipSwitch, UpjawSwitch, DownLipSwitch,
                       DownJawSwitch, PhiltrumSwitch, CornerSwitch;
    }
    [Serializable]
    public class Chest
    {
        public Vector4 upperItemSwitch, topItemSwitch, downItemSwitch;
    }
    [Serializable]
    public class Body
    {
        public Vector4 NeckSwitch, ChestSwitch, WristSwitch, HipSwitch, LegSwitch, ArmSwitch,
                       ForeheadSwitch, BISjawSwitch, ChinSwitch;
    }

    public Shape shape;
    public Face face;
    public Eyebrow eyebrow;
    public Eye eye;
    public Nose nose;
    public Mouth mouth;
    public Chest chest;
    public Body body;


    public static string Save(DeformJson deform)
    {
        return JsonUtility.ToJson(deform);
    }
    public static DeformJson Load(string json)
    {
        DeformJson deform = JsonUtility.FromJson<DeformJson>(json);
        return deform;
    }

}// 每个角色的变形属性

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

    GameObject GetBody(int gender)
    {
        string bodyName = "";
        if (gender == 0)
        {

            bodyName = "man_body";
        }
        else
        {
            bodyName = "women_body";

        }

        return mLowMeshTemplate.transform.Find(bodyName).gameObject;
    }
    GameObject GetNail(int gender)
    {
        string goName = "";
        if (gender == 0)
        {
            goName = "man_zhijia";
        }
        else
        {
            goName = "women_nail";

        }
        return mLowMeshTemplate.transform.Find(goName).gameObject;
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
        mSkinnedMeshRenderer.enabled = false;
        GetBody(0).SetActive(false);
        GetNail(0).SetActive(false);
        GetBody(1).SetActive(false);
        GetNail(1).SetActive(false);

        mLowMeshTemplate.transform.parent = LoadManager.Instance.transform;

    }
    public void SaveLoadJsonTest(bool skinned)
    {
        string json = "";
        if (skinned)
        {
            json = RoleJson.Save(mSkinnedMeshRenderer.sharedMesh, mSkinnedMeshRenderer.bones, 0, 1.78f, 75f);

        }
        else
        {
            json = RoleJson.Save(mDebugMeshFilter, 0, 1.78f, 75f);

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



    void CloneBoneHierarchy(Transform parentBoneTransform, Transform rootBoneTransform, Transform[] bones, out Transform[] newBones, out Transform newParentBoneTransform, out Transform newRootBoneTransform)
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


    void SetTemplateXDirection(bool bPositive)
    {
        Vector3 localScale = mLowMeshTemplate.transform.localScale;
        float scaleX = localScale.x;
        scaleX = Math.Abs(scaleX);

        if (bPositive)
        {
            localScale.x = scaleX;
        }
        else
        {

            localScale.x = -scaleX;
        }
        mLowMeshTemplate.transform.localScale = localScale;
    }

    public string CalculateLowPolyFace(byte[] hdObjData, int gender, float height, float weight)
    {
        if (mLowMeshTemplate == null)
        {
            return null;
        }
        SetTemplateXDirection(true);

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
        string roleJson = RoleJson.Save(lowDeformedSkinMesh, newBones, gender, height, weight);

        GameObject.Destroy(newParentBoneTransform.gameObject);

        Debug.Log("CalculateLowPolyFace Return RoleJson");

        
        SetTemplateXDirection(false);
        return roleJson;
    }


    public bool LoadLowPolyFace(string roleJson, Texture2D tex)
    {
        SetTemplateXDirection(true);

        Debug.Log("Start LoadLowPolyFace");
        if (tex != null)
        {
            mSkinnedMeshRenderer.sharedMaterial.SetTexture("_MainTex", tex);

        }
        Debug.Log("Start RoleJson Load");
        RoleJson roleJsonData = RoleJson.Load(roleJson, ref mSkinnedMeshRenderer, ref mDebugMeshFilter);

        //to do select model body by gender,height ,weight
        if (roleJsonData.gender == 0)
        {
            GetBody(0).SetActive(true);
            GetNail(0).SetActive(true);
            GetBody(1).SetActive(false);
            GetNail(1).SetActive(false);
        }
        else
        {
            GetBody(0).SetActive(false);
            GetNail(0).SetActive(false);
            GetBody(1).SetActive(true);
            GetNail(1).SetActive(true);

        }
        mSkinnedMeshRenderer.enabled = true;

        Role role = mLowMeshTemplate.GetComponent<Role>();
        LoadManager.Instance.newUser(role);

        SetTemplateXDirection(false);
        return true;
    }

    public bool FitCalculationJson(CalculateResultDataJson jsonData, int gender)
    {
        Vector3 hsvoffset = new Vector3(jsonData.info.calcRet.hsv_offset.h, jsonData.info.calcRet.hsv_offset.s, jsonData.info.calcRet.hsv_offset.v);

        foreach (var mat in GetBody(gender).GetComponent<SkinnedMeshRenderer>().sharedMaterials)
        {
            float hue = hsvoffset.x * 2;
            float sat = hsvoffset.y / 255;
            float val = hsvoffset.z / 255;



            mat.SetInt("_Hue", (int)hue);
            mat.SetFloat("_Saturation", sat);
            mat.SetFloat("_Value", val);
        }

        return true;
    }

    public string SaveDeform()
    {
        SetTemplateXDirection(true);
        string deformJson = DeformJson.Save(AppRoot.MainUser.currentModel.deform);
        
        SetTemplateXDirection(false);
        return deformJson;
    }

    public bool LoadDeform(string deformJson)
    {
        SetTemplateXDirection(true);
        DeformJson deform = DeformJson.Load(deformJson);

        DeformUI.Instance.Load(deform);
        
        SetTemplateXDirection(false);
        return true;
    }

    public string SaveAvatar()
    {
        SetTemplateXDirection(true);
        SetTemplateXDirection(false);
        return "";
    }

    public bool LoadAvatar(string avatarJson)
    {
        SetTemplateXDirection(true);
        SetTemplateXDirection(false);
        return false;
    }
}
