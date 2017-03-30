namespace MoenenGames.Shape {

	using UnityEngine;
	using System.Collections;
#if UNITY_EDITOR
	using UnityEditor;
#endif

	public class Rhombus : Shape {



		#region --- SUB ---





		#endregion



		#region --- VAR ---

		// Shot Cut
		public static Shader RhombusShader {
			get {
				if (!rhombusShader) {
					rhombusShader = Shader.Find("Simple2DShape/Rhombus");
				}
				return rhombusShader;
			}
		}
		private static Shader rhombusShader = null;


		public float Radius {
			get {
				return radius;
			}
			set {
				radius = Mathf.Max(0f, value);
				if (Model) {
					Model.localScale = Vector3.one * value * 2f;
				}
			}
		}


		// SerializeField
		[Space(4)]
		[Header("○ Rhombus")]
		[SerializeField]
		private float radius = 1f;


		#endregion



		#region --- MSG ---


		protected override void Awake () {
			base.Awake();
			ModelMaterial = new Material(RhombusShader);
			RefreshParams();
		}






#if UNITY_EDITOR

		void OnDrawGizmos () {
			if (EditorApplication.isPlaying) {
				return;
			}
			Handles.color = baseColor;
			Handles.RectangleCap(-1, transform.position, transform.rotation * Quaternion.Euler(0, 0, 45), Radius / Mathf.Sqrt(2));
		}

#endif


		#endregion




		#region --- LGC ---


		public override void RefreshParams () {
			base.RefreshParams();
			Radius = radius;
		}


		#endregion


	}




#if UNITY_EDITOR


	[CustomEditor(typeof(Rhombus), true)]
	public class RhombusInspector : ShapeInspector {



		public override void OnInspectorGUI () {
			base.OnInspectorGUI();
			if (GUI.changed && target) {
				(target as Rhombus).RefreshParams();
			}
		}



		protected override void OnSceneGUI () {

			if (EditorApplication.isPlaying || !target) {
				return;
			}

			Rhombus rhombus = target as Rhombus;
			float dotSize = SceneView.currentDrawingSceneView.size * 0.01f;

			Handles.color = Color.black;
			Vector3 pos = Handles.FreeMoveHandle(
				rhombus.transform.position + rhombus.transform.rotation * Vector3.left * rhombus.Radius,
				Quaternion.identity,
				dotSize,
				Vector2.zero,
				Handles.DotCap
			);

			rhombus.Radius = Mathf.Max(0f, Vector3.Distance(pos, rhombus.transform.position));
			serializedObject.FindProperty("radius").floatValue = rhombus.Radius;
			serializedObject.ApplyModifiedProperties();

		}


	}


#endif






}