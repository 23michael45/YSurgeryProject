using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

//因为Unity 自带的Obj Import会把Obj文件的x 反向变为-x，所以要自定义一下obj file的读取

[CustomEditor(typeof(LoadObjFile))]
public class LoadObjFileEditor : Editor
{

    bool mFlipX = false;
    string mObjFilePath = "../Model/obama53149.obj";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LoadObjFile parentObj = (LoadObjFile)target;
        EditorGUILayout.LabelField("Load obj File Path");
        mObjFilePath = EditorGUILayout.TextField(mObjFilePath);
        EditorGUILayout.LabelField("Is X Flip");
        mFlipX = EditorGUILayout.Toggle(mFlipX);

        if (GUILayout.Button("Load", EditorStyles.miniButtonRight))
        {
            string fullPath = Path.Combine(Application.dataPath, mObjFilePath);

            parentObj.Load(fullPath, mFlipX);
        }
    }
}
