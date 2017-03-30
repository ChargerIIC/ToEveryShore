namespace MoenenGames.Shape {

	using UnityEngine;
	using UnityEditor;
	using System.Collections;
	using System.Collections.Generic;

	public class Simple2DEditor {



		private static List<Transform> Created = new List<Transform>();



		#region --- Static ---

		[MenuItem("GameObject/2D Object/S2S-Static/Circle")]
		public static void CreateStaticCircle () {
			CreateSimpleShape("Circle", Circle.CircleShader);
		}


		[MenuItem("GameObject/2D Object/S2S-Static/Parallelogram")]
		public static void CreateStaticParallelogram () {
			CreateSimpleShape("Parallelogram", Parallelogram.ParaShader);
		}


		[MenuItem("GameObject/2D Object/S2S-Static/Rect")]
		public static void CreateStaticRect () {
			CreateSimpleShape("Rect", Rectangle.RectShader);
		}


		[MenuItem("GameObject/2D Object/S2S-Static/Rhombus")]
		public static void CreateStaticRhombus () {
			CreateSimpleShape("Rhombus", Rhombus.RhombusShader);
		}


		[MenuItem("GameObject/2D Object/S2S-Static/Ring")]
		public static void CreateStaticRing () {
			CreateSimpleShape("Ring", Ring.RingShader);
		}


		[MenuItem("GameObject/2D Object/S2S-Static/Triangle")]
		public static void CreateStaticTriangle () {
			CreateSimpleShape("Triangle", Triangle.TriangleShader);
		}


		#endregion



		#region --- Code ---


		[MenuItem("GameObject/2D Object/S2S-Code/Circle")]
		public static void CreateCodeCircle () {
			CreateCodeBasedShape<Circle>("Circle");
		}


		[MenuItem("GameObject/2D Object/S2S-Code/Parallelogram")]
		public static void CreateCodeParallelogram () {
			CreateCodeBasedShape<Parallelogram>("Parallelogram");
		}


		[MenuItem("GameObject/2D Object/S2S-Code/Rect")]
		public static void CreateCodeRect () {
			CreateCodeBasedShape<Rectangle>("Rect");
		}


		[MenuItem("GameObject/2D Object/S2S-Code/Rhombus")]
		public static void CreateCodeRhombus () {
			CreateCodeBasedShape<Rhombus>("Rhombus");
		}


		[MenuItem("GameObject/2D Object/S2S-Code/Ring")]
		public static void CreateCodeRing () {
			CreateCodeBasedShape<Ring>("Ring");
		}


		[MenuItem("GameObject/2D Object/S2S-Code/Triangle")]
		public static void CreateCodeTriangle () {
			CreateCodeBasedShape<Triangle>("Triangle");
		}




		#endregion


		private static void CreateCodeBasedShape<T> (string name) where T : Shape {
			if (Selection.transforms.Length > 0) {
				for (int i = 0; i < Selection.transforms.Length; i++) {
					Transform parent = Selection.transforms[i];
					if (Created.Contains(parent)) {
						continue;
					}
					SpawnCodeShape<T>(name, parent);
					Created.Add(parent);
				}
			} else {
				SpawnCodeShape<T>(name);
			}
			EditorApplication.delayCall += () => { Created.Clear(); };
		}



		private static void SpawnCodeShape<T> (string name, Transform parent = null) where T : Shape {
			Transform tf = new GameObject(name, typeof(T)).transform;
			tf.SetParent(parent);
			tf.localPosition = Vector3.zero;
			tf.localRotation = Quaternion.identity;
			tf.localScale = Vector3.one;
			Undo.RegisterCreatedObjectUndo(tf.gameObject, "Create " + name);
		}


		private static void CreateSimpleShape (string name, Shader shader) {
			if (Selection.transforms.Length > 0) {
				for (int i = 0; i < Selection.transforms.Length; i++) {
					Transform parent = Selection.transforms[i];
					if (Created.Contains(parent)) {
						continue;
					}
					SpawnShape(name,shader, parent);
					Created.Add(parent);
				}
			} else {
				SpawnShape(name, shader);
			}
			EditorApplication.delayCall += () => { Created.Clear(); };
		}



		private static void SpawnShape (string name, Shader shader, Transform parent = null) {
			Transform tf = GameObject.CreatePrimitive(PrimitiveType.Quad).transform;
			tf.name = name;
			tf.SetParent(parent);
			tf.localPosition = Vector3.zero;
			tf.localRotation = Quaternion.identity;
			tf.localScale = Vector3.one;
			Object.DestroyImmediate(tf.GetComponent<Collider>());
			Material mat = new Material(shader);
			tf.GetComponent<MeshRenderer>().material = mat;
			Undo.RegisterCreatedObjectUndo(tf.gameObject, "Create " + name);
		}



	}
}