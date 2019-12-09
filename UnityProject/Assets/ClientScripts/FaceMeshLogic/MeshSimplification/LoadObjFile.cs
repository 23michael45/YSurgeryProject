using Dummiesman;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//因为Unity 自带的Obj Import会把Obj文件的x 反向变为-x，所以要自定义一下obj file的读取
[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class LoadObjFile : MonoBehaviour
{


    public void Load(string path,bool bFlipX)
    {
        GameObject meshObj = new OBJLoader().Load(path, bFlipX);

        Mesh newMesh = Instantiate(meshObj.GetComponentInChildren<MeshFilter>().sharedMesh);
        gameObject.GetComponent<MeshFilter>().sharedMesh = newMesh;

        DestroyImmediate(meshObj);

        Debug.Log(string.Format("Vertices x: {0}  y: {1} z:{2}", newMesh.vertices[0].x, newMesh.vertices[0].y, newMesh.vertices[0].z));
        Debug.Log(string.Format("Triangle:{0} {1} {2}", newMesh.triangles[0], newMesh.triangles[1], newMesh.triangles[2]));
        Debug.Log(string.Format("UV u:{0} v:{1}", newMesh.uv[0].x,newMesh.uv[0].y));

    }

}
