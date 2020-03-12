using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeformBaseBone : MonoBehaviour
{
    [NonSerialized]
    public Vector3 mDefaultPositionToHead;

    public List<DeformLeaderBone> mPositionFromLeaderBones = new List<DeformLeaderBone>();

    public void Awake()
    {
        ResetDefaultPosition();
    }

    protected virtual void CalculatePosition()
    {
        Vector3 offsetPos = Vector3.zero;

        foreach (DeformLeaderBone lb in mPositionFromLeaderBones)
        {
            float dist = Vector3.Distance(lb.mDefaultPositionToHead, mDefaultPositionToHead);

            float portion = dist / lb.mRange;

            float weight = lb.mCurve.Evaluate(portion);

            //offsetPos += (DeformLeaderBoneManager.Instance.WorldToRootLocal(lb.transform.position) - lb.mDefaultPosition) * weight;
            offsetPos += (DeformLeaderBoneManager.Instance.WorldToHeadRootTransformPoint(lb.transform.position) - lb.mDefaultPositionToHead) * weight;
        }
        try
        {
            //transform.position = DeformLeaderBoneManager.Instance.RootLocalToWorld(mDefaultPosition + offsetPos);
            transform.position = DeformLeaderBoneManager.Instance.HeadRootToWorldTransformPoint(mDefaultPositionToHead + offsetPos);
        } 
        catch
        {
            Debug.LogError("CalculatePosition error:" + gameObject.name);
        }
    }



    protected virtual void Update()
    {
        if(gameObject.name.Contains("face"))
        {

            Calculate();
        }
        else
        {

        }
    }


    public void ResetDefaultPosition()
    {

        //mDefaultPosition = DeformLeaderBoneManager.Instance.WorldToRootLocal(transform.position);
        mDefaultPositionToHead = DeformLeaderBoneManager.Instance.WorldToHeadRootTransformPoint(transform.position);
    }


    public virtual void Calculate()
    {
    }
}
