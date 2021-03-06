﻿using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class RuntimeObjExporter
{
    public static string MeshToString(MeshFilter mf)
    {
        Mesh m = mf.mesh;
        Material[] mats = mf.gameObject.GetComponent<Renderer>().sharedMaterials;
        return MeshToString(mf.name, m, mats);
    }
    public static string MeshToString(string name, Mesh m, Material[] mats)
    {


        StringBuilder sb = new StringBuilder();

        sb.Append("g ").Append(name).Append("\n");
        foreach (Vector3 v in m.vertices)
        {
            sb.Append(string.Format("v {0} {1} {2}\n", v.x, v.y, v.z));
        }
        sb.Append("\n");
        foreach (Vector3 v in m.normals)
        {
            sb.Append(string.Format("vn {0} {1} {2}\n", v.x, v.y, v.z));
        }
        sb.Append("\n");
        foreach (Vector3 v in m.uv)
        {
            sb.Append(string.Format("vt {0} {1}\n", v.x, v.y));
        }

        int[] triangles = m.triangles;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            //sb.Append(string.Format("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n", triangles[i] + 1, triangles[i + 1] + 1, triangles[i + 2] + 1));

            sb.Append(string.Format("f {0}/{0} {1}/{1} {2}/{2}\n", triangles[i] + 1, triangles[i + 1] + 1, triangles[i + 2] + 1));
        }

        if (mats != null)
        {
            for (int material = 0; material < m.subMeshCount; material++)
            {
                sb.Append("\n");
                sb.Append("usemtl ").Append(mats[material].name).Append("\n");
                sb.Append("usemap ").Append(mats[material].name).Append("\n");

                int[] subtriangles = m.GetTriangles(material);
                for (int i = 0; i < subtriangles.Length; i += 3)
                {
                    sb.Append(string.Format("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n",
                        subtriangles[i] + 1, subtriangles[i + 1] + 1, subtriangles[i + 2] + 1));
                }
            }
        }
        return sb.ToString();
    }

    public static void MeshToFile(MeshFilter mf, string filename)
    {
        using (StreamWriter sw = new StreamWriter(filename))
        {
            sw.Write(MeshToString(mf));
        }
    }

    public static void MeshToFile(Mesh m, string filename)
    {
        using (StreamWriter sw = new StreamWriter(filename))
        {
            sw.Write(MeshToString("bakedMesh", m, null));
        }
    }
}