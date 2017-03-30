namespace MoenenGames.Shape {

	using UnityEngine;
	using System.Collections;
#if UNITY_EDITOR
	using UnityEditor;
#endif

	public class Parallelogram : Shape {



		#region --- SUB ---





		#endregion



		#region --- VAR ---


		// Shot Cut
		public static Shader ParaShader {
			get {
				if (!paraShader) {
					paraShader = Shader.Find("Simple2DShape/Parallelogram");
				}
				return paraShader;
			}
		}
		private static Shader paraShader = null;






		protected static int WidthID {
			get {
				if (!widthID.HasValue) {
					widthID = Shader.PropertyToID("_Width");
				}
				return widthID.Value;
			}
		}
		protected static int? widthID = null;


		protected static int HeightID {
			get {
				if (!heightID.HasValue) {
					heightID = Shader.PropertyToID("_Height");
				}
				return heightID.Value;
			}
		}
		protected static int? heightID = null;


		public float Width {
			get {
				return width;
			}
			set {
				width = Mathf.Clamp(value, 0f, 1f);
				if (Inited) {
					ModelMaterial.SetFloat(WidthID, value);
				}
			}
		}


		public float Height {
			get {
				return height;
			}
			set {
				height = Mathf.Clamp(value, 0f, 1f);
				if (Inited) {
					ModelMaterial.SetFloat(HeightID, value);
				}
			}
		}



		// Parallelogram
		[Space(4)]
		[Header("○ Parallelogram")]
		[SerializeField]
		[Range(0f, 1f)]
		private float width = 1f;
		[SerializeField]
		[Range(0f, 1f)]
		private float height = 1f;


		#endregion



		#region --- MSG ---


		protected override void Awake () {
			base.Awake();
			ModelMaterial = new Material(ParaShader);
			base.RefreshParams();
		}




#if UNITY_EDITOR


		void OnDrawGizmos () {

			if (EditorApplication.isPlaying) {
				return;
			}


			Handles.color = baseColor;
			Handles.DrawLines(new Vector3[] {
				transform.position + transform.rotation * new Vector3(-0.5f, - 0.5f * Height),
				transform.position + transform.rotation * new Vector3(0.5f - Width, + 0.5f * Height),

				transform.position + transform.rotation * new Vector3(0.5f - Width,0.5f * Height),
				transform.position + transform.rotation * new Vector3(0.5f, 0.5f * Height),

				transform.position + transform.rotation * new Vector3(0.5f, 0.5f * Height),
				transform.position + transform.rotation * new Vector3(Width-0.5f, - 0.5f * Height),

				transform.position + transform.rotation * new Vector3(Width-0.5f, - 0.5f * Height),
				transform.position + transform.rotation * new Vector3(-0.5f,- 0.5f * Height),
			});





		}


#endif


		#endregion



		#region --- API ---


		public override void RefreshParams () {
			base.RefreshParams();
			Width = width;
			Height = height;
		}


		#endregion



		#region --- LGC ---





		#endregion


	}



#if UNITY_EDITOR


	[CustomEditor(typeof(Parallelogram), true)]
	public class ParallelogramInspector : ShapeInspector {



		public override void OnInspectorGUI () {
			base.OnInspectorGUI();
			if (GUI.changed && target) {
				(target as Parallelogram).RefreshParams();
			}
		}



		protected override void OnSceneGUI () {

			if (EditorApplication.isPlaying || !target) {
				return;
			}

			Parallelogram pa = target as Parallelogram;
			float dotSize = SceneView.currentDrawingSceneView.size * 0.01f;

			Handles.color = Color.black;
			Vector3 pos = Handles.FreeMoveHandle(
				pa.transform.position + pa.transform.rotation * new Vector3(0.5f - pa.Width, 0.5f * pa.Height),
				Quaternion.identity,
				dotSize,
				Vector2.zero,
				Handles.DotCap
			);
			pos.x = Mathf.Clamp(pos.x, pa.transform.position.x - 0.5f, pa.transform.position.x + 0.5f);
			pos.y = Mathf.Clamp(pos.y, pa.transform.position.y, pa.transform.position.y + 0.5f);
			pa.Width = Mathf.Max(0f, Vector3.Distance(pos, pa.transform.position + pa.transform.rotation * new Vector3(0.5f, 0.5f * pa.Height, 0f)));
			pa.Height = Mathf.Max(0f, Vector3.Distance(pos, pa.transform.position + pa.transform.rotation * new Vector3(0.5f - pa.Width, 0, 0)) * 2f);

			serializedObject.FindProperty("width").floatValue = pa.Width;
			serializedObject.FindProperty("height").floatValue = pa.Height;
			serializedObject.ApplyModifiedProperties();

		}


	}


#endif


}