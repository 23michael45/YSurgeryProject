using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DeformLeaderBone))]
public class DeformLeaderBoneEditor : Editor
{
    
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        DeformLeaderBone parentObj = (DeformLeaderBone)target;

        

    }
}
