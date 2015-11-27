using UnityEngine;

namespace LotsOfTowers.Interaction.Triggers {
	[RequireComponent(typeof(Collider))]
	public sealed class FadeOutTrigger : MonoBehaviour {
		private bool active;

		public Transform spawnPoint;

		public void OnCollisionEnter(Collision coll) {
			if (coll.gameObject.tag == "xPlayer" && !active) {
				active = true;
				// @TODO: add FadeOut (in 3D_Menu branch)
				coll.gameObject.transform.position = spawnPoint.position;
				coll.gameObject.transform.rotation = spawnPoint.rotation;

				active = false;
			}
		}
	}
}