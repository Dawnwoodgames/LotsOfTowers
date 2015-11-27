using LotsOfTowers.Environment;
using UnityEngine;

namespace LotsOfTowers.Interaction.Trigger {
	[RequireComponent(typeof(Collider))]
	public sealed class BuoyGateTrigger : MonoBehaviour {
		private Buoy buoyLeft, buoyRight;
		private bool triggered;

		public void Awake() {
			RaycastHit hit;
			
			if (Physics.Raycast(transform.position, Vector3.left, out hit)) {
				buoyLeft = hit.collider.GetComponent<Buoy>();
			}

			if (Physics.Raycast(transform.position, Vector3.right, out hit)) {
				buoyRight = hit.collider.GetComponent<Buoy>();
			}
		}

		public void OnTriggerEnter() {
			if (triggered || buoyLeft.Red || buoyRight.Red) {
				// not allowed
			}

			triggered = true;
		}
	}
}