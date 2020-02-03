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
        
    }
}
