using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSkinMesh : MonoBehaviour
{
    public Mesh[] mMeshes;
    // Start is called before the first frame update
    void Start()
    {
        var v0 = mMeshes[0].vertices[0];
        var v1 = mMeshes[1].vertices[0];

        //var m0 = mMeshes[0].bindposes[0];
        var m1 = mMeshes[1].bindposes[0];

        Debug.Log(v0);
        Debug.Log(v1);

        //Debug.Log(m0);
        Debug.Log(m1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
