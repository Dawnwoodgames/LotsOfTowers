using UnityEngine;

namespace Nimbi.Interaction
{
	public class Lever : MonoBehaviour
	{
		public GameObject pressurePlate;
		private bool inTrigger = false;

		void Update()
		{
			if (Input.GetButtonDown("Submit") && inTrigger)
			{
				//Destroy(pressurePlate.GetComponent<PressurePlateScript>());
				Debug.Log(pressurePlate.name + " inactive");
			}
		}

		private void OnTriggerStay(Collider coll)
		{
			if (coll.gameObject.tag == "Player")
			{
				inTrigger = true;
			}
		}
		private void OnTriggerExit(Collider coll)
		{
			if (coll.gameObject.tag == "Player")
			{
				inTrigger = false;
			}
		}
	}
}