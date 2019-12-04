using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ModelDataManager))]
public class ModelDataManagerEditor : Editor
{
    string mHDObjFilePath = "../Model/obama53149.obj";
    string mSaveRoleJsonFilePath = "../Model/obama53149_role.json";


    string mLoadRoleJsonFilePath = "../Model/obama53149_role.json";
    string mTextureFilePath = "../Model/obamaTexture.jpg";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ModelDataManager parentObj = (ModelDataManager)target;


        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Load HD obj File Path");
        mHDObjFilePath = EditorGUILayout.TextField(mHDObjFilePath);
        EditorGUILayout.LabelField("Save LD json File Path");
        mSaveRoleJsonFilePath = EditorGUILayout.TextField(mSaveRoleJsonFilePath);


        if (GUILayout.Button("CalculateLowPolyFace", EditorStyles.miniButtonRight))
        {
            string json = parentObj.CalculateLowPolyFace(Path.Combine(Application.dataPath, mHDObjFilePath));
            File.WriteAllText(Path.Combine(Application.dataPath, mSaveRoleJsonFilePath), json);
        }

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();




        EditorGUILayout.LabelField("Load LD json File Path");
        mLoadRoleJsonFilePath = EditorGUILayout.TextField(mLoadRoleJsonFilePath);
        EditorGUILayout.LabelField("Load Texture File Path");
        mTextureFilePath = EditorGUILayout.TextField(mTextureFilePath);
        if (GUILayout.Button("LoadLowPolyFace", EditorStyles.miniButtonRight))
        {
            string roleJsonPath = Path.Combine(Application.dataPath, mLoadRoleJsonFilePath);

            string roleJson = File.ReadAllText(roleJsonPath);
            bool ret = parentObj.LoadLowPolyFace(roleJson, Path.Combine(Application.dataPath, mTextureFilePath));


        }


        EditorGUILayout.Separator();
        EditorGUILayout.Separator();


        if (GUILayout.Button("RoleJson Save Load Skinned Test", EditorStyles.miniButtonRight))
        {
            parentObj.SaveLoadJsonTest(true);
        }
        if (GUILayout.Button("RoleJson Save Load Filter Test", EditorStyles.miniButtonRight))
        {
            parentObj.SaveLoadJsonTest(false);
        }
    }
}
