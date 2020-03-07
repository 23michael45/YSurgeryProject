using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[Serializable]
public class DeformLeaderBoneManagerSetup
{
    [Serializable]
    public class LeaderBoneData
    {
        public string boneName = "default";
        public float range = 10.0f;
        public Vector4 offsetScale;

        public int curveIndex = 0;
        public List<string> excludeBoneNames = new List<string>();
    }

    [SerializeField]
    public List<LeaderBoneData> leaderBones = new List<LeaderBoneData>();

}

public class Snapshot
{
    public class BoneData
    {
        public Vector3 curPos;
        public Vector3 defaultPos;
        public Vector3 curScale;
        public bool editing;
    }
    public string toggleName;
    public string partName;
    public Dictionary<string, BoneData> map = new Dictionary<string, BoneData>();
}


public class DeformLeaderBoneManager : MonoBehaviour
{
    public static DeformLeaderBoneManager Instance;
    public Transform mRootBone;

    public bool mInitFromFile = false;
    
    [NonSerialized]
    public List<DeformLeaderBone> mLeaderBones = new List<DeformLeaderBone>();
    [NonSerialized]
    public List<DeformCommonBone> mCommonBones = new List<DeformCommonBone>();
    [NonSerialized]
    public Dictionary<string ,Transform> mBoneNameMap = new Dictionary<string,Transform>();


    [NonSerialized]
    Dictionary<string, DeformLeaderBone> mLeaderBoneDic = new Dictionary<string, DeformLeaderBone>();

    [NonSerialized]
    Dictionary<string, Vector3> mRoleJsonBoneInitPositionMap = new Dictionary<string, Vector3>();
    Dictionary<string, Transform> mRoleJsonBoneMap = new Dictionary<string, Transform>();

    DeformLeaderBoneManagerSetup mDeformLeaderBoneManagerSetup;
    void Awake()
    {
        Instance = this;
        if (mRootBone == null)
        {
            mRootBone = transform;
        }

    }

    private void Start()
    {

        if (mInitFromFile)
        {
            InitFromResource();
        }
        else
        {

            Init();
        }

        //SetWorking(false);
        //enabled = false;
    }
    void OnEnable()
    {
        SetWorking(true);
    }
    void OnDisable()
    {
        SetWorking(false);
    }
    public void SetWorking(bool b)
    {
        foreach (DeformLeaderBone lb in mLeaderBones)
        {
            lb.enabled = b;
        }
        foreach (DeformCommonBone cb in mCommonBones)
        {
            cb.enabled = b;
        }
    }

    void SetLeaderAffectByLeader(bool b)
    {
        foreach (DeformLeaderBone lb in mLeaderBones)
        {
            lb.mAffectedByLeader = b;
        }
    }

    void Init()
    {
        mLeaderBones.Clear();
        mCommonBones.Clear();
        mLeaderBoneDic.Clear();

        Transform[] bones = mRootBone.GetComponentsInChildren<Transform>();
        foreach (Transform bone in bones)
        {
            if (bone == mRootBone)
            {
                continue;
            }

            DeformLeaderBone lb = bone.GetComponent<DeformLeaderBone>();
            if (lb != null)
            {
                mLeaderBones.Add(lb);
                mLeaderBoneDic[lb.name] = lb;
                lb.mInRangeCommonBones.Clear();
                lb.mPositionFromLeaderBones.Clear();
            }
            else
            {
                DeformCommonBone cb = bone.gameObject.GetComponent<DeformCommonBone>();
                if (cb == null)
                {
                    cb = bone.gameObject.AddComponent<DeformCommonBone>();
                }


                if (cb != null)
                {
                    mCommonBones.Add(cb);
                    cb.mPositionFromLeaderBones.Clear();
                }
            }

        }


        InitBoneData();



    }

    public void ReinitBoneData()
    {
        InitBoneData();
    }

    void InitFromResource()
    {
        mLeaderBones.Clear();
        mCommonBones.Clear();
        mLeaderBoneDic.Clear();


        CurveHolder curves = Resources.Load<GameObject>("CurveHolder").GetComponent<CurveHolder>();


        TextAsset textAsset = Resources.Load<TextAsset>("LeaderBoneSetup");
        Debug.Log(textAsset.text);
        mDeformLeaderBoneManagerSetup = JsonUtility.FromJson<DeformLeaderBoneManagerSetup>(textAsset.text);

        Debug.Log(mDeformLeaderBoneManagerSetup.leaderBones.Count);



        Transform[] bones = mRootBone.GetComponentsInChildren<Transform>();
        foreach (Transform bone in bones)
        {
            mBoneNameMap[bone.name] = bone;
     
            if (bone == mRootBone)
            {
                continue;
            }
            DeformBaseBone deformBaseBone = bone.gameObject.GetComponent<DeformBaseBone>();
            if (deformBaseBone != null)
            {
                Destroy(deformBaseBone);
            }
            bool isLeader = false;
            foreach (var leaderBone in mDeformLeaderBoneManagerSetup.leaderBones)
            {
                if (bone.name == leaderBone.boneName)
                {
                    var lb = bone.gameObject.AddComponent<DeformLeaderBone>();

                    mLeaderBones.Add(lb);
                    mLeaderBoneDic[lb.name] = lb;
                    lb.mInRangeCommonBones.Clear();
                    lb.mPositionFromLeaderBones.Clear();
                    lb.mInRangeLeaderBones.Clear();

                    lb.mRange = leaderBone.range;
                    lb.mOffsetScale = leaderBone.offsetScale;
                    lb.mCurve = curves.curves[leaderBone.curveIndex];

                    lb.mExcludeBones = leaderBone.excludeBoneNames;
                    //lb.enabled = false;
                    isLeader = true;
                    break;
                }

            }

            if (!isLeader)
            {
                DeformCommonBone cb = bone.gameObject.GetComponent<DeformCommonBone>();
                if (cb == null)
                {
                    cb = bone.gameObject.AddComponent<DeformCommonBone>();
                }


                if (cb != null)
                {
                    mCommonBones.Add(cb);
                    cb.mPositionFromLeaderBones.Clear();
                    //cb.enabled = false;
                }
            }


        }
        InitBoneData();

        // DeformLeaderBoneManagerSetup setup = new DeformLeaderBoneManagerSetup();
        // setup.leaderBones.Add(new DeformLeaderBoneManagerSetup.LeaderBoneData());
        // setup.leaderBones.Add(new DeformLeaderBoneManagerSetup.LeaderBoneData());

        // string jstr = JsonUtility.ToJson(setup);
        // File.WriteAllText(Application.streamingAssetsPath + "/LeaderBoneSetup.bytes", jstr);

        SetLeaderAffectByLeader(true);
    }

    void InitBoneData()
    {

        foreach (DeformLeaderBone lb in mLeaderBones)
        {
            lb.mInRangeLeaderBones.Clear();
            lb.mPositionFromLeaderBones.Clear();
            lb.mInRangeCommonBones.Clear();
            foreach (DeformLeaderBone interlb in mLeaderBones)
            {
                if (interlb != lb)
                {
                    if (Vector3.Distance(lb.transform.position, interlb.transform.position) < lb.mRange)
                    {
                        //if (lb.transform.parent == interlb.transform.parent)
                        if(!lb.mExcludeBones.Contains(interlb.name))//lb 不影响哪些骨骼，在excludeBones里
                        {
                            lb.mInRangeLeaderBones.Add(interlb);
                        }
                    }
                    if (Vector3.Distance(lb.transform.position, interlb.transform.position) < interlb.mRange)
                    {
                        //if (lb.transform.parent == interlb.transform.parent)

                        if (!interlb.mExcludeBones.Contains(lb.name))//interlb 不影响哪些骨骼，在excludeBones里
                        {
                            lb.mPositionFromLeaderBones.Add(interlb);

                        }
                    }
                }

            }

        }




        foreach (DeformCommonBone cb in mCommonBones)
        {
            cb.mPositionFromLeaderBones.Clear();
            foreach (DeformLeaderBone interlb in mLeaderBones)
            {
                if (Vector3.Distance(cb.transform.position, interlb.transform.position) < interlb.mRange)
                {
                    //if (lb.transform.parent == cb.transform.parent)
                    if (!interlb.mExcludeBones.Contains(cb.name))//interlb 不影响哪些骨骼，在excludeBones里
                    {
                        interlb.mInRangeCommonBones.Add(cb);
                        cb.mPositionFromLeaderBones.Add(interlb);
                    }
                }
            }
        }
    }

    //重置骨骼初始值列表，为当前骨骼位置。初始值列表用于限定SLIDER条，与default position不同。初始值列表，只在LoadLowPoly时，初始化一次，每个人只有一份
    public void RoleJsonInitData()
    {

        mRoleJsonBoneInitPositionMap.Clear();
        mRoleJsonBoneMap.Clear();

        Transform[] bones = mRootBone.GetComponentsInChildren<Transform>(true);
        for(int i = 0; i< bones.Length;i++)
        {
            Transform bone = bones[i];
            //mRoleJsonBoneInitPositionMap[bone.name] = bone.position;
            mRoleJsonBoneInitPositionMap[bone.name] = bone.localPosition;
            mRoleJsonBoneMap[bone.name] = bone;
        }
        
    }

    public Vector4 GetOffsetScale(string bonename)
    {
        Vector4 scale = Vector4.one;
        if(mLeaderBoneDic.ContainsKey(bonename))
        {
            scale = mLeaderBoneDic[bonename].mOffsetScale;
        }
        else
        {
            Debug.LogError("GetOffsetScale mLeaderBoneDic not contains:" + bonename);
        }
        return scale;

    }
    public void SetLeaderBonePosition(string bonename,Vector3 offset)
    {
        //mRoleJsonBoneMap[bonename].position = RootLocalToWorld(mRoleJsonBoneInitPositionMap[bonename] + offset);
        mRoleJsonBoneMap[bonename].localPosition = mRoleJsonBoneInitPositionMap[bonename] + offset;
    }
    public Vector3 GetLeaderBonePositonOffset(string bonename)
    {
        if(!mRoleJsonBoneMap.ContainsKey(bonename) || !mRoleJsonBoneInitPositionMap.ContainsKey(bonename))
        {
            Debug.LogError("DeformLeaderBoneManager mRoleJsonBoneMap mRoleJsonBoneInitPositionMap not contains key:" + bonename);
            return Vector3.zero;
        }
        else
        {
            //Vector3 offset = WorldToRootLocal(mRoleJsonBoneMap[bonename].position) - mRoleJsonBoneInitPositionMap[bonename];
            Vector3 offset = mRoleJsonBoneMap[bonename].localPosition - mRoleJsonBoneInitPositionMap[bonename];
            return offset;

        }
    }

    public void SetLeaderBoneScale(string bonename, float newScale)
    {
        Vector3 newScale3 = Vector3.one * newScale;
        if(mRoleJsonBoneMap.ContainsKey(bonename))
        {
            Transform bone = mRoleJsonBoneMap[bonename];

            Vector3 oldScale3 = bone.localScale;
            bone.localScale = newScale3;

            DeformLeaderBone lb = bone.GetComponent<DeformLeaderBone>();
            if(lb)
            {
                foreach(var interlb in lb.mInRangeLeaderBones)
                {
                    if(interlb.transform.parent != lb.transform)
                    {
                        Vector3 dir = interlb.transform.position - lb.transform.position;
                        dir =  dir / oldScale3.x * newScale;
                        interlb.transform.position = lb.transform.position + dir;
                    }
                }
                foreach(var cb in lb.mInRangeCommonBones)
                {
                    if (cb.transform.parent != lb.transform)
                    {
                        Vector3 dir = cb.transform.position - lb.transform.position;
                        dir = dir / oldScale3.x * newScale;
                        cb.transform.position = lb.transform.position + dir;

                    }
                }
            }

        }



    }
    public float GetLeaderBoneScaleOffset(string bonename)
    {
        if (!mRoleJsonBoneMap.ContainsKey(bonename))
        {
            Debug.LogError("DeformLeaderBoneManager mRoleJsonBoneMap not contains key:" + bonename);
            return 1.0f;
        }
        else
        {
            //localScale x y z equal
            float offset = mRoleJsonBoneMap[bonename].localScale.x;
            
            return offset;

        }
    }

    public void StartEdit(DeformLeaderBone editBone)
    {
        editBone.StartEdit();
    }

    public void EndEdit(DeformLeaderBone editBone)
    {
        editBone.StopEdit();
        ResetBindPose();
    }


    public void StartEdit(string boneName)
    {
        if (mLeaderBoneDic.ContainsKey(boneName))
        {
            DeformLeaderBone editlb = mLeaderBoneDic[boneName];
            StartEdit(editlb);
        }

    }

    public void StopEdit(string boneName)
    {
        if (mLeaderBoneDic.ContainsKey(boneName))
        {
            DeformLeaderBone editlb = mLeaderBoneDic[boneName];
            EndEdit(editlb);
        }

    }

    public void ResetBindPose()
    {
        foreach (DeformLeaderBone lb in mLeaderBones)
        {
            lb.ResetDefaultPosition();
        }
        foreach (DeformCommonBone cb in mCommonBones)
        {
            cb.ResetDefaultPosition();
        }

    }


    public Snapshot TakeSnapshot(string partName,string toggleName)
    {
        Snapshot ss = new Snapshot();
        ss.toggleName = toggleName;
        ss.partName = partName;
        foreach (DeformLeaderBone lb in mLeaderBones)
        {
            Snapshot.BoneData bd = new Snapshot.BoneData();
            //bd.curPos = WorldToRootLocal(lb.transform.position);
            bd.curPos = lb.transform.localPosition;
            bd.defaultPos = lb.mDefaultPosition;
            bd.curScale = lb.transform.localScale;
            bd.editing = lb.bEditing;

            ss.map[lb.name] = bd;
        }
        foreach (DeformCommonBone cb in mCommonBones)
        {
            Snapshot.BoneData bd = new Snapshot.BoneData();
            //bd.curPos = WorldToRootLocal(cb.transform.position);
            bd.curPos = cb.transform.localPosition;
            bd.defaultPos = cb.mDefaultPosition;
            bd.editing = false;

            ss.map[cb.name] = bd;
        }
        return ss;

    }
    public void RestoreSnapshot(Snapshot snapshot)
    {

        foreach (DeformLeaderBone lb in mLeaderBones)
        {
            var bd = snapshot.map[lb.name];
            lb.transform.position = RootLocalToWorld(bd.curPos);
            lb.mDefaultPosition = bd.defaultPos;
            lb.transform.localScale = bd.curScale;
            lb.bEditing = bd.editing;
            
        }
        foreach (DeformCommonBone cb in mCommonBones)
        {
            var bd = snapshot.map[cb.name];
            cb.transform.position = RootLocalToWorld(bd.curPos);
            cb.mDefaultPosition = bd.defaultPos;
        }

    }


    public Vector3 WorldToRootLocal(Vector3 world)
    {
        // return world;
        return mRootBone.parent.parent.worldToLocalMatrix * world;
    }
    public Vector3 RootLocalToWorld(Vector3 local)
    {
        // return local;
        return mRootBone.parent.parent.localToWorldMatrix * local;

    }
}
