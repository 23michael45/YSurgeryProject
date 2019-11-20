using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoneOperation))]
public class BoneOperationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        BoneOperation parentObj = (BoneOperation)target;



        if (GUILayout.Button("Bone Rebind", EditorStyles.miniButtonRight))
        {
            parentObj.BoneRebind();
        }
        EditorGUILayout.Separator();




    }
}
