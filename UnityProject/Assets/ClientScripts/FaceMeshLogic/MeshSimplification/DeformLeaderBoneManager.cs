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
    Dictionary<string, DeformLeaderBone> mLeaderBoneDic = new Dictionary<string, DeformLeaderBone>();

    [NonSerialized]
    Dictionary<string, Vector3> mRoleJsonBoneInitPositionMap = new Dictionary<string, Vector3>();
    Dictionary<string, Transform> mRoleJsonBoneMap = new Dictionary<string, Transform>();


    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (mRootBone == null)
        {
            mRootBone = transform;
        }
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
    void SetWorking(bool b)
    {
        foreach (DeformLeaderBone lb in mLeaderBones)
        {
            lb.enabled = false;
        }
        foreach (DeformCommonBone cb in mCommonBones)
        {
            cb.enabled = false;
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
        DeformLeaderBoneManagerSetup setup = JsonUtility.FromJson<DeformLeaderBoneManagerSetup>(textAsset.text);

        Debug.Log(setup.leaderBones.Count);



        Transform[] bones = mRootBone.GetComponentsInChildren<Transform>();
        foreach (Transform bone in bones)
        {
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
            foreach (var leaderBone in setup.leaderBones)
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
                        {
                            lb.mInRangeLeaderBones.Add(interlb);

                        }
                    }
                    if (Vector3.Distance(lb.transform.position, interlb.transform.position) < interlb.mRange)
                    {
                        //if (lb.transform.parent == interlb.transform.parent)
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
            mRoleJsonBoneInitPositionMap[bone.name] = bone.position;
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
        mRoleJsonBoneMap[bonename].position = RootLocalToWorld(mRoleJsonBoneInitPositionMap[bonename] + offset);
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
            Vector3 offset = WorldToRootLocal(mRoleJsonBoneMap[bonename].position) - mRoleJsonBoneInitPositionMap[bonename];
            
            return offset;

        }
    }

    public void SetLeaderBoneScale(string bonename, float newScale)
    {
        Vector3 newScale3 = Vector3.one * newScale;
        mRoleJsonBoneMap[bonename].localScale = newScale3;
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


    public Snapshot TakeSnapshot(string toggleName)
    {
        Snapshot ss = new Snapshot();
        ss.toggleName = toggleName;
        foreach (DeformLeaderBone lb in mLeaderBones)
        {
            Snapshot.BoneData bd = new Snapshot.BoneData();
            bd.curPos = WorldToRootLocal(lb.transform.position);
            bd.defaultPos = lb.mDefaultPosition;
            bd.curScale = lb.transform.localScale;
            bd.editing = lb.bEditing;

            ss.map[lb.name] = bd;
        }
        foreach (DeformCommonBone cb in mCommonBones)
        {
            Snapshot.BoneData bd = new Snapshot.BoneData();
            bd.curPos = WorldToRootLocal(cb.transform.position);
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
