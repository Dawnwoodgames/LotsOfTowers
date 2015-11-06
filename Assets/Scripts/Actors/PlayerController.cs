using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace LotsOfTowers.Actors
{
	//We need the following components to make the player work
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Player))]

	public class PlayerController : MonoBehaviour
	{
		//The actual player with all the movement properties
		private Player player;
		private Transform mainCamera;
		private Rigidbody rigidBody;
		private CapsuleCollider capsule;

		//Jumping variables
		private bool jumping = false;
		private int jumped = 0;
		private Vector3 groundNormal;
		private bool isGrounded;

		//Moving variables
		private Vector3 movement;
		private float turnAmount;
		private float forwardAmount;
		private float movingTurnSpeed = 360;
		private float stationaryTurnSpeed = 360;

		//Crouch variables
		private bool isCrouching = false;
		private float capsuleHeight;
		private Vector3 capsuleCenter;


		private void Start()
		{
			player = GetComponent<Player>();
			rigidBody = GetComponent<Rigidbody>();
			capsule = GetComponent<CapsuleCollider>();
			capsuleHeight = capsule.height;
			capsuleCenter = capsule.center;

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

		private void Update()
		{
			//Get object length to go through the floor
			float height = GetComponent<Collider>().bounds.extents.y;

			//Check if the floor is touching the feet of the model
			if (Physics.Raycast(transform.position, Vector3.down * 1.2f, height + 0.06f))
			{
				jumped = 0;
			}
			else if(player.JumpCount < 2)
			{
				jumped++;
			}

			//If the jumpcount is not equal to the amount of jumps done (mid air)
			if (player.JumpCount > jumped)
			{
				//Check if jump button is clicked
				jumping = CrossPlatformInputManager.GetButtonDown("Jump");
			}
		}

		private void FixedUpdate()
		{ 
			//Get Input controls
			float h = CrossPlatformInputManager.GetAxis("Horizontal");
			float v = CrossPlatformInputManager.GetAxis("Vertical");
			bool crouch = CrossPlatformInputManager.GetButton("Crouch");

			bool onesie1 = CrossPlatformInputManager.GetAxis("Onesie 1") > 0;
			bool onesie2 = CrossPlatformInputManager.GetAxis("Onesie 2") > 0;
			bool onesie3 = CrossPlatformInputManager.GetAxis("Onesie 3") > 0;

			if (onesie1 || onesie2 || onesie3)
			{
				//Switch to the selected onesie
				player.SwitchOnesie(onesie1 ? 0 : (onesie2 ? 1 : 2));
			}

			//Apply movement, jumping and rotation
			movement = new Vector3(h, 0f, v);
			Move(movement);
			Jump();
			Crouch(crouch);
		}

		private void Crouch(bool crouch)
		{
			//Check if the player clicked on crouch and the player is actually grounded (can change, crouch mid air?)
			if (crouch)
			{
				//Already crouching
				if (isCrouching)
				{
					return;
				}

				//Resize the capsule collider to half height / width
				capsule.height = capsule.height / 2f;
				capsule.center = capsule.center / 2f;
				isCrouching = true;
			}
			else
			{
				Ray crouchRay = new Ray(rigidBody.position + Vector3.up * capsule.radius * 0.5f, Vector3.up);
				float crouchRayLength = capsuleHeight - capsule.radius * 0.5f;
				if (Physics.SphereCast(crouchRay, capsule.radius * 0.5f, crouchRayLength))
				{
					isCrouching = true;
					return;
				}
				capsule.height = capsuleHeight;
				capsule.center = capsuleCenter;
				isCrouching = false;
			}
		}


		private void Jump()
		{
			// check whether conditions are right to allow a jump:
			if (jumping)
            {
                // Jump!
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, player.JumpPower, rigidBody.velocity.z);
				jumped++;
				jumping = false;
			}
		}

		private void Move(Vector3 movement)
		{
			//Get camera position to face walking directory
			movement = Camera.main.transform.TransformDirection(movement);

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
			transform.Translate(movement * player.MovementSpeed * Time.deltaTime);
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
				isGrounded = true;
            }
            else
			{
				isGrounded = false;
                groundNormal = Vector3.up;
            }
        }
	}
}
