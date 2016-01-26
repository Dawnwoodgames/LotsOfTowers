using Nimbi.Environment;
using UnityEngine;

namespace Nimbi.Interaction
{

	[RequireComponent(typeof(BoxCollider))]
	public class BuoyGateTrigger : MonoBehaviour
    {
		private WaterMazeManager manager;
		private Buoy leftBuoy, rightBuoy;

		public float delay = 0.5f; // Seconds between triggers
        public Material lineMaterial;

		public void Awake() {
			RaycastHit hit;

			GetComponent<BoxCollider>().isTrigger = true;
			manager = FindObjectOfType<WaterMazeManager>();

			// Try and find a buoy on the left
			Physics.Raycast(transform.position, transform.rotation * (Vector3.left * 5), out hit);
			leftBuoy = hit.collider.GetComponent<Buoy>();

			// Try and find a buoy on the right
			Physics.Raycast(transform.position, transform.rotation * (Vector3.right * 5), out hit);
			rightBuoy = hit.collider.GetComponent<Buoy>();
		}

		public void OnTriggerEnter(Collider coll) {
			if (coll.gameObject.name != "GateTrigger") {
				return;
			}
            try
            {
                manager.GateOpened(this, leftBuoy.red || rightBuoy.red);
                if (!(leftBuoy.red || rightBuoy.red)) {
                    LineRenderer line = gameObject.AddComponent<LineRenderer>();

                    line.material = lineMaterial;
                    line.SetPosition(0, leftBuoy.transform.position);
                    line.SetPosition(1, rightBuoy.transform.position);
                }
            }
            catch (System.Exception)
            {
                manager.GateOpened(this, true);
            }
		}

		public void OnTriggerExit(Collider coll) {
			
		}

		public void Update() {
            
		}
	}
}