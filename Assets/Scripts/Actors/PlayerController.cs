using LotsOfTowers.Framework;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace LotsOfTowers.Actors
{
	//We need the following components to make the player work
	[RequireComponent(typeof(Player))]
	[RequireComponent(typeof(Rigidbody))]
	public class PlayerController : MonoBehaviour
	{
		private static readonly float InputDelay = 0.5f;

		//The actual player with all the movement properties
		private BoxCollider box;
		private CapsuleCollider capsule;
        private HeadsUpDisplayScript hudUi;
        private Transform mainCamera;
        private OnesieSwitchController onesieSwitchAnimation;
        private Player player;

        //Moving variables
        private Vector3 movement;
		private float turnAmount;
		private float forwardAmount;
		private float movingTurnSpeed = 360;
		private bool removeChargeOnNextFrame;
		private float stationaryTurnSpeed = 360;
		private float switchDelay;
		private Vector3 groundNormal;

        private bool canMove = true;
		private bool moving;

		private void Awake() {
			this.mainCamera = Camera.main.transform;
            this.onesieSwitchAnimation = GetComponentInChildren<OnesieSwitchController>();
			this.player = GetComponent<Player>();
        }

		private void FixedUpdate()
		{
			//Get Input controls
			float h = CrossPlatformInputManager.GetAxis("Horizontal");
			float v = CrossPlatformInputManager.GetAxis("Vertical");

			bool onesie1 = CrossPlatformInputManager.GetButton("Onesie 1");
			bool onesie2 = CrossPlatformInputManager.GetButton("Onesie 2");
			bool onesie3 = CrossPlatformInputManager.GetButton("Onesie 3");
			bool submit = CrossPlatformInputManager.GetButton("Submit");

			if (switchDelay > 0) {
				switchDelay -= Time.smoothDeltaTime;
			}

			if ((onesie1 || onesie2 || onesie3) && switchDelay <= 0)
			{
                int input = onesie1 ? 0 : (onesie2 ? 1 : 2);

                if ((input == 0 && !player.HasOnesie(OnesieType.Elephant)) ||
                    (input == 1 && !player.HasOnesie(OnesieType.Dragon)) ||
                    (input == 2 && !player.HasOnesie(OnesieType.Hamster))) {
                    // Player doesn't have onesie he/she is trying to switch to
                    return;
                }

                //Switch to the selected onesie
                onesieSwitchAnimation.Trigger();
				player.SwitchOnesie(input);
                switchDelay = InputDelay;
            }

            if(canMove)
            {
                //Apply movement, jumping and rotation
                movement = new Vector3(h, 0f, v);
                Move(movement);
            }

			if (removeChargeOnNextFrame) {
				player.StaticCharge = 0;
				removeChargeOnNextFrame = false;
			}

			if (submit) {
				// Remove charge in next frame to avoid it being removed before it can be used
				removeChargeOnNextFrame = true;
			}

			player.Animator.SetBool("Moving", h != 0 || v != 0);
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
			GetComponent<Rigidbody>().MovePosition(transform.position + transform.TransformDirection(movement * player.Onesie.movementSpeed * Time.deltaTime));
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
