using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class SimplifyFaceModel : MonoBehaviour
{
    [Serializable]
    public class item
    {
        public int index;
        public int highIndex;
        public int lowIndex;
    }
    [Serializable]
    public class HLVertexMap
    {
        public item[] items;
    }


    public float m_Scale = 0.001f;

    //Used to calculate Vertices Relation
    public Transform m_HDMeanFaceMesh;
    public Transform m_LDMeanFaceMesh;

    //Used to calculate Deformed Low Mesh from High Deformed Mesh Vertices Position
    public Transform m_HDDeformedFaceMesh;
    public Transform m_LDDeformedFaceMesh;

    Mesh m_TargetMesh;
    

    public bool m_bUseRaycast = false;
    
    public void CalculateDeformedMeshLD(string loadPath)
    {
        Dictionary<int, int> l2hDict;
        LoadJson(loadPath,out l2hDict);

        Vector3[] DeformedVertices = m_HDDeformedFaceMesh.gameObject.GetComponent<MeshFilter>().mesh.vertices;
        Vector2[] DeformedUVs = m_HDDeformedFaceMesh.gameObject.GetComponent<MeshFilter>().mesh.uv;
        //Vector3[] TargetSizeVertices = m_HighMeshTransform.gameObject.GetComponent<MeshFilter>().sharedMesh.vertices;
        Vector3[] lowVertices = m_LDMeanFaceMesh.gameObject.GetComponent<MeshFilter>().mesh.vertices;
        Vector2[] lowUVs = new Vector2[lowVertices.Length];


        float lowMeshScale = m_LDMeanFaceMesh.lossyScale.x;
        for (int i = 0; i < lowVertices.Length; i++)
        {

            if(l2hDict.ContainsKey(i))
            {
                int indexFromHigh = l2hDict[i];

                Vector3 pos = m_HDDeformedFaceMesh.localToWorldMatrix.MultiplyPoint(DeformedVertices[indexFromHigh]);
                //pos = pos + m_HighMeshTransform.position / lowMeshScale;


                lowVertices[i] = pos;
                lowUVs[i] = DeformedUVs[indexFromHigh];
            }
            
        }

        m_LDDeformedFaceMesh.gameObject.GetComponent<MeshFilter>().mesh.vertices = lowVertices;
        m_LDDeformedFaceMesh.gameObject.GetComponent<MeshFilter>().mesh.uv2 = lowUVs;
        m_LDDeformedFaceMesh.gameObject.GetComponent<MeshFilter>().mesh.uv = lowUVs;
        m_LDDeformedFaceMesh.gameObject.GetComponent<MeshFilter>().mesh.triangles = m_LDMeanFaceMesh.gameObject.GetComponent<MeshFilter>().mesh.triangles;

        //Debug.Log(string.Format("org: {0} {1} {2} tar: {3} {4} {5}", ov.x, ov.y, ov.z, tv.x, tv.y, tv.z));
    }

    public void CalculateVerticesCorrespondingRelation(string jsonPath,bool raycast = false)
    {

        Vector3[] highVertices = m_HDMeanFaceMesh.gameObject.GetComponent<MeshFilter>().sharedMesh.vertices;
        Vector3[] lowVertices = m_LDMeanFaceMesh.gameObject.GetComponent<MeshFilter>().sharedMesh.vertices;

        Dictionary<int, float> high2lowDistDict = new Dictionary<int, float>();
        Dictionary<int, int> high2lowDict = new Dictionary<int, int>();

        Dictionary<int, float> low2highDistDict = new Dictionary<int, float>();
        Dictionary<int, int> low2highDict = new Dictionary<int, int>();
        if (raycast)
        {


            for (int h = 0; h < highVertices.Length; h++)
            {
                Vector3 hv = m_HDMeanFaceMesh.localToWorldMatrix.MultiplyPoint(highVertices[h]);
                Vector3 from = hv + Vector3.forward * 1000;
                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(from, -Vector3.forward, out hit, Mathf.Infinity))
                {

                    Vector3 hitPos = hit.point;


                    for (int l = 0; l < lowVertices.Length; l++)
                    {
                        Vector3 lv = lowVertices[l];

                        Vector3 whv = hitPos;
                        Vector3 wlv = m_LDMeanFaceMesh.localToWorldMatrix.MultiplyPoint(lv);

                        float dist = Vector3.Distance(whv, wlv);

                        if (high2lowDistDict.ContainsKey(h))
                        {
                        }
                        else
                        {
                            high2lowDistDict[h] = float.MaxValue; ;
                        }

                        float minDist = high2lowDistDict[h];
                        if (minDist > dist)
                        {
                            high2lowDistDict[h] = dist;
                            high2lowDict[h] = l;
                        }

                    }
                }
            }
        }
        else
        {
            bool H_L = false;
            if (H_L)
            {
                for (int h = 0; h < highVertices.Length; h++)
                {
                    Vector3 hv = highVertices[h];

                    for (int l = 0; l < lowVertices.Length; l++)
                    {
                        Vector3 lv = lowVertices[l];
                        Vector3 whv = m_HDMeanFaceMesh.localToWorldMatrix.MultiplyPoint(hv);
                        Vector3 wlv = m_LDMeanFaceMesh.localToWorldMatrix.MultiplyPoint(lv);

                        float dist = Vector3.Distance(whv, wlv);

                        if (high2lowDistDict.ContainsKey(h))
                        {
                        }
                        else
                        {
                            high2lowDistDict[h] = float.MaxValue; ;
                        }

                        float minDist = high2lowDistDict[h];
                        if (minDist > dist)
                        {
                            high2lowDistDict[h] = dist;
                            high2lowDict[h] = l;
                        }
                    }
                }
                
                foreach (var pair in high2lowDistDict)
                {
                    int h = pair.Key;
                    float dist = pair.Value;
                    int l = high2lowDict[h];

                    if (low2highDistDict.ContainsKey(l))
                    {
                    }
                    else
                    {
                        low2highDistDict[l] = float.MaxValue;
                    }

                    if (low2highDistDict[l] > dist)
                    {
                        low2highDistDict[l] = dist;
                        low2highDict[l] = h;
                    }
                }
                
            }
            else
            {
                for (int l = 0; l < lowVertices.Length; l++)
                {
                    Vector3 lv = lowVertices[l];

                    for (int h = 0; h < highVertices.Length; h++)
                    {
                        Vector3 hv = highVertices[h];
                        Vector3 whv = m_HDMeanFaceMesh.localToWorldMatrix.MultiplyPoint(hv);
                        Vector3 wlv = m_LDMeanFaceMesh.localToWorldMatrix.MultiplyPoint(lv);

                        float dist = Vector3.Distance(whv, wlv);

                        if (low2highDistDict.ContainsKey(l))
                        {
                        }
                        else
                        {
                            low2highDistDict[l] = float.MaxValue; ;
                        }

                        float minDist = low2highDistDict[l];
                        if (minDist > dist)
                        {
                            low2highDistDict[l] = dist;
                            low2highDict[l] = h;
                        }
                    }
                }
                
            }
        }

        SaveJson(jsonPath, low2highDict);

    }

    public void CalculateHDLDCorresponding(string savePath,string loadPath)
    {
        MeshCorresponding mc = new MeshCorresponding();
        List<int> indicesInLDMesh = mc.Load(loadPath);

        Mesh LDMeanMesh = m_LDMeanFaceMesh.GetComponent<MeshFilter>().mesh;
        Mesh HDMeanMesh = m_HDMeanFaceMesh.GetComponent<MeshFilter>().mesh;

        List<Vector3> hdVertices = HDMeanMesh.vertices.OfType<Vector3>().ToList();
        for(int lowCorrespondIndex = 0;lowCorrespondIndex< hdVertices.Count;lowCorrespondIndex++)
        {
            Vector3 pos = m_HDMeanFaceMesh.localToWorldMatrix.MultiplyPoint(hdVertices[lowCorrespondIndex]);
            hdVertices[lowCorrespondIndex] = pos;
        }

        Dictionary<int, int> LD2HDIndicesDict = new Dictionary<int, int>();


        for (int lowCorrespondIndex = 0; lowCorrespondIndex< indicesInLDMesh.Count;lowCorrespondIndex++)
        {
            int lowIndex = indicesInLDMesh[lowCorrespondIndex];
            Vector3 ldVertex = LDMeanMesh.vertices[lowIndex];
            Vector3 ldPosition = m_LDMeanFaceMesh.localToWorldMatrix.MultiplyPoint(ldVertex);

            //float minDistIndex = hdVertices.Min(pos => Vector3.Distance(ldVertex, pos));

            //var result = hdVertices.Select((val, ind) => new { Value = val, Index = ind }).OrderBy(x => Vector3.Distance(ldVertex, x.Value)).Select(x=>x.Index).ToArray();


            //LD2HDIndicesDict[i] = result[0];

            float minDist = float.MaxValue;
            int minHDIndex = -1;
            for (int highIndex = 0; highIndex < hdVertices.Count; highIndex++)
            {
                Vector3 hdPos = hdVertices[highIndex];

                Vector3 whv = hdPos;
                Vector3 wlv = ldVertex;

                float dist = Vector3.Distance(whv, wlv);
                

                
                if (minDist > dist)
                {
                    minDist = dist;
                    minHDIndex = highIndex;
                }

            }

            LD2HDIndicesDict[lowIndex] = minHDIndex;
        }

        SaveJson(savePath, LD2HDIndicesDict);
    }

    public void DrawTopology(string loadPath)
    {

        Dictionary<int, int> l2hDict;
        LoadJson(loadPath, out l2hDict);

        var indices = new int[l2hDict.Count];
        int lowIndex = 0;
        foreach (var pair in l2hDict)
        { 
            indices[lowIndex] = pair.Key;
            lowIndex++;
        }

        m_TargetMesh = new Mesh();

        Vector3[] verticesLocalPos = m_LDMeanFaceMesh.gameObject.GetComponent<MeshFilter>().sharedMesh.vertices;
        Vector3[] verticesWorldPos = new Vector3[verticesLocalPos.Length];
        for(int i = 0; i< verticesWorldPos.Length;i++)
        {

            verticesWorldPos[i] = m_LDMeanFaceMesh.localToWorldMatrix.MultiplyPoint(verticesLocalPos[i]);

        }
        m_TargetMesh.vertices = verticesWorldPos;
        m_TargetMesh.SetIndices(indices, MeshTopology.Points, 0);

        gameObject.GetComponent<MeshFilter>().sharedMesh = m_TargetMesh;

    }

    public void SaveJson(string savePath,Dictionary<int ,int> l2hDict)
    {

        HLVertexMap hlMap = new HLVertexMap();
        hlMap.items = new item[l2hDict.Count];

        int i = 0;
        foreach (var kv in l2hDict)
        {
            hlMap.items[i] = new item();
            hlMap.items[i].lowIndex = kv.Key;
            hlMap.items[i].highIndex = kv.Value;
            hlMap.items[i].index = i;
            i++;
        }

        string jstr = JsonUtility.ToJson(hlMap);
        File.WriteAllText(Path.Combine(Application.dataPath , savePath), jstr);
    }
    public void LoadJson(string loadPath,out Dictionary<int, int> l2hDict)
    {
        string jstr = File.ReadAllText(Path.Combine(Application.dataPath , loadPath));
        HLVertexMap hlMap  = JsonUtility.FromJson<HLVertexMap>(jstr);

        l2hDict = new Dictionary<int, int>();
        foreach (var item in hlMap.items)
        {
            l2hDict[item.lowIndex] = item.highIndex;
        }
    }
}
