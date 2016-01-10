using UnityEngine;

namespace Nimbi.Interaction {
	[RequireComponent(typeof(Collider))]
	public class PressurePlate : MonoBehaviour {
		private bool colliding;

		public void OnCollisionEnter(Collision collision) {
			if (colliding || collision.gameObject.tag != "Player") return;
			colliding = true;

			foreach (TriggerBehaviour triggerBehaviour in GetComponents<TriggerBehaviour>()) {
				StartCoroutine(triggerBehaviour.TriggerOn (collision.gameObject));
			}
		}

		public void OnCollisionExit(Collision collision) {
			if (colliding) return;
			colliding = true;

			foreach (TriggerBehaviour triggerBehaviour in GetComponents<TriggerBehaviour>()) {
				StartCoroutine(triggerBehaviour.TriggerOff(collision.gameObject));
			}
		}

		public void Update() {
			colliding = false;
		}
	}
}