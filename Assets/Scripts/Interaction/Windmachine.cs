using LotsOfTowers.Framework;
using LotsOfTowers.Actors;
using LotsOfTowers.Interaction.Triggers;
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
			//If the player is staticly charged (higher then threshold) and is inside the trigger activating the machine
			if (inTrigger && (Input.GetAxis("Submit") > 0) && (player.StaticCharge >= ChargeThreshold))
			{
				//Then activate the machine + wind
				ChangeState(State.Active);

				//Release the load of the static charge of the player
				player.StaticCharge = 0.1f;
			}

			//Set state from trigger to state of machine
			WindTrigger.state = currentState;
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