using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeformCommonBone : MonoBehaviour
{
    public List<DeformLeaderBone> mLeaderBones = new List<DeformLeaderBone>();

    [NonSerialized]
    public Vector3 mDefaultPosition;

    public void Awake()
    {
        mDefaultPosition = transform.position;
    }

    public void CalculatePosition()
    {
        Vector3 offsetPos = Vector3.zero;

        foreach(DeformLeaderBone lb in mLeaderBones)
        {
            float dist = Vector3.Distance(lb.mDefaultPosition, mDefaultPosition);

            float portion = dist / lb.mRange;

            float weight = lb.mCurve.Evaluate(portion);

            offsetPos += (lb.transform.position - lb.mDefaultPosition) * weight;
        }

        transform.position = mDefaultPosition + offsetPos;
    }

    private void Update()
    {
        CalculatePosition();
    }
}
