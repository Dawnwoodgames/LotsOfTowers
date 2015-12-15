﻿using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction
{
    public class HamsterBall : MonoBehaviour
    {
        private GameObject player;
        private GameObject ball;
        private GameObject focusView;
        private Rigidbody rb;
        private Vector3 groundNormal;
        private float hMove, vMove;
        private float movementSpeed = 8f;
        private bool playerIsNear, playerIsExiting, playerInside = false;

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
                player.transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y - .8f, ball.transform.position.z);
                focusView.GetComponent<CameraControl.CameraFollowScript>().SetCameraFocus(GameObject.Find("HamsterBall"));

                Vector3 movement = new Vector3(hMove, 0f, vMove);
                Move(movement);

                if (Input.GetButtonDown("Submit"))
                    ExitHamsterBall();
            } else if (playerIsNear && Input.GetButtonDown("Submit"))
            {
                player.transform.parent = transform;
                player.GetComponent<CapsuleCollider>().enabled = false;
                player.GetComponent<Rigidbody>().useGravity = false;
                playerInside = true;
                rb.isKinematic = false;
                playerIsNear = false;
            }
        }

        private void Move(Vector3 movement)
        {
            movement = focusView.transform.TransformDirection(movement);
            if (movement.magnitude > 1f) movement.Normalize();

            movement = transform.InverseTransformDirection(movement);

            rb.AddForce(movement * movementSpeed);
        }

        private void ExitHamsterBall()
        {
            player.transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y + 1.5f, ball.transform.position.z);
            focusView.GetComponent<CameraControl.CameraFollowScript>().SetCameraFocus(GameObject.FindGameObjectWithTag("Player"));
            playerInside = false;
            ball.GetComponent<Rigidbody>().isKinematic = true;
            player.transform.parent = null;
            player.GetComponent<Rigidbody>().useGravity = true;
            player.GetComponent<CapsuleCollider>().enabled = true;
            transform.position = ball.transform.position;
            ball.transform.position = transform.position;
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