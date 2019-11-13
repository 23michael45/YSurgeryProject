using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class MeshCorresponding : MonoBehaviour
{
    public Mesh mRegionMesh;
    public Mesh mTargetMesh;

    public class SaveData
    {
        [SerializeField]
        public List<int> Vertices;
    }

    public void Corresponding(string jsonPath)
    {
        List<int> correspondIndices = new List<int>();

        //for (int i = 0; i < mTargetMesh.vertexCount; i++)
        //{
        //    Vector3 vertexTarget = mTargetMesh.vertices[i];

        //    for (int j = 0; j < mRegionMesh.vertexCount; j++)
        //    {

        //        Vector3 vertexRegion = mRegionMesh.vertices[j];


        //        if (vertexRegion == vertexTarget)
        //        {
        //            correspondIndices.Add(i);
        //            break;
        //        }
        //    }
        //}

        Vector3[] targetVertices = mTargetMesh.vertices;



        for (int j = 0; j < mRegionMesh.vertexCount; j++)
        {

            Vector3 vertexRegion = mRegionMesh.vertices[j];

            int index = Array.IndexOf(targetVertices, vertexRegion);
            correspondIndices.Add(index);

            Debug.Log(j);
        }

        //List<Vector3> targetList = mTargetMesh.vertices.OfType<Vector3>().ToList();
        //List<Vector3> regionList = mRegionMesh.vertices.OfType<Vector3>().ToList();

        //targetList.Intersect(regionList).ToList();

        SaveData data = new SaveData();
        data.Vertices = correspondIndices;
        string jstr = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.dataPath, jsonPath);
        File.WriteAllText(path, jstr);
    }

    public List<int> Load(string jsonPath)
    {
        string path = Path.Combine(Application.dataPath, jsonPath);
        string jstr = File.ReadAllText(path);
        
        SaveData data = new SaveData();
        data = JsonUtility.FromJson<SaveData>(jstr);

        return data.Vertices;
    }
}
