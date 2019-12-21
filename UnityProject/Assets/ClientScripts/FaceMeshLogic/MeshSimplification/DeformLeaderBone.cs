using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeformLeaderBone : DeformBaseBone
{
    public float mRange;
    [NonSerialized]
    public bool mLeaderEffectByLeader;
    public AnimationCurve mCurve;

    [NonSerialized]
    public List<DeformCommonBone> mCommonBones = new List<DeformCommonBone>();


    private bool bEditing = false;


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
        if (!bEditing && mLeaderEffectByLeader)
        {
            CalculatePosition();

        }
    }
}
