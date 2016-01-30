using UnityEngine;

namespace Nimbi.Interaction.Triggers
{
	public class FractureSnapTrigger : MonoBehaviour
	{
		void OnTriggerEnter(Collider coll)
		{
			if (coll.tag == "Fracture")
			{
				coll.transform.position = gameObject.transform.position;
				coll.transform.rotation = new Quaternion(0, 0, 0, 0);
				coll.GetComponent<Rigidbody>().isKinematic = true;
                coll.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                coll.GetComponent<InteractableObjectOutline>().enabled = false;
			}
		}
	}
}