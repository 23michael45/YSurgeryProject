using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeformCommonBone : DeformBaseBone
{

    public override void Calculate()
    {
        CalculatePosition();
    }

    public void PrintDebug()
    {
        Debug.Log(string.Format("DeformCommonBone : {0}  {1}  {2}", gameObject.name, transform.position, mDefaultPosition));
    }
}
