using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeformLeaderBone : MonoBehaviour
{
    public float mRange;
    public AnimationCurve mCurve;

    [NonSerialized]
    public List<DeformCommonBone> mCommonBones = new List<DeformCommonBone>();

    [NonSerialized]
    public Vector3 mDefaultPosition;
    // Start is called before the first frame update
    void Awake()
    {
        mDefaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
