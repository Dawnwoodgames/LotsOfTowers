using UnityEngine;
using Nimbi.Actors;

namespace Nimbi.Interaction
{
	public class PlaceIceBlock : MonoBehaviour
	{
		public GameObject iceBlock;
		public bool complete = false;

		private bool waterGiven = false;

		void OnTriggerStay(Collider coll)
		{
			if (coll.tag == "Player" && Input.GetButtonDown("Submit"))
			{
				if (coll.GetComponent<Player>().HoldingWater)
				{
					coll.GetComponent<Player>().HoldingWater = false;
					waterGiven = true;
				}
			}
		}

		void Update()
		{
			if (waterGiven)
			{
				if (iceBlock.transform.localScale != new Vector3(1.1f, 1.1f, 1.1f))
				{
					iceBlock.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
				}
				else
				{
					iceBlock.tag = "IceBlock";
					waterGiven = false;
					complete = true;
				}
			}
		}
	}
}
