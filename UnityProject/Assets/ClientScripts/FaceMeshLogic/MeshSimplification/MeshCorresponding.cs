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

    public MeshFilter mDebugMeshFilter;

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


        Dictionary<int, Vector3> regionVertexDict = new Dictionary<int, Vector3>();
        Dictionary<Vector3, int> regionVertexDictReverse = new Dictionary<Vector3,int>();

        for (int j = 0; j < mRegionMesh.vertexCount; j++)
        {
            Vector3 vertexRegion = mRegionMesh.vertices[j];

            if (regionVertexDict.ContainsValue(vertexRegion))
            {
                Debug.LogError(string.Format("Region Mesh Vertex Duplicate : {0} {1}", j, regionVertexDictReverse[vertexRegion]));
            }
            else
            {
                regionVertexDict[j] = vertexRegion;
                regionVertexDictReverse[vertexRegion] = j;

            }



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


    public void DrawTopology()
    {

        Vector3[] targetVertices = mTargetMesh.vertices;


        Dictionary<int, Vector3> regionVertexDict = new Dictionary<int, Vector3>();
        Dictionary<Vector3, int> regionVertexDictReverse = new Dictionary<Vector3, int>();

        List<int> DuplicateVetices = new List<int>();

        for (int j = 0; j < mRegionMesh.vertexCount; j++)
        {
            Vector3 vertexRegion = mRegionMesh.vertices[j];

            if (regionVertexDict.ContainsValue(vertexRegion))
            {
                Debug.LogError(string.Format("Region Mesh Vertex Duplicate : {0} {1}", j, regionVertexDictReverse[vertexRegion]));

                DuplicateVetices.Add(j);
            }
            else
            {
                regionVertexDict[j] = vertexRegion;
                regionVertexDictReverse[vertexRegion] = j;

            }
        }
        

        Mesh m = new Mesh();

        Vector3[] verticesRegionPos = mRegionMesh.vertices;
        Vector3[] verticesDuplicatePos = new Vector3[DuplicateVetices.Count];

        for (int i = 0; i < verticesDuplicatePos.Length; i++)
        {

            verticesDuplicatePos[i] = verticesRegionPos[DuplicateVetices[i]];

        }
        m.vertices = verticesDuplicatePos;

        int[] indices = new int[verticesDuplicatePos.Length];
        for (int i = 0; i < indices.Length; i++)
        {

            indices[i] = i;

        }

        m.SetIndices(indices, MeshTopology.Points, 0);

        mDebugMeshFilter.sharedMesh = m;

    }
}
