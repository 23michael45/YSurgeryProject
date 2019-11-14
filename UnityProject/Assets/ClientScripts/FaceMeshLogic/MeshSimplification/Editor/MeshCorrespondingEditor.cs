using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeshCorresponding))]
public class MeshCorrespondingEditor : Editor
{
    string mJsonPath = "../correspondingRegionIndices.json";
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MeshCorresponding parentObj = (MeshCorresponding)target;

        
        EditorGUILayout.Separator();


        EditorGUILayout.LabelField("Save Corresponding Json Path");
        mJsonPath = EditorGUILayout.TextField(mJsonPath);
        EditorGUILayout.Separator();



        EditorGUILayout.Separator();

        if (GUILayout.Button("Corresponding Vertices", EditorStyles.miniButtonRight))
        {
            parentObj.Corresponding(mJsonPath);
        }

        if (GUILayout.Button("Draw Duplicate Topology", EditorStyles.miniButtonRight))
        {
            parentObj.DrawDuplicateTopology();

        }
        if (GUILayout.Button("Draw Lost Topology", EditorStyles.miniButtonRight))
        {
            parentObj.DrawLostInFullTopology();

        }

        if (GUILayout.Button("Show Mesh", EditorStyles.miniButtonRight))
        {
            parentObj.ShowRegionMesh();

        }
    }
}
