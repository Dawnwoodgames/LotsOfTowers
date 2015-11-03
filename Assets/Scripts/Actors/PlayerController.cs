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

		private bool jumping = false;
		private int jumped = 0;
		private Vector3 movement;

		private float turningSpeed = 60f;

		private void Start()
		{
			player = GetComponent<Actor>();
			rigidBody = GetComponent<Rigidbody>();

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
		{
			//Get Input controls
			float h = CrossPlatformInputManager.GetAxis("Horizontal");
			float v = CrossPlatformInputManager.GetAxis("Vertical");
			
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
			movement = Camera.main.transform.TransformDirection(movement);
			transform.Translate(movement * player.MovementSpeed * Time.deltaTime);
		}
	}
}