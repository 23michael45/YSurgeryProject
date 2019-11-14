﻿/*
 * Copyright (c) 2019 Dummiesman
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
*/

using Dummiesman;
using System.Collections.Generic;
using UnityEngine;

namespace Dummiesman
{
    public class OBJObjectBuilder
    {
        //
        public int PushedFaceCount { get; private set; } = 0;

        //stuff passed in by ctor
        private OBJLoader _loader;
        private string _name;

        private Dictionary<string, List<int>> _materialIndices = new Dictionary<string, List<int>>();
        private List<int> _currentIndexList;
        private string _lastMaterial = null;

        //our local vert/normal/uv
        private List<Vector3> _vertices = new List<Vector3>();
        private List<Vector3> _normals = new List<Vector3>();
        private List<Vector2> _uvs = new List<Vector2>();

        //this will be set if the model has no normals or missing normal info
        private bool recalculateNormals = false;

        /// <summary>
        /// Loop hasher helper class
        /// </summary>
        private class ObjLoopHash
        {
            public int vertexIndex;
            public int normalIndex;
            public int uvIndex;

            public override bool Equals(object obj)
            {
                if (!(obj is ObjLoopHash))
                    return false;

                var hash = obj as ObjLoopHash;
                return (hash.vertexIndex == vertexIndex) && (hash.uvIndex == uvIndex) && (hash.normalIndex == normalIndex);
            }

            public override int GetHashCode()
            {
                int hc = 3;
                hc = unchecked(hc * 314159 + vertexIndex);
                hc = unchecked(hc * 314159 + normalIndex);
                hc = unchecked(hc * 314159 + uvIndex);
                return hc;
            }
        }

        public GameObject Build()
        {
            var go = new GameObject(_name);

            //add meshrenderer
            var mr = go.AddComponent<MeshRenderer>();
            int submesh = 0;


            //locate the material for each submesh
            Material[] materialArray = new Material[_materialIndices.Count];
            foreach (var kvp in _materialIndices)
            {
                Material material = null;
                if (_loader.Materials == null)
                {
                    material = OBJLoaderHelper.CreateNullMaterial();
                    material.name = kvp.Key;
                }
                else
                {
                    if (!_loader.Materials.TryGetValue(kvp.Key, out material))
                    {
                        material = OBJLoaderHelper.CreateNullMaterial();
                        material.name = kvp.Key;
                        _loader.Materials[kvp.Key] = material;
                    }
                }
                materialArray[submesh] = material;
                submesh++;
            }
            mr.sharedMaterials = materialArray;

            //add meshfilter
            var mf = go.AddComponent<MeshFilter>();
            submesh = 0;


            _vertices = _loader.Vertices;
            _normals = _loader.Normals;
            _uvs = _loader.UVs;

            var msh = new Mesh()
            {
                name = _name,
                indexFormat = (_vertices.Count > 65535) ? UnityEngine.Rendering.IndexFormat.UInt32 : UnityEngine.Rendering.IndexFormat.UInt16,
                subMeshCount = _materialIndices.Count
            };

            //set vertex data
            msh.SetVertices(_vertices);
            msh.SetNormals(_normals);
            msh.SetUVs(0, _uvs);

            //set faces
            foreach (var kvp in _materialIndices)
            {
                msh.SetTriangles(kvp.Value, submesh);
                submesh++;
            }

            //recalculations
            if (recalculateNormals)
                msh.RecalculateNormals();
            msh.RecalculateTangents();
            msh.RecalculateBounds();

            mf.sharedMesh = msh;

            //
            return go;
        }

        public void SetMaterial(string name)
        {
            if (!_materialIndices.TryGetValue(name, out _currentIndexList))
            {
                _currentIndexList = new List<int>();
                _materialIndices[name] = _currentIndexList;
            }
        }


        public void PushFace(string material, List<int> vertexIndices, List<int> normalIndices, List<int> uvIndices)
        {
            //invalid face size?
            if (vertexIndices.Count < 3)
            {
                return;
            }

            //set material
            if (material != _lastMaterial)
            {
                SetMaterial(material);
                _lastMaterial = material;
            }

            //for (int i = 0; i < vertexIndices.Count; i++)
            //{
            //    int vertexIndex = vertexIndices[i];
            //    int normalIndex = normalIndices[i];
            //    int uvIndex = uvIndices[i];


            //    //add new verts and what not
            //    _vertices.Add((vertexIndex >= 0 && vertexIndex < _loader.Vertices.Count) ? _loader.Vertices[vertexIndex] : Vector3.zero);
            //    _normals.Add((normalIndex >= 0 && normalIndex < _loader.Normals.Count) ? _loader.Normals[normalIndex] : Vector3.zero);
            //    _uvs.Add((uvIndex >= 0 && uvIndex < _loader.UVs.Count) ? _loader.UVs[uvIndex] : Vector2.zero);

            //    //mark recalc flag
            //    if (normalIndex < 0)
            //        recalculateNormals = true;


            //}


            //add face to our mesh list
            if (vertexIndices.Count == 3)
            {
                //by michael ,if z not flip ,need flip face ,otherwise not flip face
                _currentIndexList.AddRange(new int[] { vertexIndices[0], vertexIndices[1], vertexIndices[2] });
                //_currentIndexList.AddRange(new int[] { indexRemap[0], indexRemap[2], indexRemap[1] });
            }
            else if (vertexIndices.Count == 4)
            {
                _currentIndexList.AddRange(new int[] { vertexIndices[0], vertexIndices[1], vertexIndices[2] });
                _currentIndexList.AddRange(new int[] { vertexIndices[2], vertexIndices[3], vertexIndices[0] });
            }
            else if (vertexIndices.Count > 4)
            {
                for (int i = vertexIndices.Count - 1; i >= 2; i--)
                {
                    _currentIndexList.AddRange(new int[] { vertexIndices[0], vertexIndices[i - 1], vertexIndices[i] });
                }
            }

            PushedFaceCount++;
        }

        public OBJObjectBuilder(string name, OBJLoader loader)
        {
            _name = name;
            _loader = loader;
        }
    }
}