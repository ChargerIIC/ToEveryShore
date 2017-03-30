namespace MoenenGames.Shape {

	using UnityEngine;
#if UNITY_EDITOR
	using UnityEditor;
#endif
	using System.Collections;

	public class Rectangle : Shape {



		#region --- SUB ---





		#endregion



		#region --- VAR ---



		// Shot Cut
		public static Shader RectShader {
			get {
				if (!rectShader) {
					rectShader = Shader.Find("Simple2DShape/Rect");
				}
				return rectShader;
			}
		}
		private static Shader rectShader = null;


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


		protected static int RoundID {
			get {
				if (!roundID.HasValue) {
					roundID = Shader.PropertyToID("_Round");
				}
				return roundID.Value;
			}
		}
		protected static int? roundID = null;


		public float Round {
			get {
				return round;
			}
			set {
				round = Mathf.Clamp(value, 0f, 0.5f);
				if (Inited) {
					ModelMaterial.SetFloat(RoundID, value);
				}
			}
		}


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


		// SerializeField
		[Space(4)]
		[Header("○ Rectangle")]
		[SerializeField]
		[Range(0f, 0.5f)]
		private float round = 0.1f;
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
			ModelMaterial = new Material(RectShader);
			RefreshParams();
		}




#if UNITY_EDITOR


		void OnDrawGizmos () {

			if (EditorApplication.isPlaying) {
				return;
			}


			Handles.color = baseColor;
			float r = Mathf.Min(Round, Mathf.Min(Width * 0.5f, Height * 0.5f));
			Handles.DrawWireArc(transform.position + transform.rotation * new Vector2(Width * 0.5f - r, Height * 0.5f - r), -transform.forward, transform.up, 90f, r);
			Handles.DrawWireArc(transform.position + transform.rotation * new Vector2(Width * 0.5f - r, r - Height * 0.5f), -transform.forward, transform.right, 90f, r);
			Handles.DrawWireArc(transform.position + transform.rotation * new Vector2(r - Width * 0.5f, r - Height * 0.5f), -transform.forward, -transform.up, 90f, r);
			Handles.DrawWireArc(transform.position + transform.rotation * new Vector2(r - Width * 0.5f, Height * 0.5f - r), -transform.forward, -transform.right, 90f, r);
			Handles.DrawLine(
				transform.position + transform.rotation * new Vector2(Width * 0.5f, Height * 0.5f - r),
				transform.position + transform.rotation * new Vector2(Width * 0.5f, r - Height * 0.5f)
			);
			Handles.DrawLine(
				transform.position + transform.rotation * new Vector2(-Width * 0.5f, Height * 0.5f - r),
				transform.position + transform.rotation * new Vector2(-Width * 0.5f, r - Height * 0.5f)
			);
			Handles.DrawLine(
				transform.position + transform.rotation * new Vector2(Width * 0.5f - r, Height * 0.5f),
				transform.position + transform.rotation * new Vector2(r - Width * 0.5f, Height * 0.5f)
			);
			Handles.DrawLine(
				transform.position + transform.rotation * new Vector2(Width * 0.5f - r, -Height * 0.5f),
				transform.position + transform.rotation * new Vector2(r - Width * 0.5f, -Height * 0.5f)
			);

		}
		

#endif


		#endregion
		


		#region --- LGC ---


		public override void RefreshParams () {
			base.RefreshParams();
			Round = round;
			Width = width;
			Height = height;
		}

		
		#endregion


	}


	

#if UNITY_EDITOR


	[CustomEditor(typeof(Rectangle), true)]
	public class RectInspector : ShapeInspector {



		public override void OnInspectorGUI () {
			base.OnInspectorGUI();
			if (GUI.changed && target) {
				(target as Rectangle).RefreshParams();
			}
		}



		protected override void OnSceneGUI () {

			if (EditorApplication.isPlaying || !target) {
				return;
			}

			Rectangle rect = target as Rectangle;
			float dotSize = SceneView.currentDrawingSceneView.size * 0.01f;

			Handles.color = Color.black;
			Vector3 posX = Handles.FreeMoveHandle(
				rect.transform.position + rect.transform.rotation * Vector3.left * rect.Width * 0.5f,
				Quaternion.identity,
				dotSize,
				Vector2.zero,
				Handles.DotCap
			);
			rect.Width = Mathf.Max(0f, Vector3.Distance(posX, rect.transform.position) * 2f);
			Vector3 posY = Handles.FreeMoveHandle(
				rect.transform.position + rect.transform.rotation * Vector3.down * rect.Height * 0.5f,
				Quaternion.identity,
				dotSize,
				Vector2.zero,
				Handles.DotCap
			);
			rect.Height = Mathf.Max(0f, Vector3.Distance(posY, rect.transform.position) * 2f);
			Vector3 posR = Handles.FreeMoveHandle(
				rect.transform.position + rect.transform.rotation * -new Vector3(
					rect.Width * 0.5f,
					rect.Height * 0.5f - rect.Round,
					0f
				),
				Quaternion.identity,
				dotSize,
				Vector2.zero,
				Handles.DotCap
			);
			rect.Round = Mathf.Clamp(
				Vector3.Distance(
					posR, 
					rect.transform.position - rect.transform.up * (rect.Height * 0.5f) - rect.transform.right * (rect.Width * 0.5f)
				), 0f, 0.5f
			);

			serializedObject.FindProperty("width").floatValue = rect.Width;
			serializedObject.FindProperty("height").floatValue = rect.Height;
			serializedObject.FindProperty("round").floatValue = rect.Round;
			serializedObject.ApplyModifiedProperties();

		}


	}


#endif




}