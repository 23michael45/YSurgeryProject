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
    public Transform mRootBone;

    public bool mInitFromFile = false;
    public bool mLeaderEffectByLeader = false;

    [NonSerialized]
    public List<DeformLeaderBone> mLeaderBones = new List<DeformLeaderBone>();
    [NonSerialized]
    public List<DeformCommonBone> mCommonBones = new List<DeformCommonBone>();

    DeformLeaderBone mCurrentEditBone;


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
    }

    void Init()
    {
        mLeaderBones.Clear();
        mCommonBones.Clear();

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
                    lb.mCommonBones.Clear();
                    lb.mLeaderBones.Clear();
                    lb.mLeaderEffectByLeader = mLeaderEffectByLeader;

                    lb.mRange = leaderBone.range;
                    lb.mCurve = curves.curves[leaderBone.curveIndex];

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
                        lb.mLeaderBones.Add(interlb);
                    }
                }

            }


            foreach (DeformCommonBone cb in mCommonBones)
            {
                if (Vector3.Distance(cb.transform.position, lb.transform.position) < lb.mRange)
                {
                    lb.mCommonBones.Add(cb);
                    cb.mLeaderBones.Add(lb);
                }
            }
        }
    }

    public void StartEdit(DeformLeaderBone editBone)
    {
        mCurrentEditBone = editBone;
        mCurrentEditBone.StartEdit();
    }

    public void EndEdit(DeformLeaderBone editBone)
    {
        if (editBone != mCurrentEditBone)
        {
            Debug.LogError("End Edit Bone not same");
        }
        mCurrentEditBone.StopEdit();

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
