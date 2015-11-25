using UnityEngine;
using System.Collections;
using LotsOfTowers.Actors;

namespace LotsOfTowers.Interaction
{
    public class TowerThreeTopElevator : MonoBehaviour {

        public float targetY;
        public float elevateSpeed = 1.0F;

        private float elevatorY;
        private float elevatorX;
        private float elevatorZ;

        private bool elevating = false;
        private bool elevated = false;

        private PlayerController playerController;

        private Vector3 targetPosition;

        private float startTime;
        private float journeyLength;

        void Start()
        {
            elevatorX = transform.position.x;
            elevatorY = transform.position.y;
            elevatorZ = transform.position.z;
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

            targetPosition = new Vector3(elevatorX, targetY, elevatorZ);

            journeyLength = Vector3.Distance(transform.position, targetPosition);
        }

        void Update()
        {
            if (elevating)
            {
                if (transform.position == targetPosition)
                {
                    elevating = false;
                    elevated = true;
                }
                else
                {
                    float distCovered = (Time.time - startTime) * elevateSpeed;
                    float fracJourney = distCovered / journeyLength;
                    transform.position = Vector3.Lerp(transform.position, targetPosition, fracJourney);
                }
            }

            if(elevated)
            {
                playerController.enabled = true;
            }
        }

        void OnTriggerEnter()
        {
            if(!elevating)
            {
                startTime = Time.time;
                playerController.enabled = false;
                elevating = true;
            }
        
        }

    }

}
