using UnityEngine;
using UnityEditor;

namespace MeshTools
{
	[CustomEditor(typeof(MTHandleRenderer))]
	public class MTHandleRendererEditor : Editor
	{	
		#if UNITY_EDITOR
		void OnEnable()
		{
			if( MTEditor.instance == null )
				DestroyImmediate( (MTHandleRenderer)target );
		}
		#endif

		bool HasFrameBounds() 
		{
			return MTEditor.GetSelectedVerticesInWorldSpace().Length > 0;
		}

		Bounds OnGetFrameBounds()
		{
			Vector3[] vertices = MTEditor.GetSelectedVerticesInWorldSpace();

			Vector3 min = Vector3.zero, max = Vector3.zero;

			min = vertices[0];
			max = min;

			for(int i = 1; i < vertices.Length; i++)
			{
				min.x = Mathf.Min(vertices[i].x, min.x);
				max.x = Mathf.Max(vertices[i].x, max.x);

				min.y = Mathf.Min(vertices[i].y, min.y);
				max.y = Mathf.Max(vertices[i].y, max.y);

				min.z = Mathf.Min(vertices[i].z, min.z);
				max.z = Mathf.Max(vertices[i].z, max.z);
			}

			return new Bounds( (min+max)/2f, max != min ? max-min : Vector3.one * .1f );
		}
	}
}