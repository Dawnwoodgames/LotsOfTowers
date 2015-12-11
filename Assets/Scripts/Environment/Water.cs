using LotsOfTowers.Actors;
using UnityEngine;

namespace LotsOfTowers.Environment {
	public sealed class Water : MonoBehaviour {
		private Player player;
		private Rigidbody playerRigidbody;
		private float surfaceHeight;

		public void Awake() {
			this.surfaceHeight = (transform.position.y + transform.localScale.y / 2) - 0.75f;
		}

		public void FixedUpdate() {
			if (player != null) {
				if (player.Onesie.isHeavy) {
					// Player is heavy, sink to bottom
					playerRigidbody.isKinematic = false;
				} else {
					// Player isn't heavy, drift on surface
					player.transform.position = Vector3.Lerp(player.transform.position,
						new Vector3(player.transform.position.x, surfaceHeight, player.transform.position.z), 0.05f);
					playerRigidbody.isKinematic = true;
				}
			}
		}

		public void OnTriggerEnter(Collider coll) {
			if (coll.gameObject.tag == "Player") {
				player = coll.GetComponent<Player>();
				playerRigidbody = coll.GetComponent<Rigidbody>();
			}
		}

		public void OnTriggerExit(Collider coll) {
			if (coll.gameObject.tag == "Player") {
				player = null;
				playerRigidbody.isKinematic = false;
			}
		}
	}
}