using System.Collections;
using UnityEngine;

namespace LotsOfTowers.Interaction.Triggers {
	public class SlideTrigger : TriggerBehaviour {
		private float aX;
		private bool aLock, bLock;

		public GameObject Target; // Object that needs to be moved when trigger is called
		public Axis Axis; // The axis the object should move on
		public Direction Direction; // Direction to go along X-axis
		public float Distance; // Distance to move along X-axis
		public float Duration; // How long the animation should last (seconds)

		public void Awake() {
			this.aX = Axis == Axis.X ? Target.transform.position.x : Target.transform.position.z;
			this.aLock = false;
		}
		
		public override IEnumerator TriggerOn(GameObject source) {
			float t = 0;

			if (!aLock) {
				aLock = true;
				bLock = false;

				while (aLock && t < Duration) {
					t += Time.smoothDeltaTime;

					if (t >= Duration) { // Last cycle
						aLock = false;
						if (Axis == Axis.X) {
							Target.transform.position = new Vector3(aX + Distance * (int)Direction, Target.transform.position.y, Target.transform.position.z);
						} else {
							Target.transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, aX + Distance * (int)Direction);
						}
					} else {
						if (Axis == Axis.X) {
							Target.transform.position = new Vector3(aX + (Distance * (t / Duration)) * (int)Direction, Target.transform.position.y, Target.transform.position.z);
						} else {
							Target.transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, aX + (Distance * (t / Duration)) * (int)Direction);
						}
					}

					yield return null;
				}
			}
		}
		
		public override IEnumerator TriggerOff(GameObject source) {
			float bX = Axis == Axis.X ? Target.transform.position.x : Target.transform.position.z;
			float dX = (bX - aX) * -1;
			float t = 0;

			if (!bLock) {
				aLock = false;
				bLock = true;
				
				while (bLock && t < Duration) {
					t += Time.smoothDeltaTime;
					
					if (t >= Duration) { // Last cycle
						bLock = false;
						if (Axis == Axis.X) {
							Target.transform.position = new Vector3(aX, Target.transform.position.y, Target.transform.position.z);
						} else {
							Target.transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, aX);
						}
					} else {
						if (Axis == Axis.X) {
							Target.transform.position = new Vector3(bX - (dX * (t / Duration)) * (int)Direction, Target.transform.position.y, Target.transform.position.z);
						} else {
							Target.transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, bX - (dX * (t / Duration)) * (int)Direction);
						}
					}
					
					yield return null;
				}
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