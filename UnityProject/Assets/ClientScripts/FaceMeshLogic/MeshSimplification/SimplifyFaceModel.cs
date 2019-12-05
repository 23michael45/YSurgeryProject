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
    public class HLVertexMap
    {
        [Serializable]
        public class item
        {
            public int index;
            public int highIndex;
            public int lowIndex;
        }
        public item[] items;
    }


    [Serializable]
    public class BoneIndexMap
    {
        [Serializable]
        public class item
        {
            public string boneName;
            public int highIndex;
        }
        public item[] items;
    }
    public float m_Scale = 0.001f;

    //Used to calculate Vertices Relation
    public Transform m_HDMeanFaceMesh;
    public Transform m_LDMeanFaceMesh;
    public Transform m_HeadBoneRoot;
    public SkinnedMeshRenderer m_SkinMesh;

    //Used to calculate Deformed Low Mesh from High Deformed Mesh Vertices Position
    public Transform m_HDDeformedFaceMesh;
    public Transform m_LDDeformedFaceMesh;


    Mesh m_TargetMesh;


    public bool m_bUseRaycast = false;

    public void CalculateDeformedMeshLD(string loadPath)
    {
        Mesh hdDeformed = m_LDDeformedFaceMesh.GetComponent<MeshFilter>().sharedMesh;
        Mesh ldMean = m_LDMeanFaceMesh.GetComponent<MeshFilter>().sharedMesh;

        Vector3[] vertices;
        Vector2[] uvs;
        Vector2[] uvInRegion;
        int[] indices;
        CalculateDeformedMesh(loadPath, hdDeformed, ldMean, m_LDMeanFaceMesh, out vertices, out uvs, out uvInRegion, out indices);



        Mesh ldDeformedMesh = new Mesh();
        ldDeformedMesh.vertices = vertices;
        ldDeformedMesh.uv = uvs;
        ldDeformedMesh.uv2 = uvInRegion;
        ldDeformedMesh.triangles = indices;
        m_LDDeformedFaceMesh.GetComponent<MeshFilter>().sharedMesh = ldDeformedMesh;
    }
    public void CalculateDeformedMesh(string loadPath, Mesh hdDeformedMesh, Mesh ldMeanMesh, Transform ldMeanTransform, out Vector3[] vertices, out Vector2[] uvs, out Vector2[] uvInRegion, out int[] indices)
    {
        Dictionary<int, int> l2hDict;
        LoadHLMapJson(loadPath, out l2hDict);

        Vector3[] DeformedVertices = hdDeformedMesh.vertices;
        Vector2[] DeformedUVs = hdDeformedMesh.uv;

        Vector3[] ldVertices = new Vector3[ldMeanMesh.vertices.Length];
        Vector2[] ldUVs = new Vector2[ldMeanMesh.vertices.Length];


        uvInRegion = new Vector2[ldMeanMesh.vertices.Length];

        Debug.Log("Low Mesh Vertices Count:" + ldVertices.Length);
        for (int i = 0; i < ldVertices.Length; i++)
        {
            if (l2hDict.ContainsKey(i))
            {

                int lowIndex = i;
                int highIndex = l2hDict[i];


                Vector3 wpos = DeformedVertices[highIndex];
                Vector3 lpos = ldMeanTransform.worldToLocalMatrix.MultiplyPoint(wpos);


                ldVertices[lowIndex] = lpos;
                //ldVertices[lowIndex] = wpos;
                //ldVertices[lowIndex] = ldMeanMesh.vertices[i];


                Vector2 uv = DeformedUVs[highIndex];
                if (uv.x < 0)
                {
                    uv.x = -uv.x;
                }
                //  if(uv.y < 0)
                // {
                //     uv.y +=1;
                // }
                ldUVs[lowIndex] = uv;

                uvInRegion[lowIndex] = new Vector2(1, 1);
            }
            else
            {
                ldVertices[i] = ldMeanMesh.vertices[i];
                //ldVertices[i] = Vector3.zero;


                Vector2 uv = ldMeanMesh.uv[i];
                // if(uv.x < 0)
                // {
                //     uv.x +=1;
                // }
                //  if(uv.y < 0)
                // {
                //     uv.y +=1;
                // }
                ldUVs[i] = uv;
                uvInRegion[i] = new Vector2(0, 0);
            }

        }


        vertices = ldVertices;
        uvs = ldUVs;
        indices = ldMeanMesh.triangles;

    }

    public void CalculateVerticesCorrespondingRelation(string jsonPath, bool raycast = false)
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

        SaveHLMapJson(jsonPath, low2highDict);

    }

    public void CalculateHDLDCorresponding(string savePath, string loadPath)
    {
        MeshCorresponding mc = new MeshCorresponding();
        List<int> indicesInLDMesh = mc.Load(loadPath);

        Mesh LDMeanMesh = m_LDMeanFaceMesh.GetComponent<MeshFilter>().sharedMesh;
        Mesh HDMeanMesh = m_HDMeanFaceMesh.GetComponent<MeshFilter>().sharedMesh;

        List<Vector3> hdVertices = HDMeanMesh.vertices.OfType<Vector3>().ToList();
        for (int i = 0; i < hdVertices.Count; i++)
        {
            Vector3 pos = m_HDMeanFaceMesh.localToWorldMatrix.MultiplyPoint(hdVertices[i]);
            hdVertices[i] = pos;
        }

        Dictionary<int, int> LD2HDIndicesDict = new Dictionary<int, int>();


        for (int lowCorrespondIndex = 0; lowCorrespondIndex < indicesInLDMesh.Count; lowCorrespondIndex++)
        {
            int lowIndex = indicesInLDMesh[lowCorrespondIndex];

            if (LDMeanMesh.vertices.Length <= lowIndex || lowIndex < 0)
            {
                Debug.LogError("LowIndex Out of Range : " + lowIndex);
                return;
            }

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

                float dist = Vector3.Distance(hdPos, ldPosition);



                if (minDist > dist)
                {
                    minDist = dist;
                    minHDIndex = highIndex;
                }

            }
            if (minHDIndex != -1)
            {


                if (LD2HDIndicesDict.ContainsKey(lowIndex))
                {
                    Debug.LogError("Error Duplicate Low Index : " + lowIndex);
                }
                else
                {
                    LD2HDIndicesDict[lowIndex] = minHDIndex;

                }
            }
            else
            {
                Debug.LogError("Error High Index -1");

            }
        }

        SaveHLMapJson(savePath, LD2HDIndicesDict);
    }

    public void CalculateBoneCorresponding(string savePath)
    {
        Dictionary<string, int> boneName2HDIndexDict = new Dictionary<string, int>();

        Mesh HDMeanMesh = m_HDMeanFaceMesh.GetComponent<MeshFilter>().sharedMesh;

        List<Vector3> hdVertices = HDMeanMesh.vertices.OfType<Vector3>().ToList();
        for (int i = 0; i < hdVertices.Count; i++)
        {
            Vector3 pos = m_HDMeanFaceMesh.localToWorldMatrix.MultiplyPoint(hdVertices[i]);
            hdVertices[i] = pos;
        }


        Transform[] tranformBones = m_HeadBoneRoot.GetComponentsInChildren<Transform>();
        for (int i = 0; i < tranformBones.Length; i++)
        {
            string name = tranformBones[i].name;
            Vector3 bonePosition = tranformBones[i].position;


            float minDist = float.MaxValue;

            int minHDIndex = -1;
            for (int highIndex = 0; highIndex < hdVertices.Count; highIndex++)
            {
                Vector3 hdPos = hdVertices[highIndex];

                float dist = Vector3.Distance(hdPos, bonePosition);

                if (minDist > dist)
                {
                    minDist = dist;
                    minHDIndex = highIndex;
                }
            }

            if (minDist < 2.0f)
            {
                boneName2HDIndexDict[name] = minHDIndex;
            }
            else
            {
                boneName2HDIndexDict[name] = -1;
            }
        }
        SaveBoneIndexMap(savePath, boneName2HDIndexDict);

    }

    public void CalculateCorrespondingLowMeshUVFromHighMesh(string savePath)
    {
        StartCoroutine(CalculateCorrespondingLowMeshUVFromHighMeshCoroutine(savePath));
    }
    public IEnumerator CalculateCorrespondingLowMeshUVFromHighMeshCoroutine(string savePath)
    {
        Mesh LDMeanMesh = new Mesh();
        MeshFilter lmf = m_LDMeanFaceMesh.GetComponent<MeshFilter>();
        if (lmf)
        {
            LDMeanMesh = lmf.sharedMesh;
        }
        SkinnedMeshRenderer lsmr = m_LDMeanFaceMesh.GetComponent<SkinnedMeshRenderer>();
        if (lsmr)
        {
            LDMeanMesh = lsmr.sharedMesh;
        }
        Mesh HDMeanMesh = m_HDMeanFaceMesh.GetComponent<MeshFilter>().sharedMesh;

        Mesh newLowMesh = Instantiate(LDMeanMesh);


        Vector3[] lowVertices = LDMeanMesh.vertices;
        Vector3[] highVertices = HDMeanMesh.vertices;
        Vector2[] lowuv = LDMeanMesh.uv;
        Vector2[] highuv = HDMeanMesh.uv;

        for (int lowIndex = 0; lowIndex < lowVertices.Length; lowIndex++)
        {
            Vector3 lowVertex = lowVertices[lowIndex];
            Vector3 wlowVertex = m_LDMeanFaceMesh.localToWorldMatrix.MultiplyPoint(lowVertex);

            float minDist = float.MaxValue;
            int minHighIndex = -1;
            Vector3 minHighVertex = Vector3.zero;
            Vector3 minWHighVertex = Vector3.zero;

            //Debug.Log("Low Vertex" + lowVertex);

            for (int highIndex = 0; highIndex < highVertices.Length; highIndex++)
            {
                Vector3 highVertex = highVertices[highIndex];
                Vector3 whighVertex = m_HDMeanFaceMesh.localToWorldMatrix.MultiplyPoint(highVertex);

                float dist = Vector3.Distance(whighVertex, wlowVertex);


                if (dist < minDist)
                {
                    minDist = dist;
                    minHighIndex = highIndex;
                    minHighVertex = highVertex;
                    minWHighVertex = whighVertex;
                }
            }


            if (minDist < 10f)
            {
                //Debug.Log(string.Format("mindist {2} lowIndex {0} highIndex {1}", lowIndex, minHighIndex,minDist));
                Vector2 uv = highuv[minHighIndex];
                if (uv.x < 0)
                {
                    uv.x = -uv.x;
                }
                lowuv[lowIndex] = uv;
            }

            yield return new WaitForEndOfFrame();

            Debug.Log(lowIndex);
        }

        newLowMesh.uv = lowuv;


        MeshFilter mf = gameObject.GetComponent<MeshFilter>();
        if (mf)
        {
            mf.sharedMesh = newLowMesh;

        }
        SkinnedMeshRenderer smr = gameObject.GetComponent<SkinnedMeshRenderer>();
        if (smr)
        {
            smr.sharedMesh = newLowMesh;

        }

        UnityFBXExporter.FBXExporter.ExportGameObjToFBX(gameObject, Path.Combine(Application.dataPath, savePath));
    }

    public void RebindDeformedBone(string loadPath)
    {
        Mesh hdDeformed = m_LDDeformedFaceMesh.GetComponent<MeshFilter>().sharedMesh;

        SkinnedMeshRenderer skinMesh = m_LDMeanFaceMesh.GetComponent<SkinnedMeshRenderer>();
        Matrix4x4[] bindposes;
        RebindBones(loadPath, hdDeformed, skinMesh.transform, skinMesh.bones, skinMesh.rootBone, out bindposes);
    }
    public void RebindBones(string loadPath, Mesh hdDeformedMesh,Transform skinnedMeshRendererTransform, Transform[] bones, Transform parentBonesTransfrom, out Matrix4x4[] bindposes)
    {
        Transform[] dstBonesHierarchy = parentBonesTransfrom.GetComponentsInChildren<Transform>();
        Dictionary<string, Transform> bonesMap = new Dictionary<string, Transform>();
        foreach (Transform t in dstBonesHierarchy)
        {
            bonesMap[t.name] = t;
        }


        Dictionary<string, int> biMap;
        LoadBoneIndexMap(loadPath, out biMap);


        Vector3[] DeformedVertices = hdDeformedMesh.vertices;

        //不能直接用bindposes,要new 出一份，否则会改变ldSkinMeanMesh
        //bindposes = ldSkinMeanMesh.sharedMesh.bindposes;
        bindposes = new Matrix4x4[bones.Length];
        
        foreach (KeyValuePair<string, int> kv in biMap)
        {
            if (kv.Value != -1)
            {
                if (bonesMap.ContainsKey(kv.Key))
                {
                    bonesMap[kv.Key].position = DeformedVertices[kv.Value];
                }

            }

        }

        //bindpose  计算一个vertex,在bone的局部坐标系的位置
        for (int i = 0; i < bones.Length; i++)
        {
            bindposes[i] = bones[i].worldToLocalMatrix * skinnedMeshRendererTransform.localToWorldMatrix;
        }
    }

    public void DrawLowTopology(string loadPath)
    {

        Dictionary<int, int> l2hDict;
        LoadHLMapJson(loadPath, out l2hDict);

        var indices = new int[l2hDict.Count];
        int lowIndex = 0;
        foreach (var pair in l2hDict)
        {
            indices[lowIndex] = pair.Key;
            lowIndex++;
        }


        Vector3[] verticesLocalPos = m_LDMeanFaceMesh.gameObject.GetComponent<MeshFilter>().sharedMesh.vertices;
        Vector3[] verticesWorldPos = new Vector3[verticesLocalPos.Length];
        for (int i = 0; i < verticesWorldPos.Length; i++)
        {

            verticesWorldPos[i] = m_LDMeanFaceMesh.localToWorldMatrix.MultiplyPoint(verticesLocalPos[i]);

        }

        DrawTopologyByIndices(verticesWorldPos, indices);
    }

    public void DrawHighTopology(string loadPath)
    {

        Dictionary<int, int> l2hDict;
        LoadHLMapJson(loadPath, out l2hDict);

        var indices = new int[l2hDict.Count];
        int lowIndex = 0;
        foreach (var pair in l2hDict)
        {
            indices[lowIndex] = pair.Value;
            lowIndex++;
        }


        Vector3[] verticesLocalPos = m_HDMeanFaceMesh.gameObject.GetComponent<MeshFilter>().sharedMesh.vertices;
        Vector3[] verticesWorldPos = new Vector3[verticesLocalPos.Length];
        for (int i = 0; i < verticesWorldPos.Length; i++)
        {
            verticesWorldPos[i] = m_HDMeanFaceMesh.localToWorldMatrix.MultiplyPoint(verticesLocalPos[i]);
        }

        DrawTopologyByIndices(verticesWorldPos, indices);
    }

    public void DrawFaceRegionBones(string loadPath)
    {
        Dictionary<string, int> biMap;
        LoadBoneIndexMap(loadPath, out biMap);


        List<int> indices = new List<int>();

        Transform[] tranformBones = m_HeadBoneRoot.GetComponentsInChildren<Transform>();
        for (int i = 0; i < tranformBones.Length; i++)
        {
            Transform bone = tranformBones[i];
            if (biMap.ContainsKey(bone.name))
            {
                if (biMap[bone.name] != -1)
                {
                    indices.Add(biMap[bone.name]);
                }

            }
            else
            {
                Debug.Log("Bone not Exist in Json Map");
            }
        }

        Vector3[] verticesLocalPos = m_HDMeanFaceMesh.gameObject.GetComponent<MeshFilter>().sharedMesh.vertices;
        Vector3[] verticesWorldPos = new Vector3[verticesLocalPos.Length];
        for (int i = 0; i < verticesWorldPos.Length; i++)
        {
            verticesWorldPos[i] = m_HDMeanFaceMesh.localToWorldMatrix.MultiplyPoint(verticesLocalPos[i]);
        }
        DrawTopologyByIndices(verticesWorldPos, indices.ToArray());
    }
    public void DrawTopologyByIndices(Vector3[] vertices, int[] indices)
    {
        var mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.SetIndices(indices, MeshTopology.Points, 0);
        gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
    }


    void SaveHLMapJson(string savePath, Dictionary<int, int> l2hDict)
    {

        HLVertexMap hlMap = new HLVertexMap();
        hlMap.items = new HLVertexMap.item[l2hDict.Count];

        int i = 0;
        foreach (var kv in l2hDict)
        {
            hlMap.items[i] = new HLVertexMap.item();
            hlMap.items[i].lowIndex = kv.Key;
            hlMap.items[i].highIndex = kv.Value;
            hlMap.items[i].index = i;
            i++;
        }
        SaveJson(savePath, hlMap);
    }
    void LoadHLMapJson(string loadPath, out Dictionary<int, int> l2hDict)
    {
        HLVertexMap hlMap = LoadJson<HLVertexMap>(loadPath);
        l2hDict = new Dictionary<int, int>();
        foreach (var item in hlMap.items)
        {
            l2hDict[item.lowIndex] = item.highIndex;
        }
    }

    void SaveBoneIndexMap(string savePath, Dictionary<string, int> biMap)
    {
        BoneIndexMap biMapObj = new BoneIndexMap();
        biMapObj.items = new BoneIndexMap.item[biMap.Count];

        int i = 0;
        foreach (var kv in biMap)
        {
            biMapObj.items[i] = new BoneIndexMap.item();
            biMapObj.items[i].boneName = kv.Key;
            biMapObj.items[i].highIndex = kv.Value;
            i++;
        }
        SaveJson(savePath, biMapObj);
    }
    void LoadBoneIndexMap(string loadPath, out Dictionary<string, int> biMap)
    {
        BoneIndexMap obj = LoadJson<BoneIndexMap>(loadPath);
        biMap = new Dictionary<string, int>();
        foreach (var item in obj.items)
        {
            biMap[item.boneName] = item.highIndex;
        }
    }




    void SaveJson<T>(string savePath, T obj)
    {
        string jstr = JsonUtility.ToJson(obj);
        File.WriteAllText(Path.Combine(Application.dataPath, savePath), jstr);
    }
    T LoadJson<T>(string loadPath)
    {
        string jstr = File.ReadAllText(Path.Combine(Application.dataPath, loadPath));
        return JsonUtility.FromJson<T>(jstr);

    }
}
