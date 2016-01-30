using Nimbi.Actors;
using UnityEditor;
using UnityEngine;

namespace Nimbi.Unity {
	public static class AssetsMenu {

		[MenuItem("Assets/Create/Onesie")]
		public static void CreateOnesie() {
			ScriptableObject asset = ScriptableObject.CreateInstance<Onesie>();

			AssetDatabase.CreateAsset(asset, "Assets/Resources/NewOnesie.asset");
			AssetDatabase.SaveAssets();
			EditorUtility.FocusProjectWindow();
			Selection.activeObject = asset;
		}

		[MenuItem("GameObject/3D Object/Ramp")]
		public static void CreateRamp() {
			GameObject ramp = new GameObject();
			ramp.AddComponent<MeshFilter> ();
			ramp.GetComponent<MeshFilter> ().mesh = (Mesh)Resources.Load("Triangle",typeof(Mesh));
			ramp.AddComponent<MeshCollider> ();
			ramp.AddComponent<MeshRenderer> ();
			ramp.GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("Materials/defaultMat");
			ramp.name = "Ramp";
		}
	}
}