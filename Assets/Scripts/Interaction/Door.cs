using Nimbi.Platform;
using UnityEngine;

namespace Nimbi.Interaction
{
	public class Door : MonoBehaviour
	{
		public GameObject player;
		public Vector3 unlockedPosition;
		public GameObject key;
		private bool inTrigger = false;
		public GameObject mirrorDoor;

		void Update()
		{
			if (Input.GetButtonDown("Submit") && inTrigger)
				if (player.GetComponentInChildren<MirrorKey>() != null)
                {
                    OpenDoor(key);
                    GameObject.Find("PressurePlate").SetActive(false);
                }
					
                
		}

		private void OnTriggerStay(Collider coll) { inTrigger = true; }
		private void OnTriggerExit() { inTrigger = false; }

		private void OpenDoor(GameObject key)
		{
			Destroy(mirrorDoor);
			Destroy(key);
			Destroy(gameObject);
		}
	}
}
