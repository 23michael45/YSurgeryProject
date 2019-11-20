using UnityEngine;

[ExecuteInEditMode]
public class BoneOperation : MonoBehaviour
{
    public Material mMat;
    public void BoneRebind()
    {

        Mesh m = new Mesh();
        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(-1, -1, 0);
        vertices[1] = new Vector3(-1, 1, 0);
        vertices[2] = new Vector3(1, 1, 0);
        vertices[3] = new Vector3(1, -1, 0);

        Vector2[] uvs = new Vector2[4];
        uvs[0] = new Vector2(0, 1);
        uvs[1] = new Vector2(0, 0);
        uvs[2] = new Vector2(1, 0);
        uvs[3] = new Vector2(1, 1);

        int[] indices = new int[6];
        indices[0] = 0;
        indices[1] = 1;
        indices[2] = 2;
        indices[3] = 0;
        indices[4] = 2;
        indices[5] = 3;



        int verCount = vertices.Length;

        Transform[] bones = new Transform[verCount];
        Matrix4x4[] bindPoses = new Matrix4x4[verCount];
        BoneWeight[] boneWeights = new BoneWeight[verCount];

        for (int i = 0; i < vertices.Length; i++)
        {

            Transform bone = new GameObject("bone" + i.ToString()).transform;
            // bone.parent = transform;
            bone.position = vertices[i];


            bindPoses[i] = bone.worldToLocalMatrix * transform.localToWorldMatrix;


            boneWeights[i].boneIndex0 = i;
            boneWeights[i].weight0 = 1;

            bones[i] = bone;
        }

        m.vertices = vertices;
        m.triangles = indices;
        m.uv = uvs;

        m.bindposes = bindPoses;
        m.boneWeights = boneWeights;

        SkinnedMeshRenderer skinmesh = gameObject.GetComponent<SkinnedMeshRenderer>();
        skinmesh.sharedMesh = m;
        skinmesh.sharedMaterial = mMat;
        skinmesh.bones = bones;





    }
}