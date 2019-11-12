using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SimplifyFaceModel))]
public class MeshSimplificationEditor : Editor
{
    string mJsonPath = "";
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SimplifyFaceModel parentObj = (SimplifyFaceModel)target;


        EditorGUILayout.Separator();

        if (GUILayout.Button("Corresponding Vertices", EditorStyles.miniButtonRight))
        {
            parentObj.CalculateVerticesCorrespondingRelation();
        }

        EditorGUILayout.Separator();
        if (GUILayout.Button("Deformed LD Face", EditorStyles.miniButtonRight))
        {
            parentObj.CalculateDeformedMeshLD();
        }


        EditorGUILayout.Separator();


        EditorGUILayout.LabelField("Save Corresponding Json Path");
        EditorGUI.indentLevel++;
        mJsonPath = EditorGUILayout.TextField(mJsonPath);
        if (GUILayout.Button("Save Corresponding Json", EditorStyles.miniButtonRight))
        {
            parentObj.SaveJson(mJsonPath);
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.Separator();


    }
}
