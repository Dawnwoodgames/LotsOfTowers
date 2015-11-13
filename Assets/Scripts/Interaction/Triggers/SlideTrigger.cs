using System.Collections;
using UnityEngine;

namespace LotsOfTowers.Interaction.Triggers {
	public class SlideTrigger : TriggerBehaviour {
		private float aX;
		private bool aLock, bLock;

		public GameObject Target; // Object that needs to be moved when trigger is called
		public Direction Direction; // Direction to go along X-axis
		public float Distance; // Distance to move along X-axis
		public float Duration; // How long the animation should last (seconds)

		public void Awake() {
			this.aX = Target.transform.position.x;
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
						Target.transform.position = new Vector3(aX + Distance * (int)Direction, Target.transform.position.y, Target.transform.position.z);
					} else {
						Target.transform.position = new Vector3(aX + (Distance * (t / Duration)) * (int)Direction, Target.transform.position.y, Target.transform.position.z);
					}

					yield return null;
				}
			}
		}
		
		public override IEnumerator TriggerOff(GameObject source) {
			float bX = Target.transform.position.x;
			float dX = (bX - aX) * -1;
			float t = 0;

			if (!bLock) {
				aLock = false;
				bLock = true;
				
				while (bLock && t < Duration) {
					t += Time.smoothDeltaTime;
					
					if (t >= Duration) { // Last cycle
						bLock = false;
						Target.transform.position = new Vector3(aX, Target.transform.position.y, Target.transform.position.z);
					} else {
						Target.transform.position = new Vector3(bX - (dX * (t / Duration)) * (int)Direction, Target.transform.position.y, Target.transform.position.z);
					}
					
					yield return null;
				}
			}
		}
	}

	public enum Direction {
		Left = -1,
		Right = 1
	}
}