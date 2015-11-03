using LotsOfTowers.Actors;
using UnityEditor;

namespace LotsOfTowers.Unity {
	[CustomEditor(typeof(Actor))] public sealed class ActorHelper : Editor {

		public override void OnInspectorGUI ()
		{
			Actor actor = (Actor)target;

			base.OnInspectorGUI ();
			EditorGUILayout.LabelField("Current onesie", actor.Onesie == null ? "<null>" : actor.Onesie.name);
		}
	}
}