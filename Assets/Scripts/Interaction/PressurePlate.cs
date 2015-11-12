using UnityEngine;

namespace LotsOfTowers.Interaction {
	[RequireComponent(typeof(Collider))]
	public sealed class PressurePlate : MonoBehaviour {
		private bool colliding;
		public Side Side = Side.Top;

		public void OnCollisionEnter(Collision collision) {
			if (colliding) return;
			colliding = true;

			bool trigger;

			if (Side == Side.Bottom || Side == Side.Top) {
				float dY = collision.gameObject.GetComponent<Collider>().bounds.extents.y - GetComponent<Collider>().bounds.extents.y;
				trigger = (dY < 0 && Side == Side.Top) || (dY > 0 && Side == Side.Bottom);
			} else if (Side == Side.Left || Side == Side.Right) {
				float dX = collision.gameObject.GetComponent<Collider>().bounds.extents.x - GetComponent<Collider>().bounds.extents.x;
				trigger = (dX < 0 && Side == Side.Right) || (dX > 0 && Side == Side.Left);
			} else {
				float dZ = collision.gameObject.GetComponent<Collider>().bounds.extents.z - GetComponent<Collider>().bounds.extents.z;
				trigger = (dZ < 0 && Side == Side.Front) || (dZ > 0 && Side == Side.Behind);
			}

			if (trigger) {
				foreach (TriggerBehaviour triggerBehaviour in GetComponents<TriggerBehaviour>()) {
					StartCoroutine(triggerBehaviour.TriggerOn(collision.gameObject));
				}
			}
		}

		public void OnCollisionExit(Collision collision) {
			if (colliding) return;
			colliding = true;

			bool trigger;
			
			if (Side == Side.Bottom || Side == Side.Top) {
				float dY = collision.gameObject.GetComponent<Collider>().bounds.extents.y - GetComponent<Collider>().bounds.extents.y;
				trigger = (dY < 0 && Side == Side.Top) || (dY > 0 && Side == Side.Bottom);
			} else if (Side == Side.Left || Side == Side.Right) {
				float dX = collision.gameObject.GetComponent<Collider>().bounds.extents.x - GetComponent<Collider>().bounds.extents.x;
				trigger = (dX < 0 && Side == Side.Right) || (dX > 0 && Side == Side.Left);
			} else {
				float dZ = collision.gameObject.GetComponent<Collider>().bounds.extents.z - GetComponent<Collider>().bounds.extents.z;
				trigger = (dZ < 0 && Side == Side.Front) || (dZ > 0 && Side == Side.Behind);
			}
			
			if (trigger) {
				foreach (TriggerBehaviour triggerBehaviour in GetComponents<TriggerBehaviour>()) {
					StartCoroutine(triggerBehaviour.TriggerOff(collision.gameObject));
				}
			}
		}

		public void Update() {
			colliding = false;
		}
	}
	
	public enum Side {
		Top,
		Bottom,

		Left,
		Right,

		Front,
		Behind
	}
}