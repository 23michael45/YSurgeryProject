using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ModelDataManager))]
public class ModelDataManagerEditor : Editor
{

    string mPhotoName = "obama53149";

    string mHDObjFilePath = "../Model/obama53149.obj";
    string mSaveRoleJsonFilePath = "../Model/obama53149_role.json";


    string mLoadRoleJsonFilePath = "../Model/obama53149_role.json";
    string mTextureFilePath = "../Model/obama53149Texture.jpg";


    string mSaveDeformFilePath = "../Model/obama53149_deform.json";
    string mLoadDeformFilePath = "../Model/obama53149_deform.json";


    string mBakeMeshPath = "../Model/bakeMesh.obj";
    bool mGender = false;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ModelDataManager parentObj = (ModelDataManager)target;


        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Load HD obj File Path");
        mHDObjFilePath = EditorGUILayout.TextField(mHDObjFilePath);
        EditorGUILayout.LabelField("Save LD json File Path");
        mSaveRoleJsonFilePath = EditorGUILayout.TextField(mSaveRoleJsonFilePath);

        EditorGUILayout.LabelField("Is Male or Female");
        mGender = EditorGUILayout.Toggle(mGender);


        if (GUILayout.Button("CalculateLowPolyFace", EditorStyles.miniButtonRight))
        {
            byte[] objData = File.ReadAllBytes(Path.Combine(Application.dataPath, mHDObjFilePath));

            string json = parentObj.CalculateLowPolyFace(objData, mGender?0:1, 180,75,null);

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

            string texPath = Path.Combine(Application.dataPath, mTextureFilePath);
            byte[] byteArray = File.ReadAllBytes(texPath);
            Texture2D tex = new Texture2D(2, 2);
            bool isLoaded = tex.LoadImage(byteArray);
            if(isLoaded)
            {
                bool ret = parentObj.LoadLowPolyFace(roleJson,tex);

            }


        }


        EditorGUILayout.Separator();
        EditorGUILayout.Separator();


        EditorGUILayout.LabelField("Save Deform File Path");
        mSaveDeformFilePath = EditorGUILayout.TextField(mSaveDeformFilePath);
        if (GUILayout.Button("Save Deform", EditorStyles.miniButtonRight))
        {
            string deformJsonPath = Path.Combine(Application.dataPath, mSaveDeformFilePath);

            string deformJson = parentObj.SaveDeform();


            File.WriteAllText(deformJsonPath,deformJson);
        }

        EditorGUILayout.LabelField("Load Deform File Path");
        mLoadDeformFilePath = EditorGUILayout.TextField(mLoadDeformFilePath);
        if (GUILayout.Button("Load Deform", EditorStyles.miniButtonRight))
        {
            string deformJsonPath = Path.Combine(Application.dataPath, mLoadDeformFilePath);

            string deformJson = File.ReadAllText(deformJsonPath);
            bool b = parentObj.LoadDeform(deformJson);
            
        }


        if (GUILayout.Button("RoleJson Save Load Skinned Test", EditorStyles.miniButtonRight))
        {
            parentObj.SaveLoadJsonTest(true);
        }
        if (GUILayout.Button("RoleJson Save Load Filter Test", EditorStyles.miniButtonRight))
        {
            parentObj.SaveLoadJsonTest(false);
        }
        if (GUILayout.Button("Rebind Bone Test", EditorStyles.miniButtonRight))
        {
            parentObj.RebindBone();
        }

        if (GUILayout.Button("EnterEditMode", EditorStyles.miniButtonRight))
        {
            ViewUI.Instance.EditButton_clk();
        }
        if (GUILayout.Button("ExitEditMode", EditorStyles.miniButtonRight))
        {
            ActiveScene.Instance.BackFirstpage();
        }

        if (GUILayout.Button("Reset Bone and Vertex Data to Mean Face", EditorStyles.miniButtonRight))
        {
            parentObj.ResetBoneInitData();
        }
        if (GUILayout.Button("Test Animation Default", EditorStyles.miniButtonRight))
        {
            parentObj.PlayAnimation("Default");
        }


        EditorGUILayout.LabelField("Bake Skinned Mesh Path");
        mBakeMeshPath = EditorGUILayout.TextField(mBakeMeshPath);
        if (GUILayout.Button("Test Bake Mesh", EditorStyles.miniButtonRight))
        {
            string bakePath = Path.Combine(Application.dataPath, mBakeMeshPath);
            //parentObj.BakeSkinnedMesh();
        }
    }
}
