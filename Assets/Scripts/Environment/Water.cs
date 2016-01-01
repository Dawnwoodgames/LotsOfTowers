using LotsOfTowers.Actors;
using UnityEngine;

namespace LotsOfTowers.Environment {
	public class Water : MonoBehaviour {
		private RigidbodyConstraints driftConstraints, sinkConstraints;
		private Player player;
		private Rigidbody playerRigidbody;
		private float surfaceHeight;

		public void Awake() {
			this.driftConstraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
			this.sinkConstraints = RigidbodyConstraints.FreezeRotation;
			this.surfaceHeight = (transform.position.y + transform.localScale.y / 2) - 0.75f;
		}

		public void FixedUpdate() {
			if (player != null) {
				if (player.Onesie.isHeavy) {
					// Player is heavy, sink to bottom
					playerRigidbody.constraints = sinkConstraints;
					playerRigidbody.useGravity = true;
				} else {
					// Player isn't heavy, drift on surface
					player.transform.position = Vector3.Lerp(player.transform.position,
						new Vector3(player.transform.position.x, surfaceHeight, player.transform.position.z), 0.05f);
					playerRigidbody.constraints = driftConstraints;
					playerRigidbody.useGravity = false;
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
				playerRigidbody.constraints = sinkConstraints;
				playerRigidbody.useGravity = true;
			}
		}
	}
}