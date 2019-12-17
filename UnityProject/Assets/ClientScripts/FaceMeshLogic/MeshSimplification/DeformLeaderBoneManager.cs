using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeformLeaderBoneManager : MonoBehaviour
{
    public Transform mRootBone;

    

    [NonSerialized]
    public List<DeformLeaderBone> mLeaderBones = new List<DeformLeaderBone>();
    [NonSerialized]
    public List<DeformCommonBone> mCommonBones = new List<DeformCommonBone>();



    private void Start()
    {
        Init();
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




        foreach (DeformLeaderBone lb in mLeaderBones)
        {

            foreach (DeformCommonBone cb in mCommonBones)
            {
                if(Vector3.Distance(cb.transform.position,lb.transform.position) < lb.mRange)
                {
                    lb.mCommonBones.Add(cb);
                    cb.mLeaderBones.Add(lb);
                }
            }
        }

    }
}
