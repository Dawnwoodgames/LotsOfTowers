﻿using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction
{
    public class FloatingCarpet : MonoBehaviour {

		private GameObject player;
		private PlayerController playerController;
		
		private bool triggered;

        private int state;
        private bool finishedFlight = false;

		public float interpolationSpeed = 1;
		public Vector3 targetMidway;
		public Vector3 targetEnd;

		public void Awake() {
			player = GameObject.FindGameObjectWithTag ("Player");
			playerController = player.GetComponent<PlayerController>();
			state = 0;
		}
		
		public void FixedUpdate() {
            if (triggered)
            {
				if (state == 0)
                {
					transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetMidway, interpolationSpeed * Time.deltaTime);
					player.transform.position = transform.position;
				}
                else if (state == 1)
                {
					transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetEnd, interpolationSpeed * Time.deltaTime);
					player.transform.position = transform.position;
				}
                else if (state == 2)
                {
					state = 3;
					transform.localPosition = targetEnd;
					player.transform.position = transform.position;
					playerController.EnableMovement();
                    finishedFlight = true;
				}
			}
		}

		public void OnTriggerEnter(Collider coll) {
			if (coll.gameObject.tag == "Player" && state == 0)
            {
				playerController.DisableMovement();
                triggered = true;
			}
		}

		public void Update() {
			if (state == 0 && Vector3.Distance(transform.localPosition, targetMidway) < 0.25f)
            {
				state = 1;
			}
            else if (state == 1 && Vector3.Distance(transform.localPosition, targetEnd) < 0.1f)
            {
				state = 2;
			}
		}

        public void resetCarpet()
        {
            finishedFlight = false;
            triggered = false;
        }

        public void setState(int value)
        {
            state = value;
        }

        public bool isFlightFinished()
        {
            return finishedFlight;
        }

    }

}