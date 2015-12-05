using UnityEngine;
using System.Collections;
using LotsOfTowers.Actors;

namespace LotsOfTowers.Interaction
{
	public class LiftingObject : MonoBehaviour
	{
		private bool pickedUp;
		private bool inTrigger = false;
		private Transform player;
		private float smoothLerp = 5;
		private Rigidbody rigid;
		private MeshCollider meshColl;

		void Start()
		{
			try
			{
				player = GameObject.FindGameObjectWithTag("Player").transform;
				rigid = GetComponent<Rigidbody>();
				meshColl = GetComponent<MeshCollider>();
			}
			catch (System.Exception ex)
			{
				Logger.Log(ex);
				throw;
			}
		}
		private void OnCollisionEnter(Collision col)
		{
			if (col.gameObject.tag == "Player")
			{
				inTrigger = true;
			}
		}

		private void OnCollisionExit(Collision col)
		{
			if (col.gameObject.tag == "Player")
			{
				inTrigger = false;
			}
		}

		void Update()
		{
			if (inTrigger)
			{
				if (Input.GetButton("Submit") && player.GetComponent<Player>().Onesie.isElephant)
				{
					pickedUp = true;
				}
			}

			//Move the object with the player if its picked up
			if (pickedUp)
			{
				transform.position = Vector3.Lerp(transform.position, player.transform.position + Vector3.forward * 3 + Vector3.up * 1.2f, Time.deltaTime * smoothLerp);

				if (!rigid.isKinematic)
				{
					rigid.isKinematic = true;
					meshColl.isTrigger = true;
				}

				if (!Input.GetButton("Submit") || !player.GetComponent<Player>().Onesie.isElephant)
				{
					pickedUp = false;
					rigid.isKinematic = false;
					meshColl.isTrigger = false;
				}
			}
		}
	}
}