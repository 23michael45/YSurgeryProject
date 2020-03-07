using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AvatarManager))]
public class AvatarManagerEditor : Editor
{
    string avatarID;
    AvatarManager.AVATARPART part;


    AnimatorOverrideController mActionController;
    AnimatorOverrideController mEmptyController;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        AvatarManager parentObj = (AvatarManager)target;


        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Avatar ID");
        avatarID = EditorGUILayout.TextField(avatarID);
        part = (AvatarManager.AVATARPART)EditorGUILayout.EnumPopup("Avatar Part",part);

        

        if (GUILayout.Button("Load", EditorStyles.miniButtonRight))
        {
            AvatarManager.Instance.StartLoadAvatar(part, avatarID);
        }


         
        mActionController = EditorGUILayout.ObjectField("Action Controller", mActionController,typeof(AnimatorOverrideController),false) as AnimatorOverrideController;

        if (GUILayout.Button("PlayAction", EditorStyles.miniButtonRight))
        {
            AvatarManager.Instance.PlayAction(mActionController);
        }
        mEmptyController = EditorGUILayout.ObjectField("Empty Controller", mEmptyController, typeof(AnimatorOverrideController), false) as AnimatorOverrideController;
        if (GUILayout.Button("ClearAction", EditorStyles.miniButtonRight))
        {
            AvatarManager.Instance.ClearAction();
        }
    }
}
