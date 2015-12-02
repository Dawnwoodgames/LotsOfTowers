using UnityEngine;
using System.Collections;
using LotsOfTowers.Actors;

namespace LotsOfTowers.Interaction
{
    public class TowerThreeTopElevator : MonoBehaviour {
		private GameObject player;
		private PlayerController playerController;
		private Vector3 playerDistance;
		private int state;
		private bool triggered;

		public float interpolationSpeed = 1;
		public Vector3 targetMidway;
		public Vector3 targetEnd;

		public void Awake() {
			this.player = GameObject.FindGameObjectWithTag ("Player");
			this.playerController = player.GetComponent<PlayerController>();
			this.state = 0;
		}
		
		public void FixedUpdate() {
			if (triggered) {
				if (state == 0) {
					transform.localPosition = Vector3.Slerp(transform.localPosition, targetMidway, interpolationSpeed * Time.deltaTime);
					player.transform.position = transform.position + playerDistance;
				} else if (state == 1) {
					transform.localPosition = Vector3.Lerp(transform.localPosition, targetEnd, interpolationSpeed * Time.deltaTime);
					player.transform.position = transform.position + playerDistance;
				} else if (state == 2) {
					state = 3;
					transform.localPosition = targetEnd;
					player.transform.position = transform.position + playerDistance;
					playerController.enabled = true;
				}
			}
		}

		public void OnTriggerEnter(Collider coll) {
			if (coll.gameObject.tag == "Player" && state == 0) {
				playerController.enabled = false;
				playerDistance = new Vector3 (
				transform.position.x - player.transform.position.x,
				transform.position.y - player.transform.position.y,
				transform.position.z - player.transform.position.z
				);
				triggered = true;
			}
		}

		public void Update() {
			if (state == 0 && Vector3.Distance(transform.localPosition, targetMidway) < 0.25f) {
				state = 1;
			} else if (state == 1 && Vector3.Distance(transform.localPosition, targetEnd) < 0.1f) {
				state = 2;
			}
		}
    }

}