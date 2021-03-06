﻿#define USE_TEMPLATE_REF

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
        [Serializable]
        public class JRGBOffset
        {
            public float r;
            public float g;
            public float b;

        }
        public JHSVOffset hsv_offset;
        public JRGBOffset rgb_offset;

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

    public CalculateResultDataJson retJsonData;

    public Vector3[] vertices;
    public Vector2[] uv;
    public Vector2[] uv2Type;
    //public int[] triangles;
    //public BoneWeight[] boneWeights;

    //public Matrix4x4[] bindposes;

    public Vector3[] bonelocalpositions;
    public Quaternion[] bonelocalrotation;
    public Vector3[] bonelocalscale;


    public static string Save(Mesh mesh, Transform[] bones, int gender, float height, float weight, CalculateResultDataJson retJsonData)
    {
        RoleJson data = new RoleJson();

        data.gender = gender;
        data.height = height;
        data.weight = weight;
        data.retJsonData = retJsonData;

        data.vertices = mesh.vertices;
        data.uv = mesh.uv;
        data.uv2Type = mesh.uv2;
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

    public static string Save(SkinnedMeshRenderer skinnedMeshRenderer, int gender, float height, float weight, CalculateResultDataJson retJsonData)
    {
        Mesh skinnedMesh = skinnedMeshRenderer.sharedMesh;
        Transform[] bones = skinnedMeshRenderer.bones;
        return Save(skinnedMesh, bones, gender, height, weight, retJsonData);

    }
    public static string Save(MeshFilter meshFilter, int gender, float height, float weight, CalculateResultDataJson retJsonData)
    {
        Mesh mesh = meshFilter.sharedMesh;
        return Save(mesh, null, gender, height, weight, retJsonData);

    }

    public static RoleJson Load(string json)
    {
        RoleJson data = JsonUtility.FromJson<RoleJson>(json);
        return data;
    }
    public void Load(ref SkinnedMeshRenderer skinnedMesh, ref MeshFilter meshFilter)
    {



        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.uv2 = uv2Type;
        mesh.triangles = skinnedMesh.sharedMesh.triangles;

        if (skinnedMesh.bones.Length != bonelocalpositions.Length)
        {
        }
        else
        {
            Transform[] bones = skinnedMesh.bones;
            for (int i = 0; i < bonelocalpositions.Length; i++)
            {


                bones[i].localPosition = bonelocalpositions[i];
                bones[i].localRotation = bonelocalrotation[i];
                bones[i].localScale = bonelocalscale[i];

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
    public static DeformJson Load(string json, ref SkinnedMeshRenderer skinnedMesh)
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

        return data;
    }

}

public class MeanInitData
{
    public class VertexData
    {
        public Vector3 localPosition;
    }
    public class BoneData
    {
        public Vector3 localPosition;
        public Quaternion localRotation;
        public Vector3 localScale;
        public Vector3 Position;
    }
    public List<VertexData> Vertices = new List<VertexData>();
    public Dictionary<string, BoneData> Bones = new Dictionary<string, BoneData>();
}

[Serializable]
public class DeformJson_UI
{
    public DeformJson_UI()
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
    public Texture2D mBoneWeightMask;
    public AnimationCurve mSkinColorCurve;
    [NonSerialized]
    public GameObject mLowGeometryTemplate;
    string correspondingHDLDIndicesJson;
    string boneIndexMapJson;

    SkinnedMeshRenderer mHeadSkinnedMeshRenderer;
    Animator mAnimator;
    Transform mReferenceTransform;
    public MeshFilter mDebugMeshFilter;

    MeanInitData mMeanInitData;
    RoleJson mCurrentRoleJson;
    Texture2D mCurrentHeadTexture;
    DeformJson mCurrentDeformJson;

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
    //Guo puls Gameobject head

    GameObject GetHead(int gender) {
        string headName = "";

        headName = "head";
        return mLowGeometryTemplate.transform.Find(headName).gameObject;
    }


    GameObject GetArm(int gender)
    {
        string bodyName = "";
        if (gender == 0)
        {

            bodyName = "man_arm";
        }
        else
        {
            bodyName = "women_arm";

        }

        return mLowGeometryTemplate.transform.Find(bodyName).gameObject;
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

        return mLowGeometryTemplate.transform.Find(bodyName).gameObject;
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
        return mLowGeometryTemplate.transform.Find(goName).gameObject;
    }

    void SetGender(int gender)
    {
        for(int i = 0; i<2;i++)
        {
            if(i == gender)
            {

                GetBody(i).SetActive(true);
                GetNail(i).SetActive(true);
                GetArm(i).SetActive(true);
            }
            else
            {
                GetBody(i).SetActive(false);
                GetNail(i).SetActive(false);
                GetArm(i).SetActive(false);
            }
        }
    }

    void CloneMaterial(SkinnedMeshRenderer smr)
    {
        for (int i = 0; i < smr.materials.Length; i++)
        {
            smr.materials[i] = new Material(smr.materials[i]);
        }
    }


    void InitBoneInitData()
    {
        mMeanInitData = new MeanInitData();


        Vector3[] vertices = mHeadSkinnedMeshRenderer.sharedMesh.vertices;
        foreach (Vector3 vertex in vertices)
        {
            MeanInitData.VertexData vd = new MeanInitData.VertexData();
            vd.localPosition = vertex;
            mMeanInitData.Vertices.Add(vd);
        }


        Transform[] srcBones = mReferenceTransform.GetComponentsInChildren<Transform>();
        foreach (Transform t in srcBones)
        {
            MeanInitData.BoneData bd = new MeanInitData.BoneData();
            bd.localPosition = t.localPosition;
            bd.localRotation = t.localRotation;
            bd.localScale = t.localScale;
            bd.Position = t.position;
            mMeanInitData.Bones[t.name] = bd;
        }
    }

   public  void LookAtBone(string boneName, ref Vector3 BonePostion) {
       
        BonePostion = mMeanInitData.Bones[boneName].Position;
    }


    public void ResetBoneInitData()
    {
        Mesh mesh = mHeadSkinnedMeshRenderer.sharedMesh;
        Vector3[] vertices = new Vector3[mesh.vertices.Length];
        for(int i = 0; i< vertices.Length;i++)
        {
            vertices[i] = mMeanInitData.Vertices[i].localPosition;
        }
        mHeadSkinnedMeshRenderer.sharedMesh.vertices = vertices;


        Transform[] srcBones = mReferenceTransform.GetComponentsInChildren<Transform>();
        foreach (Transform t in srcBones)
        {
            MeanInitData.BoneData bd = mMeanInitData.Bones[t.name];

            t.localPosition = bd.localPosition;
            t.localRotation = bd.localRotation;
            t.localScale = bd.localScale;
        }

        var bones = mHeadSkinnedMeshRenderer.bones;
        Matrix4x4[] bindposes = new Matrix4x4[mesh.bindposes.Length];
        for (int i = 0; i < bones.Length; i++)
        {
            bindposes[i] = bones[i].worldToLocalMatrix * mHeadSkinnedMeshRenderer.localToWorldMatrix;
        }
        mesh.bindposes = bindposes;

        DeformLeaderBoneManager.Instance.ResetBindPose();
    }

    void OnLoadTemplateMeshDone(AsyncOperationHandle<GameObject> obj)
    {
        mLowGeometryTemplate = GameObject.Instantiate(obj.Result);

        mAnimator = mLowGeometryTemplate.GetComponent<Animator>();

        Transform orgHighModel = mLowGeometryTemplate.transform.Find("BaselFaceModel2017");
        if (orgHighModel)
        {
            GameObject.Destroy(orgHighModel.gameObject);
        }
        //mLowGeometryTemplate.transform.Find("Object001").gameObject.SetActive(false);
       // mLowGeometryTemplate.transform.Find("teeth").gameObject.SetActive(false);

        mHeadSkinnedMeshRenderer = mLowGeometryTemplate.transform.Find("head").GetComponent<SkinnedMeshRenderer>();
        mHeadSkinnedMeshRenderer.enabled = false;

        //hide all man and woman
        SetGender(-1);

        mReferenceTransform = mLowGeometryTemplate.transform.Find("Reference");


        CloneMaterial(mHeadSkinnedMeshRenderer);
        CloneMaterial(GetBody(0).GetComponent<SkinnedMeshRenderer>());
        CloneMaterial(GetBody(1).GetComponent<SkinnedMeshRenderer>());


        mLowGeometryTemplate.transform.parent = LoadManager.Instance.transform;
        InitBoneInitData();
    }
    public void SaveLoadJsonTest(bool skinned)
    {
        string json = "";
        if (skinned)
        {
            json = RoleJson.Save(mHeadSkinnedMeshRenderer.sharedMesh, mHeadSkinnedMeshRenderer.bones, 0, 1.78f, 75f, null);

        }
        else
        {
            json = RoleJson.Save(mDebugMeshFilter, 0, 1.78f, 75f, null);

        }
        var roleJson = RoleJson.Load(json);
        roleJson.Load(ref mHeadSkinnedMeshRenderer, ref mDebugMeshFilter);
    }
    public void RebindBone()
    {

        Mesh mesh = Instantiate(mHeadSkinnedMeshRenderer.sharedMesh);

        var bindposes = mesh.bindposes;
        for (int i = 0; i < mHeadSkinnedMeshRenderer.bones.Length; i++)
        {
            bindposes[i] = mHeadSkinnedMeshRenderer.bones[i].worldToLocalMatrix * mHeadSkinnedMeshRenderer.transform.localToWorldMatrix;
        }
        mesh.bindposes = bindposes;

        mHeadSkinnedMeshRenderer.sharedMesh = mesh;

    }



    void CloneBoneHierarchy(Transform referenceTransform, Transform rootBoneTransform, Transform[] bones, out Transform[] newBones, out Transform newParentBoneTransform, out Transform newRootBoneTransform)
    {
        newParentBoneTransform = null;
        newRootBoneTransform = null;


        Transform[] srcBones = referenceTransform.GetComponentsInChildren<Transform>();


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
        Vector3 localScale = mLowGeometryTemplate.transform.localScale;
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
        mLowGeometryTemplate.transform.localScale = localScale;
    }
    CalculateResultDataJson GetResultDataJson(string jstr)
    {
        if (string.IsNullOrEmpty(jstr))
        {
            return null;
        }
        jstr = jstr.Replace("\\\"", "\"");
        jstr = jstr.Replace("/\"", "\"");
        jstr = jstr.Replace("\"{\"", "{\"");
        jstr = jstr.Replace("\"}\"", "\"}");
        jstr = jstr.Replace("\"{", "{");
        jstr = jstr.Replace("}\"", "}");
        jstr = jstr.Replace("\\\\n", "");
        jstr = jstr.Replace("\\\\t", "");
        jstr = jstr.Replace("\\\\", "");

        var jsondata = JsonUtility.FromJson<CalculateResultDataJson>(jstr);
        return jsondata;
    }

    public string CalculateLowPolyFace(byte[] hdObjData, int gender, float height, float weight, string retJson)
    {
        FreeView.Inst().ResetStage();
        ResetBoneInitData();
        if (mLowGeometryTemplate == null)
        {
            return null;
        }
        SetTemplateXDirection(true);


        Debug.Log("CalculateLowPolyFace Start");

        mLowGeometryTemplate.name = "LoadedAssetTemplateModel";
        Transform skinTransform = mHeadSkinnedMeshRenderer.transform;


        Stream stream = new MemoryStream(hdObjData);
        GameObject deformedMeshObject = new OBJLoader().Load(stream);



        SimplifyFaceModel sf = new SimplifyFaceModel();
        MeshFilter defromedMeshFilter = deformedMeshObject.GetComponentInChildren<MeshFilter>();
        Mesh hdDeformedMesh = Instantiate(defromedMeshFilter.sharedMesh);
        GameObject.Destroy(deformedMeshObject);

        Vector3[] vertices;
        Vector2[] uvs;
        Vector2[] uv2Type;
        int[] indices;


        Debug.Log("CalculateLowPolyFace CalculateDeformedMesh");


        sf.CalculateDeformedMesh(correspondingHDLDIndicesJson, hdDeformedMesh, defromedMeshFilter.transform, mHeadSkinnedMeshRenderer.sharedMesh, skinTransform, out vertices, out uvs, out uv2Type, out indices);



        Transform[] newBones;
        Transform newParentBoneTransform;
        Transform newRootBoneTransform;

        Debug.Log("CalculateLowPolyFace CloneBoneHierarchy");
        CloneBoneHierarchy(mReferenceTransform, mHeadSkinnedMeshRenderer.rootBone, mHeadSkinnedMeshRenderer.bones, out newBones, out newParentBoneTransform, out newRootBoneTransform);



        Matrix4x4[] bindposes;
        BoneWeight[] weights;

        Debug.Log("CalculateLowPolyFace RebindBones");
        sf.RebindBones(boneIndexMapJson, hdDeformedMesh, mHeadSkinnedMeshRenderer.transform, newBones, newParentBoneTransform, out bindposes);






        Vector3[] finalVertices;
        sf.CalculateFinalWeighTexture(correspondingHDLDIndicesJson, mBoneWeightMask, vertices, uvs, mHeadSkinnedMeshRenderer.sharedMesh.vertices, out finalVertices);




        Mesh lowDeformedSkinMesh = new Mesh();
        lowDeformedSkinMesh.vertices = finalVertices;

        //uv分两通道 uv2表示点类型 x 1表示脸内，0表示脸外   y表示点具体类型，如鼻孔等等
        lowDeformedSkinMesh.uv = uvs;
        lowDeformedSkinMesh.uv2 = uv2Type;


        lowDeformedSkinMesh.triangles = indices;

        lowDeformedSkinMesh.bindposes = bindposes;
        lowDeformedSkinMesh.boneWeights = mHeadSkinnedMeshRenderer.sharedMesh.boneWeights;






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

        CalculateResultDataJson retJsonData = GetResultDataJson(retJson);
        string roleJson = RoleJson.Save(lowDeformedSkinMesh, newBones, gender, height, weight, retJsonData);

        GameObject.Destroy(newParentBoneTransform.gameObject);

        Debug.Log("CalculateLowPolyFace Return RoleJson");



        SetTemplateXDirection(false);


        //after flip x ,then rebind bone leader pose
        DeformLeaderBoneManager.Instance.ResetBindPose();

        return roleJson;
    }



    public bool LoadLowPolyFace(string roleJson, Texture2D tex)
    {
        RoleJson roleJsonData = RoleJson.Load(roleJson);
        mCurrentRoleJson = roleJsonData;
        mCurrentHeadTexture = tex;
        bool b = LoadLowPolyFace(mCurrentRoleJson, mCurrentHeadTexture);

        return b;
    }
    public void ResetRole()
    {
        if (mCurrentRoleJson != null && mCurrentHeadTexture != null)
        {
            LoadLowPolyFace(mCurrentRoleJson, mCurrentHeadTexture);
        }
    }
    public bool LoadLowPolyFace(RoleJson roleJsonData, Texture2D tex)
    {
        mCurrentRoleJson = roleJsonData;


        FreeView.Inst().ResetStage();
        SetTemplateXDirection(true);

        Debug.Log("Start LoadLowPolyFace");
        if (tex != null)
        {
            mHeadSkinnedMeshRenderer.sharedMaterial.SetTexture("_MainTex", tex);

        }
        Debug.Log("Start RoleJson Load");
        roleJsonData.Load(ref mHeadSkinnedMeshRenderer, ref mDebugMeshFilter);


        //to do select model body by gender,height ,weight
        SetGender(roleJsonData.gender);
        mHeadSkinnedMeshRenderer.enabled = true;
        

        FitCalculationJson(roleJsonData.retJsonData, roleJsonData.gender, roleJsonData.weight, roleJsonData.height);


        SetTemplateXDirection(false);



        //after flip x ,then rebind bone leader pose
        DeformLeaderBoneManager.Instance.ResetBindPose();
        DeformLeaderBoneManager.Instance.RoleJsonInitData();
        DeformUI.Instance.Reload();
        mCurrentDeformJson = null;
        return true;
    }
    
    void FitHSV(SkinnedMeshRenderer smr,Vector3 hsvoffset)
    {
        for (int i = 0; i < smr.materials.Length; i++)
        {
            var mat = smr.materials[i];

            float hue = hsvoffset.x * 2;
            float sat = hsvoffset.y / 255;
            float val = hsvoffset.z / 255;

            mat.SetInt("_Hue", (int)hue);
            mat.SetFloat("_Saturation", sat);
            mat.SetFloat("_Value", val);

        }
    }
    void FitRGB(SkinnedMeshRenderer smr, Vector3 rgboffset)
    {
        for (int i = 0; i < smr.materials.Length; i++)
        {
            var mat = smr.materials[i];

            float r = rgboffset.x / 255;
            float g = rgboffset.y / 255;
            float b = rgboffset.z / 255;

            mat.SetFloat("_R", r);
            mat.SetFloat("_G", g);
            mat.SetFloat("_B", b);

        }
    }


    public bool FitCalculationJson(CalculateResultDataJson jsonData, int gender, float weight, float height)
    {
        if (jsonData != null && jsonData.info != null)
        {


            //Vector3 hsvoffset = new Vector3(jsonData.info.calcRet.hsv_offset.h, jsonData.info.calcRet.hsv_offset.s, jsonData.info.calcRet.hsv_offset.v);

            //FitHSV(GetBody(gender).GetComponent<SkinnedMeshRenderer>(), hsvoffset);
            //FitHSV(GetArm(gender).GetComponent<SkinnedMeshRenderer>(), hsvoffset);
            //FitHSV(GetHead(gender).GetComponent<SkinnedMeshRenderer>(), hsvoffset);


            Vector3 rgboffset = new Vector3(jsonData.info.calcRet.rgb_offset.r, jsonData.info.calcRet.rgb_offset.g, jsonData.info.calcRet.rgb_offset.b);

            FitRGB(GetBody(gender).GetComponent<SkinnedMeshRenderer>(), rgboffset);
            FitRGB(GetArm(gender).GetComponent<SkinnedMeshRenderer>(), rgboffset);
            FitRGB(GetHead(gender).GetComponent<SkinnedMeshRenderer>(), rgboffset);
            return true;
        }
        return false;
    }

    public string SaveDeform()
    {
        string deformJson = DeformJson.Save(mHeadSkinnedMeshRenderer);
        DeformLeaderBoneManager.Instance.ResetBindPose();

        return deformJson;
    }

    public bool LoadDeform(string deformJson)
    {
        DeformJson deform = DeformJson.Load(deformJson, ref mHeadSkinnedMeshRenderer);
        mCurrentDeformJson = deform;

        DeformLeaderBoneManager.Instance.ResetBindPose();
        DeformUI.Instance.Reload();
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



    public void ChangeFaceArea(string TexturePath)
    {
        Texture2D texture = Resources.Load(TexturePath) as Texture2D;

        Material Facematerial = mHeadSkinnedMeshRenderer.material;
        Facematerial.SetTexture("_AreaTex", texture);
        
    }


    public void PlayAnimation(string animationName)
    {
        mAnimator.Play(animationName);
    }




    public List<SkinnedMeshRenderer> GetAllSkinnedMeshRenderer()
    {
        List<SkinnedMeshRenderer> list = new List<SkinnedMeshRenderer>();
        list.Add(mHeadSkinnedMeshRenderer);
        list.Add(GetBody(mCurrentRoleJson.gender).GetComponent<SkinnedMeshRenderer>());
        list.Add(GetArm(mCurrentRoleJson.gender).GetComponent<SkinnedMeshRenderer>());
        list.Add(GetNail(mCurrentRoleJson.gender).GetComponent<SkinnedMeshRenderer>());


        //List<GameObject> avatars = new List<GameObject>();
        //for (int i = 0; i < avatars.Count; i++)
        //{
        //    list.Add(avatars[i].GetComponent<SkinnedMeshRenderer>());
        //}
        return list;
        
    }



    public void Makeup(string memberName, Texture tex)
    {
        ModelDataManager.Instance.mHeadSkinnedMeshRenderer.material.SetTexture(memberName, tex);
    }



    public void MakeupColor(string memberName, Vector3 color) {

        string _H = memberName + "_H";
        string _S = memberName + "_S";
        string _V = memberName + "_V";        

        ModelDataManager.Instance.mHeadSkinnedMeshRenderer.material.SetFloat(_H, color.x);
        ModelDataManager.Instance.mHeadSkinnedMeshRenderer.material.SetFloat(_S, color.y);
        ModelDataManager.Instance.mHeadSkinnedMeshRenderer.material.SetFloat(_V, color.z);
    }

}
