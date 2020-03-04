using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ModelDataManager))]
public class ModelDataManagerEditor : Editor
{

    string mPhotoMainName = "obama";




    string mBakeMeshPath = "../Model/bakeMesh.obj";
    bool mGender = false;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ModelDataManager parentObj = (ModelDataManager)target;


        mPhotoMainName = EditorGUILayout.TextField(mPhotoMainName);
        string mHDObjFilePath = string.Format("../Model/{0}53149.obj",mPhotoMainName);
        string mSaveRoleJsonFilePath = string.Format("../Model/{0}53149_role.json", mPhotoMainName);
        string mLoadRoleJsonFilePath = string.Format("../Model/{0}53149_role.json", mPhotoMainName);
        string mTextureFilePath = string.Format("../Model/{0}53149Texture.jpg", mPhotoMainName);
        string mSaveDeformFilePath = string.Format("../Model/{0}53149_deform.json", mPhotoMainName);
        string mLoadDeformFilePath = string.Format("../Model/{0}53149_deform.json", mPhotoMainName);

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Load HD obj File Path");
        EditorGUILayout.LabelField(mHDObjFilePath);
        EditorGUILayout.LabelField("Save LD json File Path");
        EditorGUILayout.LabelField(mSaveRoleJsonFilePath);

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
        EditorGUILayout.LabelField(mLoadRoleJsonFilePath);
        EditorGUILayout.LabelField("Load Texture File Path");
        EditorGUILayout.LabelField(mTextureFilePath);
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
        EditorGUILayout.LabelField(mSaveDeformFilePath);
        if (GUILayout.Button("Save Deform", EditorStyles.miniButtonRight))
        {
            string deformJsonPath = Path.Combine(Application.dataPath, mSaveDeformFilePath);

            string deformJson = parentObj.SaveDeform();


            File.WriteAllText(deformJsonPath,deformJson);
        }

        EditorGUILayout.LabelField("Load Deform File Path");
        EditorGUILayout.LabelField(mLoadDeformFilePath);
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
