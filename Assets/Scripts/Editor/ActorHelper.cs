using LotsOfTowers.Actors;
using UnityEditor;

namespace LotsOfTowers.Unity {
	[CustomEditor(typeof(Player))] public sealed class ActorHelper : Editor {

		public override void OnInspectorGUI ()
		{
			Player actor = (Player)target;

			base.OnInspectorGUI ();
			EditorGUILayout.LabelField("Current onesie", actor.Onesie == null ? "<null>" : actor.Onesie.name);
		}
	}
}