using UnityEngine;

namespace LotsOfTowers.Interaction {
	[RequireComponent(typeof(Collider))]
	public sealed class PressurePlate : MonoBehaviour {
		private bool colliding;
		public Side Side = Side.Top;

		public void OnCollisionEnter(Collision collision) {
			if (colliding) return;
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