using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class MeshInfoSelector : MonoBehaviour
{
    public int mSelectIndex;
    public Vector3 mSelectVertex;
    public Vector2 mSelectUV;


    public int mTri0, mTri1, mTri2;

    public float mMinDist;

    public static float DistancePointLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
    {
        return Vector3.Magnitude(ProjectPointLine(point, lineStart, lineEnd) - point);
    }
    public static Vector3 ProjectPointLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
    {
        Vector3 rhs = point - lineStart;
        Vector3 vector2 = lineEnd - lineStart;
        float magnitude = vector2.magnitude;
        Vector3 lhs = vector2;
        if (magnitude > 1E-06f)
        {
            lhs = (Vector3)(lhs / magnitude);
        }
        float num2 = Mathf.Clamp(Vector3.Dot(lhs, rhs), 0f, magnitude);
        return (lineStart + ((Vector3)(lhs * num2)));
    }
    private void Update()
    {
        Mesh mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
        Vector3[] trivertices = new Vector3[3];

        trivertices[0] = mesh.vertices[mTri0];
        trivertices[1] = mesh.vertices[mTri1];
        trivertices[2] = mesh.vertices[mTri2];

        trivertices[0] = transform.TransformPoint(trivertices[0]);
        trivertices[1] = transform.TransformPoint(trivertices[1]);
        trivertices[2] = transform.TransformPoint(trivertices[2]);
        Debug.DrawLine(trivertices[0], trivertices[1]);
        Debug.DrawLine(trivertices[1], trivertices[2]);
        Debug.DrawLine(trivertices[2], trivertices[0]);
    }

    public void PickVertex(Ray ray)
    {

        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit))
            return;

        MeshCollider meshCollider = hit.collider as MeshCollider;
        if (meshCollider == null || meshCollider.sharedMesh == null)
            return;

        Mesh mesh = meshCollider.sharedMesh;
        Vector3[] vertices = mesh.vertices;
        Vector2[] uvs = mesh.uv;
        int[] triangles = mesh.triangles;

        Vector3[] trivertices = new Vector3[3];

        mTri0 = triangles[hit.triangleIndex * 3 + 0];
        mTri1 = triangles[hit.triangleIndex * 3 + 1];
        mTri2 = triangles[hit.triangleIndex * 3 + 2];

        trivertices[0] = vertices[mTri0];
        trivertices[1] = vertices[mTri1];
        trivertices[2] = vertices[mTri2];

        Transform hitTransform = hit.collider.transform;
        trivertices[0] = hitTransform.TransformPoint(trivertices[0]);
        trivertices[1] = hitTransform.TransformPoint(trivertices[1]);
        trivertices[2] = hitTransform.TransformPoint(trivertices[2]);
        Debug.DrawLine(trivertices[0], trivertices[1]);
        Debug.DrawLine(trivertices[1], trivertices[2]);
        Debug.DrawLine(trivertices[2], trivertices[0]);



        mMinDist = float.MaxValue;

        for (int i = 0; i < 3; i++)
        {
            float dist = Vector3.Distance(trivertices[i], hit.point);
            //float dist = Vector3.Cross(ray.direction,  - ray.origin).magnitude;
            //float dist = DistancePointLine(trivertices[i], ray.origin, ray.origin + ray.direction * float.MaxValue); 
            if (dist < mMinDist)
            {
                mMinDist = dist;
                mSelectIndex = triangles[hit.triangleIndex * 3 + i];
                mSelectVertex = vertices[mSelectIndex];
                mSelectUV = uvs[mSelectIndex];
            }
        }
    }


    public void LoadMeshFromSource(GameObject meshSourceObject)
    {
        Mesh mesh = null;
        Material material = null;

        MeshFilter mf = meshSourceObject.GetComponent<MeshFilter>();
        MeshRenderer mr = meshSourceObject.GetComponent<MeshRenderer>();
        if (mf && mr)
        {
            mesh = mf.sharedMesh;
            material = mr.sharedMaterial;
        }

        SkinnedMeshRenderer smr = meshSourceObject.GetComponent<SkinnedMeshRenderer>();
        if (smr)
        {
            mesh = smr.sharedMesh;
            material = smr.sharedMaterial;

        }

        gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
        gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = material;

        transform.position = meshSourceObject.transform.position;
        transform.rotation = meshSourceObject.transform.rotation;



    }



    void OnDrawGizmosSelected()
    {

        Mesh mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;

        Vector3 localPos = mesh.vertices[mSelectIndex];

        mSelectVertex = localPos;

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.TransformPoint(localPos), 5);



    }
}
