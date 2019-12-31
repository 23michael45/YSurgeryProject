using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SimplifyFaceModel))]
public class MeshSimplificationEditor : Editor
{
    string mRegionJsonPath = "StreamingAssets/Json/correspondingRegionIndices.json";
    string mSaveLD2HDJsonPath = "StreamingAssets/Json/correspondingHDLDIndices.json";

    string mSaveBoneIndexJsonPath = "StreamingAssets/Json/boneIndexMap.json";

    string mLoadLD2HDJsonPath = "StreamingAssets/Json/correspondingHDLDIndices.json";
    string mLoadBoneIndexJsonPath = "StreamingAssets/Json/boneIndexMap.json";

    string mSaveMeshPath = "../lowMeshWithUV.fbx";

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

        EditorGUILayout.LabelField("Save Corresponding Bone Index Path");
        mSaveBoneIndexJsonPath = EditorGUILayout.TextField(mSaveBoneIndexJsonPath);
        if (GUILayout.Button("Corresponding Bone 2 HD IndexJson", EditorStyles.miniButtonRight))
        {
            parentObj.CalculateBoneCorresponding(mSaveBoneIndexJsonPath);
        }


        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Save Corresponding Low Mesh with UV From High Mesh");
        mSaveMeshPath = EditorGUILayout.TextField(mSaveMeshPath);
        if (GUILayout.Button("Corresponding Low Mesh with UV From High Mesh", EditorStyles.miniButtonRight))
        {
            parentObj.CalculateCorrespondingLowMeshUVFromHighMesh(mSaveMeshPath);
        }

        EditorGUILayout.Separator();



        EditorGUILayout.LabelField("Load Corresponding Json Path");
        mLoadLD2HDJsonPath = EditorGUILayout.TextField(mLoadLD2HDJsonPath);
        if (GUILayout.Button("Deformed LD Face", EditorStyles.miniButtonRight))
        {
            parentObj.CalculateDeformedMeshLD(mLoadLD2HDJsonPath);
        }
        if (GUILayout.Button("Draw Low Topology", EditorStyles.miniButtonRight))
        {
            parentObj.DrawLowTopology(mLoadLD2HDJsonPath);
        }
        if (GUILayout.Button("Draw High Topology", EditorStyles.miniButtonRight))
        {
            parentObj.DrawHighTopology(mLoadLD2HDJsonPath);
        }
        if (GUILayout.Button("Draw Bones In FaceRegion Topology", EditorStyles.miniButtonRight))
        {
            parentObj.DrawFaceRegionBones(mLoadBoneIndexJsonPath);
        }
        EditorGUILayout.Separator();




    }
}
