using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeshInfoSelector))]
public class MeshInfoSelectorEditor : Editor
{
    MeshInfoSelector parentObj;

    GameObject meshSource;

    private void OnEnable()
    {
        parentObj = (MeshInfoSelector)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        EditorGUILayout.LabelField("Mesh Source");
        meshSource = (GameObject)EditorGUILayout.ObjectField(meshSource, typeof(GameObject), true);
        if (GUILayout.Button("Load Mesh From Source", EditorStyles.miniButtonRight))
        {
            parentObj.LoadMeshFromSource(meshSource);
        }
    }


    public void OnSceneGUI()
    {
        Event e = Event.current;

        //Check the event type and make sure it's left click.
        if (e.type == EventType.MouseDown)
        {
            Vector2 guiPosition = Event.current.mousePosition;
            Ray ray = HandleUtility.GUIPointToWorldRay(guiPosition);

            parentObj.PickVertex(ray);



         
            e.Use();  //Eat the event so it doesn't propagate through the editor.
        }

        //Handles.RectangleHandleCap(0, parentObj.mSelectVertex, Quaternion.identity, 5, EventType.Repaint);
    }
}


