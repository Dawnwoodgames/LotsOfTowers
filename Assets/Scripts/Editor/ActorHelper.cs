using LotsOfTowers.Actors;
using UnityEditor;
using UnityEngine;

namespace LotsOfTowers.Unity {
	[CustomEditor(typeof(Player))] public class ActorHelper : Editor {

		public override void OnInspectorGUI()
		{
			Player actor = (Player)target;

			// Shows current onesie
			EditorGUILayout.LabelField("Current onesie", actor.Onesie == null ? "<null>" : actor.Onesie.name);
			EditorUtility.SetDirty(target);

			// Shows static charge
			EditorGUI.ProgressBar(EditorGUILayout.BeginVertical(), actor.StaticCharge / 100, "Static Charge");
			GUILayout.Space(16);
			EditorGUILayout.EndVertical();

			EditorGUILayout.Separator();
            base.OnInspectorGUI();
		}
	}
}