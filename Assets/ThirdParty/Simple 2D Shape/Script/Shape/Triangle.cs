namespace MoenenGames.Shape {

	using UnityEngine;
#if UNITY_EDITOR
	using UnityEditor;
#endif
	using System.Collections;

	public class Triangle : Shape {
		


		#region --- VAR ---


		// Global Shot Cut
		public static Shader TriangleShader {
			get {
				if (!triangleShader) {
					triangleShader = Shader.Find("Simple2DShape/Triangle");
				}
				return triangleShader;
			}
		}
		private static Shader triangleShader = null;


		// Shot Cut
		public float Size {
			get {
				return size;
			}
			set {
				size = Mathf.Max(0f, value);
				if (Model) {
					Model.localScale = Vector3.one * value * 2f;
				}
			}
		}


		// SerializeField
		[Space(4)]
		[Header("○ Triangle")]
		[SerializeField]
		private float size = 1f;



		#endregion



		protected override void Awake () {
			base.Awake();
			ModelMaterial = new Material(TriangleShader);
			RefreshParams();
		}



#if UNITY_EDITOR

		void OnDrawGizmos () {
			if (EditorApplication.isPlaying) {
				return;
			}
			Handles.color = baseColor;
			Handles.DrawLine(
				transform.position + transform.rotation * new Vector3(-1f, -1f, 0) * Size,
				transform.position + transform.rotation * new Vector3(1f, -1f, 0) * Size
			);
			Handles.DrawLine(
				transform.position + transform.rotation * new Vector3(1f, -1f, 0) * Size,
				transform.position + transform.rotation * new Vector3(0f, 1f, 0) * Size
			);
			Handles.DrawLine(
				transform.position + transform.rotation * new Vector3(0f, 1f, 0) * Size,
				transform.position + transform.rotation * new Vector3(-1f, -1f, 0) * Size
			);
		}

#endif




		public override void RefreshParams () {
			base.RefreshParams();
			Size = size;
		}





	}






#if UNITY_EDITOR


	[CustomEditor(typeof(Triangle), true)]
	public class TriangleInspector : ShapeInspector {



		public override void OnInspectorGUI () {
			base.OnInspectorGUI();
			if (GUI.changed && target) {
				(target as Triangle).RefreshParams();
			}
		}



		protected override void OnSceneGUI () {

			if (EditorApplication.isPlaying || !target) {
				return;
			}

			Triangle tri = target as Triangle;
			float dotSize = SceneView.currentDrawingSceneView.size * 0.01f;

			Handles.color = Color.black;
			Vector3 pos = Handles.FreeMoveHandle(
				tri.transform.position + tri.transform.rotation * Vector3.down * tri.Size,
				Quaternion.identity,
				dotSize,
				Vector2.zero,
				Handles.DotCap
			);

			tri.Size = Mathf.Max(0f, Vector3.Distance(pos, tri.transform.position));
			serializedObject.FindProperty("size").floatValue = tri.Size;
			serializedObject.ApplyModifiedProperties();

		}


	}


#endif




}