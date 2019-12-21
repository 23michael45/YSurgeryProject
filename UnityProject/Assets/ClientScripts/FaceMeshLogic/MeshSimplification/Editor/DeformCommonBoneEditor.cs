using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(DeformCommonBone))]
public class DeformCommonBoneEditor : Editor
{
    DeformCommonBone parentObj;
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();


        parentObj = (DeformCommonBone)target;
        if (GUILayout.Button("Print Debug", EditorStyles.miniButtonRight))
        {
            parentObj.PrintDebug();
        }

    }
}
