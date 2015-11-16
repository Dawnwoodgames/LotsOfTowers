using Assets.Scripts.Framework;
using LotsOfTowers.Actors;
using LotsOfTowers.Interaction.Triggers;
using UnityEngine;

namespace LotsOfTowers.Interaction
{
	public class Windmachine : MonoBehaviour
	{
		private State currentState;
		private bool inTrigger;

		private void Start()
		{
			//Default state is deactive for the machine
			currentState = State.Deactive;
			inTrigger = false;
		}

		private void Update()
		{
			//Check if the player collides
			if (GameObject.Find("Player") != null)
			{
				//Check if the player is staticly loaded
				// If the action button is clicked
				// And the player is inside the trigger
				if (GameObject.Find("Player").GetComponent<Player>().isStatic
					&& Input.GetButtonUp("Submit")
					&& inTrigger)
				{
					//Activate the machine!
					ChangeState(State.Active);
				}
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