using LotsOfTowers.Framework;
using LotsOfTowers.Actors;
using LotsOfTowers.Interaction.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace LotsOfTowers.Objects
{
	public class Windmachine : MonoBehaviour
	{
		private State currentState;
		private bool inTrigger;
		private Player player;

		public float ChargeThreshold = 80; // Charge needed to activate the machine
        public GameObject batteryLight;
        public GameObject batteryImage;
        public Sprite emptyBattery;
        public Sprite fullBattery;
        
		private void Start()
		{
			//Default state is deactive for the machine
			currentState = State.Deactive;
			inTrigger = false;
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		}

		private void FixedUpdate()
		{
			//If the player is staticly charged (higher then threshold) and is inside the trigger activating the machine
			if (inTrigger && (Input.GetAxis("Submit") > 0) && (player.StaticCharge >= ChargeThreshold))
			{
				//Then activate the machine + wind
				ChangeState(State.Active);

                //Set the new battery image
                batteryImage.GetComponent<Image>().sprite = fullBattery;

                //Set the new battery light color
                batteryLight.GetComponent<Light>().color = Color.green;
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