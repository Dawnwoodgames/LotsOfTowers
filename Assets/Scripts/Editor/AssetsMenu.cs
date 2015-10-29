using LotsOfTowers.Actors;
using UnityEditor;
using UnityEngine;

namespace LotsOfTowers.Unity {
	public static class AssetsMenu {

		[MenuItem("Assets/Create/Onesie")]
		public static void CreateOnesie() {
			ScriptableObject asset = ScriptableObject.CreateInstance<Onesie>();

			AssetDatabase.CreateAsset(asset, "Assets/Resources/NewOnesie.asset");
			AssetDatabase.SaveAssets();
			EditorUtility.FocusProjectWindow();
			Selection.activeObject = asset;
		}
	}
}