using System.Collections;
using UnityEngine;

namespace LotsOfTowers.Interaction.Triggers {
	public class SlideTrigger : MonoBehaviour {
		private bool colliding;
		private float origin;
		private float unit;

		public GameObject Target; // Object that needs to be moved when trigger is called
		public Axis Axis; // The axis the object should move on
		public Direction Direction; // Direction to go along X-axis
		public float Distance; // Distance to move along X-axis
		public float Duration; // How long the animation should last (seconds)
		public bool PlayerOnly; // Depicts whether or not this trigger should react to the player only

		public float Position {
			get { return Axis == Axis.X ? Target.transform.position.x : Target.transform.position.z; }
			set {
				Target.transform.position = new Vector3 (
					Axis == Axis.X ? value : Target.transform.position.x,
					Target.transform.position.y,
					Axis == Axis.Z ? value : Target.transform.position.z
				);
			}
		}

		public float Unit {
			get { return unit * Time.smoothDeltaTime; }
		}

		public void Awake() {
			this.origin = Axis == Axis.X ? Target.transform.position.x : Target.transform.position.z;
			this.unit = Distance / Duration * (int)Direction;
		}

		public void OnCollisionEnter(Collision coll) {
			if (colliding)
				return;

			colliding = true;
			StopAllCoroutines();
			if ((PlayerOnly && coll.gameObject.tag == "Player") || !PlayerOnly) {
				StartCoroutine(SlideToDestination());
			}
		}

		public void OnCollisionExit(Collision coll) {
			if (!colliding)
				return;

			colliding = false;
			StopAllCoroutines();
			if ((PlayerOnly && coll.gameObject.tag == "Player") || !PlayerOnly) {
				StartCoroutine(SlideToOrigin());
			}
		}

		public IEnumerator SlideToDestination() {
			float destination = origin + Distance * (int)Direction;
			bool greaterThan = (int)Direction > 0;

			while (destination != Position) {
				if (greaterThan) {
					Position = (Position + Unit) >= destination ? destination : Position + Unit;
				} else {
					Position = (Position + Unit) <= destination ? destination : Position + Unit;
				}

				yield return null;
			}
		}

		public IEnumerator SlideToOrigin() {
			bool greaterThan = (int)Direction < 0;

			while (Position != origin) {
				if (greaterThan) {
					Position = (Position - Unit) >= origin ? origin : Position - Unit;
				} else {
					Position = (Position - Unit) <= origin ? origin : Position - Unit;
				}

				yield return null;
			}
		}
	}

	public enum Axis {
		X, Z
	}

	public enum Direction {
		Left = -1,
		Right = 1
	}
}