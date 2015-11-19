﻿using UnityEngine;
using System.Collections;
using LotsOfTowers.Actors;

namespace LotsOfTowers.Interaction.Triggers {
	public class ElevatorTrigger : MonoBehaviour {

		private bool hasStarted;

		public float Delay = 0.5f; // Delay before the animation starts, after it's been triggered (seconds)
		public float Duration = 1.5f; // Animation duration (seconds)

		public GameObject DownwardsObject; // Object that will be moved downwards
		public float DownwardDistance; // Distance said object will move along Y-axis

		public GameObject UpwardsObject; // Object that will be moved upwards
		public float UpwardDistance; // Distance said object will move along Y-axis

		public void OnCollisionStay(Collision coll) {
			GameObject source = coll.gameObject;

			if (!hasStarted && source.tag == "Player" && source.GetComponent<Player>().IsElephant) {
                GameObject.Find("Level").GetComponent<Level.LevelTwo>().ModifySpawnPoint();
				hasStarted = true;
				StartCoroutine (Trigger());
			}
		}

		public IEnumerator Trigger ()
		{
			float t0 = 0, t1 = 0, y0 = 0, y1 = 0;

			while (t0 < Delay) {
				t0 += Time.smoothDeltaTime;
				yield return null;
			}

			y0 = DownwardsObject.transform.position.y;
			y1 = UpwardsObject.transform.position.y;

			while (t1 < Duration) {
				t1 += Time.smoothDeltaTime;

				if (t1 >= Duration) {
					DownwardsObject.transform.position = new Vector3(DownwardsObject.transform.position.x, y0 - DownwardDistance, DownwardsObject.transform.position.z);
					UpwardsObject.transform.position = new Vector3(UpwardsObject.transform.position.x, y1 + UpwardDistance, UpwardsObject.transform.position.z);
				} else {
					DownwardsObject.transform.position = new Vector3(DownwardsObject.transform.position.x, y0 - (DownwardDistance * (t1 / Duration)), DownwardsObject.transform.position.z);
					UpwardsObject.transform.position = new Vector3(UpwardsObject.transform.position.x, y1+ (UpwardDistance * (t1 / Duration)), UpwardsObject.transform.position.z);
				}

				yield return null;
			}
		}
	}
}