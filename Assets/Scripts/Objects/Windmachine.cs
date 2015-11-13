using Assets.Scripts.Framework;
using LotsOfTowers.Actors;
using UnityEngine;

namespace LotsOfTowers.Objects
{
	public class Windmachine : MonoBehaviour
	{
		private State currentState;
		private bool inTrigger;
		private Player player;

		public float ChargeThreshold = 80; // Charge needed to activate the machine

		private void Start()
		{
			//Default state is deactive for the machine
			currentState = State.Deactive;
			inTrigger = false;
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		}

		private void Update()
		{
			if (player == null) {
				player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
			}

			if (inTrigger && Input.GetAxis("Submit") > 0 && player != null && player.StaticCharge >= ChargeThreshold) {
				ChangeState(State.Active);
			}
		}

		private void OnTriggerStay(Collider coll)
		{
			if (coll.attachedRigidbody)
			{
				inTrigger = true;
			}
		}

		private void OnTriggerExit()
		{
			inTrigger = false;
		}

		private void ChangeState(State newState)
		{
			if (newState != currentState)
			{
				currentState = newState;
			}
		}
	}
}