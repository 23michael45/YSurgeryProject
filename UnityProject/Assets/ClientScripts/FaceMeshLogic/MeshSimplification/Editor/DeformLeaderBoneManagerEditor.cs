using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DeformLeaderBoneManager))]
public class DeformLeaderBoneManagerEditor : Editor
{

    static DeformLeaderBone editLeaderBone;
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        DeformLeaderBoneManager parentObj = (DeformLeaderBoneManager)target;



        EditorGUILayout.LabelField("Leader Bones:");

        DeformLeaderBone[] leaderBones = parentObj.GetComponentsInChildren<DeformLeaderBone>();

        EditorGUI.indentLevel++;
        foreach (var leaderBone in leaderBones)
        {
            var bone = EditorGUILayout.ObjectField(leaderBone.name, leaderBone, typeof(Transform), true) as Transform;
        }

        EditorGUI.indentLevel--;


        editLeaderBone = EditorGUILayout.ObjectField("Editing Leader Bone", editLeaderBone, typeof(DeformLeaderBone), true) as DeformLeaderBone;

        if (GUILayout.Button("Start Edit", EditorStyles.miniButtonRight))
        {
            parentObj.StartEdit(editLeaderBone);

        }
        if (GUILayout.Button("Stop Edit", EditorStyles.miniButtonRight))
        {
            parentObj.EndEdit(editLeaderBone);
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Reinit Bone Data", EditorStyles.miniButtonRight))
        {
            parentObj.ReinitBoneData();

        }
    }
}
