using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LotsOfTowers.Interaction.Triggers
{
	public class BrickPuzzleTrigger : MonoBehaviour
	{
		private List<Transform> fractures = new List<Transform>();
		public GameObject floatingFloor;
		public GameObject snapArea;

		private void Start()
		{
			foreach (Transform child in floatingFloor.transform)
			{
				fractures.Add(child);
			}
		}
		void OnTriggerEnter(Collider coll)
		{
			if (coll.tag == "Player")
			{
				foreach (Transform child in fractures)
				{
					Rigidbody childRigid = child.GetComponent<Rigidbody>();

					childRigid.isKinematic = false;
					child.localScale = new Vector3(0.4f, 0.4f, 0.4f);

					childRigid.AddForce(transform.up * 923, ForceMode.Acceleration);
					childRigid.AddForce(child.forward * 291, ForceMode.Acceleration);

					childRigid.useGravity = true;
				}

				//Disable trigger to add force to fracments
				GetComponent<BoxCollider>().enabled = false;

				StartCoroutine(ActivateFrozenFractures());
			}
		}

		private IEnumerator ActivateFrozenFractures()
		{
			yield return new WaitForSeconds(2);

			snapArea.SetActive(true);
			foreach (Transform child in fractures)
			{
				child.GetComponent<Rigidbody>().mass = 0.1f;
			}
		}
	}
}
