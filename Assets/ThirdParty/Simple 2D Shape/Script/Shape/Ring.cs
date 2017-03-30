namespace MoenenGames.Shape {

	using UnityEngine;
#if UNITY_EDITOR
	using UnityEditor;
#endif
	using System.Collections;

	public class Ring : Circle {



		#region --- SUB ---





		#endregion



		#region --- VAR ---


		// Shot Cut
		public static Shader RingShader {
			get {
				if (!ringShader) {
					ringShader = Shader.Find("Simple2DShape/Ring");
				}
				return ringShader;
			}
		}
		private static Shader ringShader = null;


		protected static int ThicknessID {
			get {
				if (!thicknessID.HasValue) {
					thicknessID = Shader.PropertyToID("_Thickness");
				}
				return thicknessID.Value;
			}
		}
		protected static int? thicknessID = null;

		
		public float Thickness {
			get {
				return thickness;
			}
			set {
				thickness = Mathf.Clamp(value, 0f, 0.5f);
				if (Inited) {
					ModelMaterial.SetFloat(ThicknessID, value);
				}
			}
		}


		// SerializeField
		[Space(4)]
		[Header("○ Ring")]
		[SerializeField]
		private float thickness = 0.6f;


		#endregion



		#region --- MSG ---


		protected override void Awake () {
			base.Awake();
			ModelMaterial = new Material(RingShader);
			RefreshParams();
		}



#if UNITY_EDITOR

		void OnDrawGizmos () {

			if (EditorApplication.isPlaying) {
				return;
			}
			
			float innerR = Radius - 2f * Radius * Thickness;
			
			Handles.color = baseColor;
			Handles.DrawWireDisc(transform.position, -transform.forward, Radius);
			Handles.DrawWireDisc(transform.position, -transform.forward, innerR);
			
		}

#endif


#endregion



		#region --- API ---





		#endregion



		#region --- LGC ---

		public override void RefreshParams () {
			base.RefreshParams();
			Thickness = thickness;
		}



		#endregion


	}




#if UNITY_EDITOR


	[CustomEditor(typeof(Ring), true)]
	public class RingInspector : CircleInspector {



		public override void OnInspectorGUI () {
			base.OnInspectorGUI();
			if (GUI.changed && target) {
				(target as Ring).RefreshParams();
			}
		}



		protected override void OnSceneGUI () {

			if (EditorApplication.isPlaying || !target) {
				return;
			}
			
			Ring ring = target as Ring;
			float dotSize = SceneView.currentDrawingSceneView.size * 0.01f;
			float innerR = ring.Radius - 2f * ring.Radius * ring.Thickness;
			
			Handles.color = Color.black;
			Vector3 pos = Handles.FreeMoveHandle(
				ring.transform.position + ring.transform.rotation * Vector3.down * ring.Radius,
				Quaternion.identity,
				dotSize,
				Vector2.zero,
				Handles.DotCap
			);
			ring.Radius = Mathf.Max(innerR, Vector3.Distance(pos, ring.transform.position));


			Vector3 inPos = Handles.FreeMoveHandle(
				ring.transform.position + ring.transform.rotation * Vector3.left * innerR,
				Quaternion.identity,
				dotSize,
				Vector2.zero,
				Handles.DotCap
			);
			ring.Thickness = Mathf.Clamp(
				(Vector3.Distance(ring.transform.position, inPos) - ring.Radius) / (-2f) / Mathf.Max(ring.Radius, float.Epsilon), 
				0f, 
				0.5f
			);

			serializedObject.FindProperty("radius").floatValue = ring.Radius;
			serializedObject.FindProperty("thickness").floatValue = ring.Thickness;
			serializedObject.ApplyModifiedProperties();

		}


	}


#endif




}