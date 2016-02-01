using UnityEngine;
using Nimbi.Actors;

namespace Nimbi.Interaction
{
	public class PlaceIceBlock : MonoBehaviour
	{
		//Iceblock variables
		public GameObject iceBlock;
		public Vector3 iceBlockScale = new Vector3(1.1f, 1.1f, 1.1f);
		public float iceBlockScaleSpeed = 0.05f;

		[HideInInspector]
		public bool complete = false;

		private bool waterGiven = false;

		void OnTriggerStay(Collider coll)
		{
			if (coll.tag == "Player" && Input.GetButtonDown("Submit"))
			{
				if (coll.GetComponent<Player>().HoldingWater && coll.GetComponent<Player>().Onesie.isHeavy)
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
				if (iceBlock.transform.localScale != iceBlockScale)
				{
					iceBlock.transform.localScale += new Vector3(iceBlockScaleSpeed, iceBlockScaleSpeed, iceBlockScaleSpeed);
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
