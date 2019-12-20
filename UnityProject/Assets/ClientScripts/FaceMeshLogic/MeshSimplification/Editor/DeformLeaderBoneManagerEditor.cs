using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DeformLeaderBoneManager))]
public class DeformLeaderBoneManagerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        
        base.OnInspectorGUI();
        DeformLeaderBoneManager parentObj = (DeformLeaderBoneManager)target;    



        EditorGUILayout.LabelField("Leader Bones:");

        DeformLeaderBone[] leaderBones = parentObj.GetComponentsInChildren<DeformLeaderBone>();

        EditorGUI.indentLevel ++;
        foreach(var leaderBone in leaderBones)
        {
             var bone = EditorGUILayout.ObjectField(leaderBone.name, leaderBone, typeof(Transform), true) as Transform;
        }

        EditorGUI.indentLevel --;
    }
}
