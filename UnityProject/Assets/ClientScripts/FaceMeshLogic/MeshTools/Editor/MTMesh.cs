using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace MeshTools
{
	[System.Serializable]
	public class MTMesh : ScriptableObject, ISerializationCallbackReceiver
	{
		public GameObject gameObject;				// Reference to source gameObject.
		public Transform transform;					// Reference to source gameObject transform.
		public Mesh cloneMesh;						// A clone of the source mesh that we can edit.
		public Mesh originalMesh;					// Used to revert in the case that this is a procedural mesh.
		public MTHandleRenderer handlesRenderer;	// 
		public ModelSource source;					// The original mesh.
		public string originalMeshGUID;				// The original mesh GUID.
		public Dictionary<int, int> triangleLookup = new Dictionary<int, int>();	// Shared triangle lookup table.  Keys are indices in triangles array, Value is index in sharedTriangles array.
		public List<List<int>> sharedTriangles = new List<List<int>>();
 
		// Getters
		public int vertexCount { get { return cloneMesh.vertexCount; } }
		public Vector3[] vertices { get { return cloneMesh.vertices; } set { cloneMesh.vertices = value; } }
		public Vector2[] uvs { get { return cloneMesh.uv; } }
		public Vector3[] normals { get { return cloneMesh.normals; } }
		public int[] indices { get { return cloneMesh.triangles; } }
		public int[] GetIndices(int submesh) { return cloneMesh.GetIndices(submesh); }
		public void SetIndices(int submesh, int[] tris) { cloneMesh.SetIndices(tris, cloneMesh.GetTopology(submesh), submesh); }

		[SerializeField] private MTTriangle[] _faces;
		[SerializeField] private MTEdge[] _edges;
		[SerializeField] private MTEdge[] _userEdges;	// Same as _edges, but with no duplicates and guaranteed to point to first index sharedTriangle array.
		[SerializeField] private int _vertexCount;		// used to determine if the cache needs rebuilt
		[SerializeField] private int _triangleCount;	// used to determine if the cache needs rebuilt

		public MTTriangle[] faces { get { return _faces; } }
		public MTEdge[] edges { get { return _edges; } }
		public MTEdge[] userEdges { get { return _userEdges; } }
		public int GetCachedVertexCount() { return _vertexCount; }
		public int GetCachedTriangleCount() { return _triangleCount; }
		/** 
		 * Get an array of MTTriangle from this mesh.
		 */
		public MTTriangle[] GetFaces()
		{
			int[] tris = cloneMesh.triangles;

			MTTriangle[] t = new MTTriangle[ tris.Length / 3 ];
			int index = 0;

			for(int i = 0; i < tris.Length; i+=3)
			{
				t[index++] = new MTTriangle(tris[i], tris[i+1], tris[i+2]);
			}

			return t;
		}

		/**
		 * Get an array of MTEdge from this mesh (three per-triangle).
		 */
		public MTEdge[] GetEdges()
		{
			int[] tris = cloneMesh.triangles;

			MTEdge[] edges = new MTEdge[tris.Length];

			for(int i = 0; i < tris.Length; i+=3)
			{
				edges[i+0] = new MTEdge(tris[i+0], tris[i+1]);
				edges[i+1] = new MTEdge(tris[i+1], tris[i+2]);
				edges[i+2] = new MTEdge(tris[i+2], tris[i+0]);
			}

			return edges;
		}

		/**
		 * One triangle per user editable vertex.
		 */
		public IList<int> GetUserIndices()
		{
			List<int> arr = new List<int>();
			for(int i = 0; i < sharedTriangles.Count; i++)
				arr.Add(sharedTriangles[i][0]);
			return arr;
		}

		public IList<int> GetUserIndices(IList<int> indices)
		{
			List<int> user = new List<int>( indices );

			for(int i = 0; i < user.Count; i++)
				user[i] = triangleLookup[user[i]];

			user = user.Distinct().ToList();

			for(int i = 0; i < user.Count; i++)
				user[i] = sharedTriangles[user[i]][0];

			return user;
		}

		public IList<int> GetAllIndices(IList<int> indices)
		{
			List<int> _ind = new List<int>(indices);

			for(int i = 0; i < indices.Count; i++)
				_ind[i] = triangleLookup[indices[i]];

			_ind = _ind.Distinct().ToList();

			List<int> all = new List<int>(indices);

			for(int i = 0; i < _ind.Count; i++)
				all.AddRange( sharedTriangles[_ind[i]] );

			return all;
		}

		public int ToUserIndex(int triangle)
		{
			return sharedTriangles[triangleLookup[triangle]][0];
		}

		/**
		 * Initialize a new MTMesh with @InGameObject.  Must have a valid meshfilter and mesh.
		 * GameObject will have it's MeshFilter.sharedMesh property set to the clone mesh for editing.
		 * Call MTMesh.Revert() to undo this change (and destroy the cloned mesh in the process).
		 */
		public static MTMesh Create(GameObject InGameObject)
		{
			MTMesh qmesh = ScriptableObject.CreateInstance<MTMesh>();
			qmesh.hideFlags = HideFlags.DontSave;

			qmesh.gameObject = InGameObject;
			qmesh.transform = qmesh.gameObject.transform;

			qmesh.originalMesh = InGameObject.GetComponent<MeshFilter>().sharedMesh;
			qmesh.source = MTEditor_Utility.GetMeshGUID( qmesh.originalMesh, ref qmesh.originalMeshGUID );

			// Copy mesh from InMesh.
			qmesh.cloneMesh = MTMesh_Utility.Clone( InGameObject.GetComponent<MeshFilter>().sharedMesh );
			Undo.RegisterCreatedObjectUndo(qmesh.cloneMesh, "Open Quick Edit");

			Undo.RecordObject(qmesh, "Open Quick Edit");
			qmesh.Apply();

			qmesh.handlesRenderer = (MTHandleRenderer) Undo.AddComponent(InGameObject, typeof(MTHandleRenderer));
			qmesh.handlesRenderer.hideFlags = HideFlags.HideAndDontSave;

			qmesh.handlesRenderer.mesh = new Mesh();
			qmesh.handlesRenderer.mesh.hideFlags = HideFlags.HideAndDontSave;
			qmesh.handlesRenderer.material = null;

			qmesh.CacheElements();

			return qmesh;
		}

		public void CacheElements()
		{
			int vertexCount = cloneMesh.vertexCount;
			_vertexCount = vertexCount;
			_triangleCount = cloneMesh.triangles.Length;

			Vector3[] v = cloneMesh.vertices;

			bool[] assigned = MTUtil.FilledArray(false, vertexCount);

			sharedTriangles = new List<List<int>>();

			bool showProgressBar = vertexCount > 4000;
			
			for(int i = 0; i < vertexCount-1; i++)
			{
				if(assigned[i])
					continue;

				List<int> indices = new List<int>(1) {i};
				for(int n = i+1; n < vertexCount; n++)
				{
					if( v[i] == v[n] )
					{
						indices.Add(n);
						assigned[n] = true;
					}
				}

				if( showProgressBar && i % 1000 == 0)
					EditorUtility.DisplayProgressBar("Optimize Mesh for Editing", "Caching elements...", i/(float)vertexCount);

				sharedTriangles.Add( indices );
			}

		 	if(!assigned[vertexCount-1])
				sharedTriangles.Add( new List<int>() {vertexCount-1} );

			triangleLookup = new Dictionary<int, int>();

			for(int i = 0; i < sharedTriangles.Count; i++)	
			{
				for(int n = 0; n < sharedTriangles[i].Count; n++)
					triangleLookup.Add(sharedTriangles[i][n], i);
			}

			_faces = GetFaces();
			_edges = GetEdges();
			_userEdges = _edges.ToSharedIndex(triangleLookup)
												.Distinct()
												.ToTriangleIndex(sharedTriangles)
												.ToArray();

			EditorUtility.ClearProgressBar();
		}

		/**
		 * Sets the MeshFilter sharedMesh to the cloned mesh.
		 */
		public void Apply()
		{
			if( gameObject != null )
				gameObject.GetComponent<MeshFilter>().sharedMesh = cloneMesh;
		}

		/**
		 * Sets the MeshFilter sharedMesh to the original mesh, and destroys the 
		 * cloned mesh.
		 */
		public void Revert()
		{
			if( cloneMesh != null )
				Undo.DestroyObjectImmediate( cloneMesh );

			if( originalMesh != null && gameObject != null )
			{
				MeshFilter mf = gameObject.GetComponent<MeshFilter>();
				if( mf != null )
					mf.sharedMesh = originalMesh;
			}
		}

		void OnDestroy()
		{
			if( handlesRenderer != null )
				DestroyImmediate( handlesRenderer );
		}

#region Serialization Override

		[SerializeField] List<int> lookup_keys = new List<int>();
		[SerializeField] List<int> lookup_values = new List<int>();
		[SerializeField] List<JaggedArrayContainer> shared_values = new List<JaggedArrayContainer>();

		[System.Serializable]
		class JaggedArrayContainer
		{
			public List<int> value;

			public JaggedArrayContainer(List<int> val)
			{
				value = val;
			}
		}

		public void OnBeforeSerialize()
		{
			lookup_keys.Clear();
			lookup_values.Clear();

			foreach(KeyValuePair<int, int> kvp in triangleLookup)
			{
				lookup_keys.Add(kvp.Key);
				lookup_values.Add(kvp.Value);
			}

			shared_values.Clear();

			for(int i = 0; i < sharedTriangles.Count; i++)
				shared_values.Add( new JaggedArrayContainer(sharedTriangles[i]) );
		}

		public void OnAfterDeserialize()
		{
			triangleLookup = new Dictionary<int, int>();
			sharedTriangles = new List<List<int>>();

			for(int i = 0; i < lookup_keys.Count; i++)
				triangleLookup.Add(lookup_keys[i], lookup_values[i]);

			for(int i = 0; i < shared_values.Count; i++)
				sharedTriangles.Add(shared_values[i].value);
		}
#endregion
	}
}