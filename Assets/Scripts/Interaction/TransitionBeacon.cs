using UnityEngine;

namespace LotsOfTowers.Interaction {
	public sealed class TransitionBeacon : MonoBehaviour {

		public int levelIndex;

		public void OnTriggerEnter(Collider coll) {
			if (coll.tag == "Player") {
				GameManager.Instance.LoadLevel(levelIndex, true);
			}
		}
	}
}