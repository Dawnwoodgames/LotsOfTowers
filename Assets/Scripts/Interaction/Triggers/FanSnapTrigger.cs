using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction.Triggers
{
	public class FanSnapTrigger : MonoBehaviour
	{
		public bool isPlaced = false;

		private void OnTriggerEnter(Collider coll)
		{
			if (coll.name == gameObject.name.Replace("Trigger", ""))
			{
				try
				{
					coll.transform.rotation = gameObject.transform.rotation;
					coll.transform.position = gameObject.transform.position;
					coll.GetComponent<Rigidbody>().isKinematic = true;
					coll.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
					coll.GetComponent<LiftingObject>().enabled = false;
					isPlaced = true;
                }
				catch (System.Exception)
				{
					throw;
				}
				
			}
		}
	}
}