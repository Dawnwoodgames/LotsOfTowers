using LotsOfTowers.Environment;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LotsOfTowers.Interaction.Trigger {
	[RequireComponent(typeof(Collider))]
	public sealed class BuoyGateTrigger : MonoBehaviour {
		private static Transform spawnPoint;
		private static List<BuoyGateTrigger> triggers;
		private Buoy buoyLeft, buoyRight;
		private bool triggered;

		public static int BuoysTriggered {
			get { return triggers.Where(b => b.triggered).Count(); }
		}

		static BuoyGateTrigger() {
			try {
				BuoyGateTrigger.spawnPoint = GameObject.Find("Poolside/Spawn Point").transform;
				BuoyGateTrigger.triggers = new List<BuoyGateTrigger>();
			} catch (UnityException) { }
		}

		public static void ResetBuoys() {
			triggers.ForEach(b => b.triggered = false);
		}

		public void Awake() {
			RaycastHit hit;
			
			if (Physics.Raycast(transform.position, Vector3.left, out hit)) {
				buoyLeft = hit.collider.GetComponent<Buoy>();
			}

			if (Physics.Raycast(transform.position, Vector3.right, out hit)) {
				buoyRight = hit.collider.GetComponent<Buoy>();
			}

			triggers.Add(this);
		}

		public void OnTriggerEnter() {
			if (triggered || (buoyLeft != null && buoyLeft.Red) || (buoyRight != null && buoyRight.Red)) {
				// not allowed
			} else {
				triggered = true;
			}
		}
	}
}