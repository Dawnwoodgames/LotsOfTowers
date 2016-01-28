using UnityEngine;
using System.Collections;
using Nimbi.CameraControl;
using System.Linq;
using System;
using Nimbi.Actors;

namespace Nimbi.Interaction
{
	public class HamsterBall : MonoBehaviour
	{
		private GameObject player;
		private GameObject ball;
		private GameObject focusView;
		private Rigidbody rb;
		private float hMove, vMove;
		private float movementSpeed = 25f;
		private bool playerIsNear = false;
		public bool playerInside = false;
        public float maxSpeed = 1.5f;

        public GameObject Ball
        {
            get { return ball; }
        }

		void Start()
		{
			player = GameObject.FindGameObjectWithTag("Player");
			ball = GameObject.Find("HamsterBall");
			focusView = GameObject.Find("CenterFocus");
			rb = ball.GetComponent<Rigidbody>();
		}

		void Update()
		{
			if (playerInside)
			{
				hMove = Input.GetAxisRaw("Horizontal");
				vMove = Input.GetAxisRaw("Vertical");
				player.transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y, ball.transform.position.z);
				focusView.GetComponent<CameraFollowScript>().SetCameraFocus(ball);

				Vector3 movement = new Vector3(hMove, 0f, vMove);
				Move(movement);
			}
			else if (playerIsNear 
                && Input.GetButtonDown("Submit")
                && player.GetComponent<Player>().Onesie.type == OnesieType.Hamster)
			{
                ball.tag = "HamsterBall";
                player.GetComponent<Player>().PlayerCanSwitchOnesie = false; // No no, you no switch
                player.transform.parent = transform;
                player.GetComponent<CapsuleCollider>().enabled = false;
                player.GetComponent<Rigidbody>().useGravity = false;
                player.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                Destroy(GameObject.Find("TemporaryBlock"));
                playerInside = true;
                rb.isKinematic = false;
                playerIsNear = false;
			}
		}

		private void Move(Vector3 movement)
		{
            if (movement == Vector3.zero)
            {
                rb.velocity = Vector3.zero;
            }
			movement = focusView.transform.TransformDirection(movement);

            if (movement.magnitude > 1f)
            {
                movement.Normalize();
            }
			movement = transform.InverseTransformDirection(movement);

			rb.AddForce(movement * movementSpeed);
            if(rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
		}

		private void OnTriggerStay(Collider coll)
		{
			if (coll.tag == "Player")
				playerIsNear = true;
		}

		private void OnTriggerExit()
		{
			playerIsNear = false;
		}

        public bool EasyBallMovement { get; set; }
        
	}
}