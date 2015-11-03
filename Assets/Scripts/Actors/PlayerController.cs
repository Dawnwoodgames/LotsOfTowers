using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace LotsOfTowers.Actors
{
	//We need the following components to make the player work
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Actor))]

	public class PlayerController : MonoBehaviour
	{
		//The actual player with all the movement properties
		private Actor player;
		private Transform mainCamera;
		private Rigidbody rigidBody;

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
		private float stationaryTurnSpeed = 180;

		private void Start()
		{
			player = GetComponent<Actor>();
			rigidBody = GetComponent<Rigidbody>();
			
			//Set constraints for rotation lock, so the character doesn't fall
			rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

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
			//If the jumpcount is not equal to the amount of jumps done (mid air)
			if (player.JumpCount != jumped)
			{
				//Check if jump button is clicked
				jumping = CrossPlatformInputManager.GetButtonDown("Jump");
			}

			//Get object length to go through the floor
			float height = GetComponent<Collider>().bounds.extents.y;

			//Check if the floor is touching the feet of the model
			if (Physics.Raycast(transform.position, Vector3.down, height))
			{
				jumped = 0;
			}
		}

		private void FixedUpdate()
		{ //Get Input controls
			float h = CrossPlatformInputManager.GetAxis("Horizontal");
			float v = CrossPlatformInputManager.GetAxis("Vertical");
			
			bool onesie1 = CrossPlatformInputManager.GetAxis("Onesie 1") > 0;
			bool onesie2 = CrossPlatformInputManager.GetAxis("Onesie 2") > 0;
			bool onesie3 = CrossPlatformInputManager.GetAxis("Onesie 3") > 0;

			if (onesie1 || onesie2 || onesie3) {
				player.SwitchOnesie(onesie1 ? 0 : (onesie2 ? 1 : 2));
			}

			movement = new Vector3(h, 0f, v);
			Move(movement);
			Jump();
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

			//I don't know wtf this does, please find out thanks :)
			movement = Vector3.ProjectOnPlane(movement, groundNormal);
			turnAmount = Mathf.Atan2(movement.x, movement.z);

			forwardAmount = movement.z;

			//Turn the character
			TurnRotation();

			//Translate the current position, based on the movementspeed / time to move the player
			transform.Translate(movement * player.MovementSpeed * Time.deltaTime);
		}

		void TurnRotation()
		{
			// help the character turn faster (this is in addition to root rotation in the animation)
			float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
			transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
		}

		void IsGrounded()
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