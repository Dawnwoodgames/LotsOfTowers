using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace LotsOfTowers.Actors
{
	//We need the following components to make the player work
	[RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Player))]

	public class PlayerController : MonoBehaviour
	{

		//The actual player with all the movement properties
		private Player player;
		private Transform mainCamera;
		private CapsuleCollider capsule;
        private BoxCollider box;

		//Moving variables
		private Vector3 movement;
		private float turnAmount;
		private float forwardAmount;
		private float movingTurnSpeed = 360;
		private float stationaryTurnSpeed = 360;
		private Vector3 groundNormal;

        private bool canMove = true;


        private void Start()
		{
			player = GetComponent<Player>();

			//Get camera transform
			if (Camera.main != null)
			{
				mainCamera = Camera.main.transform;
			}
			else
			{
				Debug.LogWarning("No \"Main Camera\" found! Tag a camera as main");
			}
        }

		private void FixedUpdate()
		{
			//Get Input controls
			float h = CrossPlatformInputManager.GetAxis("Horizontal");
			float v = CrossPlatformInputManager.GetAxis("Vertical");

			bool onesie1 = CrossPlatformInputManager.GetAxis("Onesie 1") > 0;
			bool onesie2 = CrossPlatformInputManager.GetAxis("Onesie 2") > 0;
			bool onesie3 = CrossPlatformInputManager.GetAxis("Onesie 3") > 0;

			if (onesie1 || onesie2 || onesie3)
			{
				//Switch to the selected onesie
				player.SwitchOnesie(onesie1 ? 0 : (onesie2 ? 1 : 2));
			}

            if(canMove)
            {
                //Apply movement, jumping and rotation
                movement = new Vector3(h, 0f, v);
                Move(movement);
            }
			
        }

		private void Move(Vector3 movement)
		{
			//Get camera position to face walking directory
			movement = mainCamera.transform.TransformDirection(movement);

			//If the magintude goes higher then 1, bring it back to 1
			//Magnitude is the length between the vectors origin and its endpoint
			if (movement.magnitude > 1f) movement.Normalize();

			//Invert direction - http://docs.unity3d.com/ScriptReference/Transform.InverseTransformDirection.html
			movement = transform.InverseTransformDirection(movement);

			//Check if the player is grounded
			IsGrounded();

			//Stick the model to the surface
			movement = Vector3.ProjectOnPlane(movement, groundNormal);

			//Calculate turning amount based on ???
			turnAmount = Mathf.Atan2(movement.x, movement.z);

			forwardAmount = movement.z;

			//Turn the character
			TurnRotation();

			//Translate the current position, based on the movementspeed / time to move the player
			transform.Translate(movement * player.Onesie.movementSpeed * Time.deltaTime);
		}

		private void TurnRotation()
		{
			// help the character turn faster (this is in addition to root rotation in the animation)
			float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
			transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
		}

		private void IsGrounded()
		{
			RaycastHit hitInfo;

            // 0.1f is a small offset to start the ray from inside the character
            // it is also good to note that the transform position in the sample assets is at the base of the character
            if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, 0.1f))
			{
				groundNormal = hitInfo.normal;
            }
            else
			{
                groundNormal = Vector3.up;
            }
        }

        public void EnableMovement()
        {
            if (!canMove)
            {
                canMove = true;
            }
        }
        public void DisableMovement()
        {
            if(canMove)
            {
                canMove = false;
            }
        }

	}
}
