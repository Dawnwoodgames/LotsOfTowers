using UnityEngine;
using System.Collections;
using Nimbi.CameraControl;
using System.Linq;
using System;

namespace Nimbi.Interaction
{
	public class HamsterBall : MonoBehaviour
	{
		private GameObject player;
		private GameObject ball;
		private GameObject focusView;
		private Rigidbody rb;
		private float hMove, vMove;
		private float movementSpeed = 8f;
		private bool playerIsNear = false;
		public bool playerInside = false;

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
				hMove = Input.GetAxis("Horizontal");
				vMove = Input.GetAxis("Vertical");
				player.transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y, ball.transform.position.z);
				focusView.GetComponent<CameraFollowScript>().SetCameraFocus(ball);

				Vector3 movement = new Vector3(hMove, 0f, vMove);
				Move(movement);
			}
			else if (playerIsNear && Input.GetButtonDown("Submit"))
			{
                ball.tag = "HamsterBall";
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
            if (movement == Vector3.zero) rb.velocity = Vector3.zero;
			movement = focusView.transform.TransformDirection(movement);

            if (movement.magnitude > 1f) movement.Normalize();
			movement = transform.InverseTransformDirection(movement);

			rb.AddForce(movement * movementSpeed);
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
	}
}