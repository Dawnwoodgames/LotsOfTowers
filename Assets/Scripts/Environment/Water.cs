using Nimbi.Actors;
using UnityEngine;

namespace Nimbi.Environment {
	public class Water : MonoBehaviour {
		private GameObject ball;
		private Rigidbody ballRigidBody;
		private RigidbodyConstraints driftConstraints, sinkConstraints;
		private Player player;
		private Rigidbody playerRigidbody;
		private float surfaceHeight;

        public float depthLevel;

		public void Awake() {
			this.driftConstraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
			this.sinkConstraints = RigidbodyConstraints.FreezeRotation;
			this.surfaceHeight = (transform.position.y + transform.localScale.y / 3) + depthLevel;
		}

		public void FixedUpdate() {
			if (ball != null && player != null) {
				if (player.Onesie.isHeavy) {
					// Player is heavy, sink to bottom
					ballRigidBody.useGravity = true;
					playerRigidbody.constraints = sinkConstraints;
					playerRigidbody.useGravity = true;
				} else {
					// Player isn't heavy, drift on surface
					ballRigidBody.useGravity = false;
					ball.transform.position = Vector3.Lerp(ball.transform.position,
						new Vector3(ball.transform.position.x, surfaceHeight, ball.transform.position.z), 0.05f);
					player.transform.position = Vector3.Lerp(player.transform.position,
						new Vector3(player.transform.position.x, surfaceHeight, player.transform.position.z), 0.05f);
					playerRigidbody.constraints = driftConstraints;
					playerRigidbody.useGravity = false;
				}
			} else if (player != null) {
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
			if (coll.gameObject.tag == "HamsterBall" || coll.gameObject.tag == "Player") {
				if (coll.gameObject.tag == "HamsterBall") {
					ball = coll.gameObject;
					ballRigidBody = coll.GetComponent<Rigidbody>();
				} else {
					ball = null;
					ballRigidBody = null;
				}

				player = FindObjectOfType<Player>();
				playerRigidbody = player.GetComponent<Rigidbody>();
			}
		}

		public void OnTriggerExit(Collider coll) {
			if (coll.gameObject.tag == "HamsterBall" || coll.gameObject.tag == "Player") {
                if (ball != null)
                {
                    ball = null;
                    ballRigidBody.useGravity = true;
                }
				player = null;
				playerRigidbody.constraints = sinkConstraints;
				playerRigidbody.useGravity = true;
			}
		}
	}
}