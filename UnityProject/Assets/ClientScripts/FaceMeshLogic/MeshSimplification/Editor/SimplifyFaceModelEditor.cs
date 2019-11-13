using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SimplifyFaceModel))]
public class MeshSimplificationEditor : Editor
{
    string mRegionJsonPath = "../correspondingRegionIndices.json";
    string mSaveLD2HDJsonPath = "../correspondingHDLDIndices.json";

    string mLoadLD2HDJsonPath = "../correspondingHDLDIndices.json";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SimplifyFaceModel parentObj = (SimplifyFaceModel)target;


        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Save Corresponding Json Path");
        mSaveLD2HDJsonPath = EditorGUILayout.TextField(mSaveLD2HDJsonPath);

        //EditorGUILayout.Separator();

        //if (GUILayout.Button("Corresponding Vertices", EditorStyles.miniButtonRight))
        //{
        //    parentObj.CalculateVerticesCorrespondingRelation(mSaveLD2HDJsonPath);
        //}

        //EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Load Region Corresponding Json Path");
        mRegionJsonPath = EditorGUILayout.TextField(mRegionJsonPath);
        if (GUILayout.Button("Corresponding Vertices HD LD Json", EditorStyles.miniButtonRight))
        {
            parentObj.CalculateHDLDCorresponding(mSaveLD2HDJsonPath, mRegionJsonPath);
        }


        EditorGUILayout.Separator();


        EditorGUILayout.LabelField("Load Corresponding Json Path");
        mLoadLD2HDJsonPath = EditorGUILayout.TextField(mLoadLD2HDJsonPath);
        if (GUILayout.Button("Deformed LD Face", EditorStyles.miniButtonRight))
        {
            parentObj.CalculateDeformedMeshLD(mLoadLD2HDJsonPath);
        }
        if (GUILayout.Button("Draw Topology", EditorStyles.miniButtonRight))
        {
            parentObj.DrawTopology(mLoadLD2HDJsonPath);
        }

        EditorGUILayout.Separator();




    }
}
