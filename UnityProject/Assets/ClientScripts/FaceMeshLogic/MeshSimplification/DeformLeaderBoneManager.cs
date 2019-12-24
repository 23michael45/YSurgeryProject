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

        public int curveIndex = 0;
    }

    [SerializeField]
    public List<LeaderBoneData> leaderBones = new List<LeaderBoneData>();

}


public class DeformLeaderBoneManager : MonoBehaviour
{
    public static DeformLeaderBoneManager Instance;
    public Transform mRootBone;

    public bool mInitFromFile = false;
    public bool mLeaderEffectByLeader = false;



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

        enabled = false;
    }
    void OnEnable()
    {
        foreach (DeformLeaderBone lb in mLeaderBones)
        {
            lb.enabled = true;
        }
        foreach (DeformCommonBone cb in mCommonBones)
        {
            cb.enabled = true;
        }

    }
    void OnDisable()
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
                lb.mCommonBones.Clear();
                lb.mLeaderBones.Clear();
                lb.mLeaderEffectByLeader = mLeaderEffectByLeader;

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
                    cb.mLeaderBones.Clear();
                }
            }

        }


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
                    lb.mCommonBones.Clear();
                    lb.mLeaderBones.Clear();
                    lb.mLeaderEffectByLeader = mLeaderEffectByLeader;

                    lb.mRange = leaderBone.range;
                    lb.mCurve = curves.curves[leaderBone.curveIndex];

                    lb.enabled = false;
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
                    cb.mLeaderBones.Clear();
                    cb.enabled = false;
                }
            }


        }
        InitBoneData();

        // DeformLeaderBoneManagerSetup setup = new DeformLeaderBoneManagerSetup();
        // setup.leaderBones.Add(new DeformLeaderBoneManagerSetup.LeaderBoneData());
        // setup.leaderBones.Add(new DeformLeaderBoneManagerSetup.LeaderBoneData());

        // string jstr = JsonUtility.ToJson(setup);
        // File.WriteAllText(Application.streamingAssetsPath + "/LeaderBoneSetup.bytes", jstr);


    }

    void InitBoneData()
    {

        foreach (DeformLeaderBone lb in mLeaderBones)
        {
            foreach (DeformLeaderBone interlb in mLeaderBones)
            {
                if (interlb != lb)
                {
                    if (Vector3.Distance(lb.transform.position, interlb.transform.position) < lb.mRange)
                    {
                        if (lb.transform.parent == interlb.transform.parent)
                        {
                            lb.mLeaderBones.Add(interlb);

                        }
                    }
                }

            }


            foreach (DeformCommonBone cb in mCommonBones)
            {
                if (Vector3.Distance(cb.transform.position, lb.transform.position) < lb.mRange)
                {
                    if (lb.transform.parent == cb.transform.parent)
                    {
                        lb.mCommonBones.Add(cb);
                        cb.mLeaderBones.Add(lb);
                    }
                }
            }
        }
    }


    public void RoleJsonInitData(SkinnedMeshRenderer skinnedmesh)
    {

        mRoleJsonBoneInitPositionMap.Clear();
        mRoleJsonBoneMap.Clear();

        Transform[] bones = skinnedmesh.bones;
        for(int i = 0; i< bones.Length;i++)
        {
            Transform bone = bones[i];
            mRoleJsonBoneInitPositionMap[bone.name] = bone.position;
            mRoleJsonBoneMap[bone.name] = bone;
        }
        
    }
    public void SetLeaderBonePosition(string bonename,Vector3 offset)
    {
        mRoleJsonBoneMap[bonename].position = mRoleJsonBoneInitPositionMap[bonename] + offset;
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
            Vector3 offset = mRoleJsonBoneMap[bonename].position - mRoleJsonBoneInitPositionMap[bonename];
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
}
