using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeformBaseBone : MonoBehaviour
{
    [NonSerialized]
    public Vector3 mDefaultPosition;

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
            float dist = Vector3.Distance(lb.mDefaultPosition, mDefaultPosition);

            float portion = dist / lb.mRange;

            float weight = lb.mCurve.Evaluate(portion);

            offsetPos += (DeformLeaderBoneManager.Instance.WorldToRootLocal(lb.transform.position) - lb.mDefaultPosition) * weight;
        }
        try
        {
            transform.position = DeformLeaderBoneManager.Instance.RootLocalToWorld(mDefaultPosition + offsetPos);

        }
        catch
        {
            Debug.LogError("CalculatePosition error:" + gameObject.name);
        }
    }



    protected virtual void Update()
    {
        Calculate();
    }


    public void ResetDefaultPosition()
    {

        mDefaultPosition = DeformLeaderBoneManager.Instance.WorldToRootLocal(transform.position);
    }


    public virtual void Calculate()
    {
    }
}
