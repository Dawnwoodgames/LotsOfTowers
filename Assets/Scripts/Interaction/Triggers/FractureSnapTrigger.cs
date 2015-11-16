using UnityEngine;

namespace LotsOfTowers.Interaction.Triggers
{
	public class FractureSnapTrigger : MonoBehaviour
	{
		public GameObject fracture;

		void OnTriggerEnter(Collider coll)
		{
			if (coll.tag == "Fracture")
			{
				fracture.transform.position = gameObject.transform.position;
				fracture.transform.rotation = new Quaternion(0, 0, 0, 0);
				fracture.GetComponent<Rigidbody>().isKinematic = true;
			}
		}
	}
}