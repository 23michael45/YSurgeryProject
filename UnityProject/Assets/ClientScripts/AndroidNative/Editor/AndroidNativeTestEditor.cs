using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AndroidNativeTest))]
public class AndroidNativeTestEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        AndroidNativeTest parentObj = (AndroidNativeTest)target;

        if (GUILayout.Button("Java Calculate", EditorStyles.miniButtonRight))
        {
            parentObj.TestCallJavaCalculate();
        }

        if (GUILayout.Button("Java Load", EditorStyles.miniButtonRight))
        {
            parentObj.TestCallJavaLoad();
        }
    }
}
