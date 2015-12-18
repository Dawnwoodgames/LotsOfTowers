using LotsOfTowers.Environment;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LotsOfTowers.Interaction {
	public sealed class WaterMazeManager : MonoBehaviour {
		private List<BuoyGateTrigger> gates;
		private bool respawnOnNext;
        private BookcaseDoor door;

		public void Awake() {
			this.gates = new List<BuoyGateTrigger>();
            door = GameObject.Find("BookcaseDoor").GetComponent<BookcaseDoor>();
		}

		public void GateOpened(BuoyGateTrigger gate, bool hasRedBuoy) {
			if (hasRedBuoy || gates.Contains(gate)) {
				respawnOnNext = true;
			}

			gates.Add(gate);
		}

		public void OnGUI() {
			GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Maze progress: " + gates.Count + " / 22");
		}

		public void Update() {
			if (respawnOnNext || gates.Count > 22) {
				GameManager.Instance.PlayerPassOutAndRespawn(transform);
				gates.Clear();
				respawnOnNext = false;
			}

            if (gates.Count == 22)
            {
                
                FindObjectsOfType<Buoy>().ToList().ForEach(b => Destroy(b));
            }
		}
	}
}