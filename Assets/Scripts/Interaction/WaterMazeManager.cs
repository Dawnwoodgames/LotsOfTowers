using LotsOfTowers.Environment;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction {
	public class WaterMazeManager : MonoBehaviour {
		private GameObject exit;
		private List<BuoyGateTrigger> gates;
		private bool respawnOnNext;

		public bool testMode;

		public void Awake() {
			this.exit = GameObject.Find("BookcaseDoor");
			this.gates = new List<BuoyGateTrigger>();
		}

		public void GateOpened(BuoyGateTrigger gate, bool hasRedBuoy) {
			if (hasRedBuoy || gates.Contains(gate)) {
				respawnOnNext = true;
			}

			gates.Add(gate);
		}

		private IEnumerator OpenExit() {
			while (exit.transform.rotation.eulerAngles.y > 100) {
				exit.transform.Rotate(new Vector3(0, 10 * Time.smoothDeltaTime, 0));
				yield return null;
			}
		}

		public void OnGUI() {
			GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Maze progress: " + gates.Count + " / " + (testMode ? "1 - Watermaze test mode is enabled; only 1 gate needs to be cleared" : "22"));
		}

		public void Update() {
			if (respawnOnNext || gates.Count > 22) {
				GameManager.Instance.PlayerPassOutAndRespawn(transform);
				gates.Clear();
				respawnOnNext = false;
			}

			if ((testMode && gates.Count > 0) || (!testMode && gates.Count == 22))
            {
				FindObjectsOfType<Buoy>().ToList().ForEach(b => Destroy(b.gameObject));
				FindObjectsOfType<BuoyGateTrigger>().ToList().ForEach(b => Destroy(b.gameObject));
				StartCoroutine(OpenExit());
            }
		}
	}
}