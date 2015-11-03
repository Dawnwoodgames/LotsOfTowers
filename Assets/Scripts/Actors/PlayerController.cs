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

		float m_TurnAmount;
		float m_ForwardAmount;
		float m_MovingTurnSpeed = 360;
		float m_StationaryTurnSpeed = 180;
		public float m_JumpPower = 12f;
		float m_GravityMultiplier = 2f;
		float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
		float m_MoveSpeedMultiplier = 1f;
		float m_AnimSpeedMultiplier = 1f;
		float m_GroundCheckDistance = 0.1f;
		Vector3 m_GroundNormal;

		bool m_IsGrounded;
		float m_OrigGroundCheckDistance;
		const float k_Half = 0.5f;
		float m_CapsuleHeight;
		Vector3 m_CapsuleCenter;
		CapsuleCollider m_Capsule;
		bool m_Crouching;


		private void Start()
		{
			player = GetComponent<Actor>();
			rigidBody = GetComponent<Rigidbody>();
			rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;


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
			//Get camera position to face walking directory
			movement = Camera.main.transform.TransformDirection(movement);

			if (movement.magnitude > 1f) movement.Normalize();
			movement = transform.InverseTransformDirection(movement);
			CheckGroundStatus();
			movement = Vector3.ProjectOnPlane(movement, m_GroundNormal);
			m_TurnAmount = Mathf.Atan2(movement.x, movement.z);
			m_ForwardAmount = movement.z;

			ApplyExtraTurnRotation();

			transform.Translate(movement * player.MovementSpeed * Time.deltaTime);
		}
		void ApplyExtraTurnRotation()
		{
			// help the character turn faster (this is in addition to root rotation in the animation)
			float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
			transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
		}

		void CheckGroundStatus()
		{
			RaycastHit hitInfo;
#if UNITY_EDITOR
			// helper to visualise the ground check ray in the scene view
			Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
			// 0.1f is a small offset to start the ray from inside the character
			// it is also good to note that the transform position in the sample assets is at the base of the character
			if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
			{
				m_GroundNormal = hitInfo.normal;
				m_IsGrounded = true;
			}
			else
			{
				m_IsGrounded = false;
				m_GroundNormal = Vector3.up;
			}
		}

	}
}