﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShowVertexByIndex : MonoBehaviour
{
    public List<int> mSelectIndices = new List<int>();

    public GameObject mPrefab;

    List<GameObject> mHandlers = new List<GameObject>();

    public int mVertexIndex = -1;
    private void Start()
    {
        Clear();
    }
    void Clear()
    {
        foreach(GameObject go in mHandlers)
        {
            DestroyImmediate(go);
        }
        mHandlers.Clear();
    }

    void Create()
    {
        for(int i = 0; i< mSelectIndices.Count;i++)
        {
            GameObject gonew = Instantiate(mPrefab);
            gonew.transform.parent = transform;
            gonew.SetActive(true);
            mHandlers.Add(gonew);
        }
    }

    private void Update()
    {
        if(mSelectIndices.Count != mHandlers.Count)
        {
            Clear();
            Create();
        }
        for (int i = 0; i < mSelectIndices.Count; i++)
        {
            int index = mSelectIndices[i];
            mHandlers[i].name = index.ToString();

            MeshFilter mf = GetComponent<MeshFilter>();
            if(mf)
            {

                Vector3 lpos = mf.sharedMesh.vertices[index];
                mHandlers[i].transform.position = transform.localToWorldMatrix.MultiplyPoint(lpos);
            }

            SkinnedMeshRenderer smr = GetComponent<SkinnedMeshRenderer>();
            if (smr)
            {
                Vector3 lpos = smr.sharedMesh.vertices[index];
                mHandlers[i].transform.position = transform.localToWorldMatrix.MultiplyPoint(lpos);


            }
        }

        
        if(mVertexIndex >= 0)
        {
            SkinnedMeshRenderer smr = GetComponent<SkinnedMeshRenderer>();
            if(smr)
            {
                Vector2 uv = smr.sharedMesh.uv[mVertexIndex];
                Debug.Log(string.Format("u: {0}  v:{1}", uv.x, uv.y));

            }
        }
    }

}
