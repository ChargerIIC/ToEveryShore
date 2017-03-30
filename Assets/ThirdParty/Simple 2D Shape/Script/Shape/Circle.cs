namespace MoenenGames.Shape {


	using UnityEngine;
#if UNITY_EDITOR
	using UnityEditor;
#endif
	using System.Collections;


	public class Circle : Shape {


		

		// Global Shot Cut
		public static Shader CircleShader {
			get {
				if (!circleShader) {
					circleShader = Shader.Find("Simple2DShape/Circle");
				}
				return circleShader;
			}
		}
		private static Shader circleShader = null;


		// Shot Cut
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
		[Header("○ Circle")]
		[SerializeField]
		private float radius = 1f;
		
		

		protected override void Awake () {
			base.Awake();
			ModelMaterial = new Material(CircleShader);
			RefreshParams();
		}



#if UNITY_EDITOR

		void OnDrawGizmos () {
			if (EditorApplication.isPlaying) {
				return;
			}
			Handles.color = baseColor;
			Handles.DrawWireDisc(transform.position, -transform.forward, Radius);
		}

#endif


		

		public override void RefreshParams () {
			base.RefreshParams();
			Radius = radius;
		}

		

	}



#if UNITY_EDITOR


	[CustomEditor(typeof(Circle), true)]
	public class CircleInspector : ShapeInspector {



		public override void OnInspectorGUI () {
			base.OnInspectorGUI();
			if (GUI.changed && target) {
				(target as Circle).RefreshParams();
			}
		}



		protected override void OnSceneGUI () {

			if (EditorApplication.isPlaying || !target) {
				return;
			}

			Circle circle = target as Circle;
			float dotSize = SceneView.currentDrawingSceneView.size * 0.01f;
			
			Handles.color = Color.black;
			Vector3 pos = Handles.FreeMoveHandle(
				circle.transform.position + circle.transform.rotation * Vector3.left * circle.Radius,
				Quaternion.identity,
				dotSize,
				Vector2.zero,
				Handles.DotCap
			);

			circle.Radius = Mathf.Max(0f, Vector3.Distance(pos, circle.transform.position));
			serializedObject.FindProperty("radius").floatValue = circle.Radius;
			serializedObject.ApplyModifiedProperties();

		}


	}


#endif



}