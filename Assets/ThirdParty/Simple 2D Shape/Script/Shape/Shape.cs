namespace MoenenGames.Shape {


	using UnityEngine;
#if UNITY_EDITOR
	using UnityEditor;
#endif
	using System.Collections;



	public abstract class Shape : MonoBehaviour {



		#region --- SUB ---


		public enum CullingFadeStyle {
			LeftToRight = 0,
			RightToLeft = 1,
			DownToUp = 2,
			UpToDown = 3,
			Clockwise = 4,
			AntiClockwise = 5,
		}


		#endregion



		#region --- VAR ---


		// Shot Cut


		#region --- ID ---


		private static int SmoothID {
			get {
				if (!smoothID.HasValue) {
					smoothID = Shader.PropertyToID("_Smooth");
				}
				return smoothID.Value;
			}
		}
		private static int? smoothID = null;


		private static int ColorID {
			get {
				if (!colorID.HasValue) {
					colorID = Shader.PropertyToID("_Color");
				}
				return colorID.Value;
			}
		}
		private static int? colorID = null;


		private static int TexScaleID {
			get {
				if (!texScaleID.HasValue) {
					texScaleID = Shader.PropertyToID("_TexScale");
				}
				return texScaleID.Value;
			}
		}
		private static int? texScaleID = null;


		private static int MainTexID {
			get {
				if (!mainTexID.HasValue) {
					mainTexID = Shader.PropertyToID("_MainTex");
				}
				return mainTexID.Value;
			}
		}
		private static int? mainTexID = null;


		private static int MainTex_STID {
			get {
				if (!mainTex_STID.HasValue) {
					mainTex_STID = Shader.PropertyToID("_MainTex_ST");
				}
				return mainTex_STID.Value;
			}
		}
		private static int? mainTex_STID = null;


		private static int FadeID {
			get {
				if (!fadeID.HasValue) {
					fadeID = Shader.PropertyToID("_Fade");
				}
				return fadeID.Value;
			}
		}
		private static int? fadeID = null;


		private static int FadeStyleID {
			get {
				if (!fadeStyleID.HasValue) {
					fadeStyleID = Shader.PropertyToID("_FadeType");
				}
				return fadeStyleID.Value;
			}
		}
		private static int? fadeStyleID = null;


		#endregion



		#region --- Shader Param ---


		public float Smooth {
			get {
				return smooth;
			}
			set {
				smooth = Mathf.Clamp01(value);
				if (Inited) {
					ModelMaterial.SetFloat(SmoothID, value);
				}
			}
		}


		public float TexScale {
			get {
				return texScale;
			}
			set {
				texScale = value;
				if (Inited) {
					ModelMaterial.SetFloat(TexScaleID, value);
				}
			}
		}


		public float Fade {
			get {
				return fade;
			}
			set {
				fade = Mathf.Clamp01(value);
				if (Inited) {
					ModelMaterial.SetFloat(FadeID, value);
				}
			}
		}


		public CullingFadeStyle FadeStyle {
			get {
				return fadeStyle;
			}
			set {
				fadeStyle = value;
				if (Inited) {
					ModelMaterial.SetInt(FadeStyleID, (int)value);
				}
			}
		}


		public Vector4 MainTex_ST {
			get {
				return new Vector4(1, 1, -mainTex_ST.x, -mainTex_ST.y);
			}
			set {
				mainTex_ST = new Vector2(value.z, value.w);
				if (Inited) {
					ModelMaterial.SetVector(MainTex_STID, new Vector4(1, 1, -value.z, -value.w));
				}
			}
		}


		public Texture2D MainTex {
			get {
				return mainTex;
			}
			set {
				mainTex = value;
				if (Inited) {
					ModelMaterial.SetTexture(MainTexID, value);
				}
			}
		}


		public Color BaseColor {
			get {
				return baseColor;
			}
			set {
				baseColor = value;
				if (Inited) {
					ModelMaterial.SetColor(ColorID, value);
				}
			}
		}



		#endregion



		protected Material ModelMaterial {
			get {
				return ShapeMeshRenderer.material;
			}
			set {
				ShapeMeshRenderer.material = value;
				RefreshParams();
			}
		}
		

		// Data
		protected Transform Model;
		protected MeshRenderer ShapeMeshRenderer;
		protected bool Inited = false;


		// Serialize

		[Space(4)]
		[Header("○ Basic")]
		[Space(4)]

		[SerializeField]
		[Tooltip("Basic color of this shape.")]
		protected Color baseColor = new Color(0.1f, 0.1f, 0.1f, 1f);

		[SerializeField]
		[Tooltip("Anti aliasing fuzzy amount.")]
		[Range(0f, 0.5f)]
		protected float smooth = 0.01f;

		[SerializeField]
		[Tooltip("Current fade for culling.")]
		[Range(0f, 1f)]
		protected float fade = 1f;

		[SerializeField]
		[Tooltip("Culling style.")]
		protected CullingFadeStyle fadeStyle = CullingFadeStyle.LeftToRight;



		[Header("○ Texture")]


		[SerializeField]
		[Tooltip("Diffuse Texture of this shape, set to null when you don't need it.")]
		protected Texture2D mainTex = null;

		[SerializeField]
		[Tooltip("Texture tile size.")]
		protected float texScale = 1f;

		[SerializeField]
		[Tooltip("Texture tile offset.")]
		protected Vector2 mainTex_ST = Vector2.zero;



		#endregion



		#region --- MSG ---



		protected virtual void Awake () {

			// System
			Inited = true;

			// Clear
			int len = transform.childCount;
			for (int i = 0; i < len; i++) {
				DestroyImmediate(transform.GetChild(0).gameObject, false);
			}

			// Model
			Model = GameObject.CreatePrimitive(PrimitiveType.Quad).transform;
			Model.SetParent(transform);
			Model.localPosition = Vector3.zero;
			Model.localRotation = Quaternion.identity;
			Model.localScale = Vector3.one;
			DestroyImmediate(Model.GetComponent<Collider>());
			Model.hideFlags = HideFlags.HideInHierarchy;

			// Mesh Renderer
			ShapeMeshRenderer = Model.GetComponent<MeshRenderer>();

			RefreshParams();

		}




		#endregion



		#region --- API ---





		#endregion



		#region --- LGC ---


		public virtual void RefreshParams () {
			BaseColor = baseColor; // Refresh Color
			Smooth = smooth;
			TexScale = texScale;
			MainTex_ST = new Vector4(1, 1, mainTex_ST.x, mainTex_ST.y);
			MainTex = mainTex;
			Fade = fade;
			FadeStyle = fadeStyle;
		}


		#endregion


	}

#if UNITY_EDITOR


	[CustomEditor(typeof(Shape), true)]
	public class ShapeInspector : Editor {


		public override void OnInspectorGUI () {
			base.OnInspectorGUI();
			if (GUI.changed && target) {
				(target as Shape).RefreshParams();
			}
		}


		protected virtual void OnSceneGUI () {

		}



	}


#endif

}