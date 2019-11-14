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
    public Transform mDuplicateContainer;
    public GameObject mPointPrefab;

    public class SaveData
    {
        [SerializeField]
        public List<int> Vertices;
    }

    public void Corresponding(string jsonPath)
    {
        List<int> correspondIndices = new List<int>();

        Vector3[] targetVertices = mTargetMesh.vertices;


        Dictionary<int, Vector3> regionVertexDict = new Dictionary<int, Vector3>();
        Dictionary<Vector3, int> regionVertexDictReverse = new Dictionary<Vector3,int>();

        for (int j = 0; j < mRegionMesh.vertexCount; j++)
        {
            Vector3 vertexRegion = mRegionMesh.vertices[j];


            //处理点位置重复的情况
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

            if(index == -1)
            {
                float minDist = float.MaxValue;
                int minFullIndex = -1;
                for (int highIndex = 0; highIndex < targetVertices.Length; highIndex++)
                {
                    Vector3 fullPos = targetVertices[highIndex];
                    
                    float dist = Vector3.Distance(fullPos, vertexRegion);
                    
                    if (minDist > dist)
                    {
                        minDist = dist;
                        minFullIndex = highIndex;
                    }

                }

                index = minFullIndex;
            }


            correspondIndices.Add(index);
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

    public void DrawLostInFullTopology()
    {
        Mesh srcMesh = mRegionMesh;

        List<int> LostVetices = new List<int>();

        Vector3[] targetVertices = mTargetMesh.vertices;

        for (int j = 0; j < mRegionMesh.vertexCount; j++)
        {
            Vector3 vertexRegion = mRegionMesh.vertices[j];

            int index = Array.IndexOf(targetVertices, vertexRegion);

            if (index == -1)
            {
                LostVetices.Add(j);
            }
        }

        Mesh m = new Mesh();

        Vector3[] verticesRegionPos = srcMesh.vertices;
        Vector3[] verticesLostPos = new Vector3[LostVetices.Count];

        for (int i = 0; i < verticesLostPos.Length; i++)
        {
            verticesLostPos[i] = verticesRegionPos[LostVetices[i]];
        }
        m.vertices = verticesLostPos;

        int[] indices = new int[verticesLostPos.Length];
        for (int i = 0; i < indices.Length; i++)
        {
            indices[i] = i;
        }

        m.SetIndices(indices, MeshTopology.Points, 0);

        mDebugMeshFilter.sharedMesh = m;

    }
    public void DrawDuplicateTopology()
    {


        ClearContainer();

        //Mesh srcMesh = mTargetMesh;
        Mesh srcMesh = mRegionMesh;

        Vector3[] targetVertices = mTargetMesh.vertices;


        Dictionary<int, Vector3> regionVertexDict = new Dictionary<int, Vector3>();
        Dictionary<Vector3, int> regionVertexDictReverse = new Dictionary<Vector3, int>();

        List<int> DuplicateVetices = new List<int>();

        for (int j = 0; j < srcMesh.vertexCount; j++)
        {
            Vector3 vertexRegion = srcMesh.vertices[j];

            if (regionVertexDict.ContainsValue(vertexRegion))
            {
                Debug.LogError(string.Format("Region Mesh Vertex Duplicate : {0} {1}", j, regionVertexDictReverse[vertexRegion]));

                DuplicateVetices.Add(j);

                CreatePoint(j, vertexRegion);
            }
            else
            {
                regionVertexDict[j] = vertexRegion;
                regionVertexDictReverse[vertexRegion] = j;

            }
        }
        

        Mesh m = new Mesh();

        Vector3[] verticesRegionPos = srcMesh.vertices;
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


    void CreatePoint(int index,Vector3 pos)
    {
        GameObject gonew = Instantiate(mPointPrefab);
        gonew.transform.parent = mDuplicateContainer;
        gonew.name = index.ToString();
        gonew.transform.localPosition = pos;
        gonew.SetActive(true);
    }

    public void ClearContainer()
    {
        var components = mDuplicateContainer.GetComponentsInChildren<Transform>();
        for(int i  = 0; i < components.Length;i++)
        {
            if(components[i].gameObject != mDuplicateContainer.gameObject)
            {
                GameObject.DestroyImmediate(components[i].gameObject);

            }
        }
    }

    public void ShowRegionMesh()
    {
        ClearContainer();
        for (int j = 0; j < mRegionMesh.vertexCount; j++)
        {
            Vector3 vertexRegion = mRegionMesh.vertices[j];
            CreatePoint(j,vertexRegion);
        }
    }
}
