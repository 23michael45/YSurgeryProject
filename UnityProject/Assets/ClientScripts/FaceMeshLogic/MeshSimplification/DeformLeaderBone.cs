using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnlyAttribute : PropertyAttribute
{
}
public class DeformLeaderBone : DeformBaseBone
{
    public float mRange;
    public Vector4 mOffsetScale;
    public AnimationCurve mCurve;

    public bool mShowPositionFromLeaderBones = false;
    public bool mSelectedDirectly = false;



    [ShowOnly]
    public bool bEditing = false;
    [ShowOnly]
    public bool mAffectedByLeader;
    
    public List<DeformCommonBone> mInRangeCommonBones = new List<DeformCommonBone>();
    public List<DeformLeaderBone> mInRangeLeaderBones = new List<DeformLeaderBone>();

    public List<string> mExcludeBones = new List<string>();

    public void StartEdit()
    {
        bEditing = true;
    }
    public void StopEdit()
    {
        bEditing = false;
    }

    public override void Calculate()
    {
        if (!bEditing && mAffectedByLeader)
        {
            CalculatePosition();

        }
    }


    protected override void CalculatePosition()
    {
        Vector3 offsetPos = Vector3.zero;

        foreach (DeformLeaderBone lb in mPositionFromLeaderBones)
        {
            if(lb.bEditing == true)
            {

                float dist = Vector3.Distance(lb.mDefaultPositionToHead, mDefaultPositionToHead);

                float portion = dist / lb.mRange;

                float weight = lb.mCurve.Evaluate(portion);

                //offsetPos += (DeformLeaderBoneManager.Instance.WorldToRootLocal(lb.transform.position) - lb.mDefaultPosition) * weight;
                offsetPos += (DeformLeaderBoneManager.Instance.WorldToHeadRootTransformPoint(lb.transform.position) - lb.mDefaultPositionToHead) * weight;

                Debug.Log(offsetPos);
            }
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

    void OnDrawGizmosSelected()
    {
        if (mSelectedDirectly)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, mRange);

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 5);

            Gizmos.color = Color.blue;
            foreach (var commonBone in mInRangeCommonBones)
            {
                Gizmos.DrawSphere(commonBone.transform.position, 5);
            }
            Gizmos.color = Color.red;
            foreach (var commonBone in mInRangeLeaderBones)
            {
                Gizmos.DrawSphere(commonBone.transform.position, 5);
            }


            if (mShowPositionFromLeaderBones)
            {
                Gizmos.color = Color.green;
                foreach (var leaderBone in mPositionFromLeaderBones)
                {
                    Gizmos.DrawSphere(leaderBone.transform.position, 5);
                }
            }
        }
    }
}
