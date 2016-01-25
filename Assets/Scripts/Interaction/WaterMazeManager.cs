using Nimbi.Environment;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction {
	public class WaterMazeManager : MonoBehaviour {
		private GameObject exit;
		private List<BuoyGateTrigger> gates;
        private bool puzzleCompleted;
		private bool respawnOnNext;

		public bool testMode;

		public void Awake() {
			this.exit = GameObject.Find("BookcaseDoor");
			this.gates = new List<BuoyGateTrigger>();
		}

        public int GatesOpened {
            get { return gates == null ? 0 : gates.Count; }
        }

		public void GateOpened(BuoyGateTrigger gate, bool hasRedBuoy) {
			if (hasRedBuoy || gates.Contains(gate)) {
				respawnOnNext = true;
			}

			gates.Add(gate);
		}

        private IEnumerator PuzzleClearedCoroutine()
        {
            List<GameObject> buoys = new List<GameObject>();
            float t = 0;
            GameObject water = GameObject.Find("Segment 2/Water");
            float waterHeight = water.transform.position.y;
            float waterSize = water.transform.localScale.y;

            // Destroy all Buoy scripts & BuoyGateTrigger GameObjects
            FindObjectsOfType<Buoy>().ToList().ForEach( b => { buoys.Add(b.gameObject); Destroy(b); });
            FindObjectsOfType<BuoyGateTrigger>().ToList().ForEach(b => Destroy(b.gameObject));

            Vector3 buoySize = buoys.FirstOrDefault().transform.localScale;

            while (t < 3)
            {
                t += Time.smoothDeltaTime;
                if (t >= 3)
                {
                    buoys.ForEach(b => Destroy(b));
                    Destroy(water);
                    yield return null;
                }
                else
                {
                    buoys.ForEach(b => b.transform.localScale = buoySize - (buoySize * t / 3));
                    water.transform.localScale = new Vector3(water.transform.localScale.x, waterSize - (t / 3 * waterSize), water.transform.localScale.z);
                    water.transform.position = new Vector3(water.transform.position.x, waterHeight - (t / 6 * waterSize), water.transform.position.z);
                    yield return null;
                }
            }

            t = 0;
            
            while (t < 2)
            {
                t += Time.smoothDeltaTime;
                if (t >= 2)
                {
                    exit.transform.localRotation = Quaternion.Euler(new Vector3(exit.transform.localRotation.eulerAngles.x, -50, exit.transform.localRotation.eulerAngles.z));
                } else
                {
                    exit.transform.localRotation = Quaternion.Euler(new Vector3(exit.transform.localRotation.eulerAngles.x, 0 - 25 * t, exit.transform.localRotation.eulerAngles.z));
                }
                yield return null;
            }

            Destroy(gameObject);
        }

		public void OnGUI() {
			GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Maze progress: " + gates.Count + " / " + (testMode ? "1 - Watermaze test mode is enabled; only 1 gate needs to be cleared" : "22"));
		}

		public void Update() {
			if (respawnOnNext || gates.Count > 22) {
                FindObjectsOfType<LineRenderer>().ToList().ForEach(l => Destroy(l));
				GameManager.Instance.PlayerPassOutAndRespawn(transform);
				gates.Clear();
				respawnOnNext = false;
			}

			if ((testMode && gates.Count > 0) || (!testMode && gates.Count == 22))
            {
                if (!puzzleCompleted)
                {
                    FindObjectsOfType<LineRenderer>().ToList().ForEach(l => Destroy(l));
                    puzzleCompleted = true;
                    StartCoroutine(PuzzleClearedCoroutine());
                }
            }
		}
	}
}