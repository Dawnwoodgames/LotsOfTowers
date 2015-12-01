using UnityEngine;

namespace LotsOfTowers.Interaction {
	public sealed class TransitionBeacon : MonoBehaviour {

		public int levelIndex;

		public void OnCollisionEnter(Collision coll) {
			if (coll.gameObject.tag == "Player") {
				GameManager.Instance.LoadLevel(levelIndex, true);
			}
		}
	}
}