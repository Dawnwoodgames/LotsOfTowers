using UnityEngine;
using System.Collections;
using LotsOfTowers.Actors;

namespace LotsOfTowers.Interaction
{
    public class TowerThreeTopElevator : MonoBehaviour {

        public float targetY;
        public float targetZ;
        public float halfwayZ;
        public float elevateSpeed = 1.0F;
        
        public GameObject invisWalls;
        public GameObject invisWallEntrance;

        private float elevatorY;
        private float elevatorX;
        private float elevatorZ;

        private bool elevating = false;
        private bool elevated = false;
        private bool halfwayelevated = false;
        private bool secondwayelevated = false;

        private PlayerController playerController;

        private Vector3 targetPosition;
        private Vector3 targetHalfwayPosition;
        private Vector3 targetSecondPosition;

        private float startTime;
        private float journeyLength;
        private float journeyHalfLenght;
        private float journeySecondLenght;
        private float halfofdist;

        void Start()
        {
            if (targetZ != 0)
            {
                elevatorZ = targetZ;
            }
            else
            {
                elevatorZ = transform.localPosition.z;
            }

            elevatorY = transform.localPosition.y;
            elevatorX = transform.localPosition.x;
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();


            halfofdist = (transform.localPosition.y - targetY) / 2;
            halfofdist = transform.localPosition.y - halfofdist;

            targetHalfwayPosition = new Vector3(elevatorX, halfofdist, halfwayZ);
            targetSecondPosition = new Vector3(elevatorX, targetY, halfwayZ);
            targetPosition = new Vector3(elevatorX, targetY, elevatorZ);

            journeyHalfLenght = Vector3.Distance(transform.localPosition, targetHalfwayPosition);
            journeySecondLenght = Vector3.Distance(transform.localPosition, targetSecondPosition);
            journeyLength = Vector3.Distance(transform.localPosition, targetPosition);
            
        }

        void FixedUpdate()
        {
            if (elevating)
            {
                elevateHalfway();
            }

            if (halfwayelevated)
            {
                elevateSecond();
            }

            if (secondwayelevated)
            {
                elevateToTop();
            }
            
            if (elevated)
            {
                elevating = false;
                halfwayelevated = false;
                secondwayelevated = false;

                playerController.enabled = true;
                invisWallEntrance.SetActive(false);
            }
        }

        void elevateSecond()
        {
            if (transform.localPosition == targetSecondPosition)
            {
                startTime = Time.time;
                secondwayelevated = true;
                halfwayelevated = false;
            }
            else
            {
                float distCovered = (Time.time - startTime) * 0.25f;
                float fracJourney = distCovered / journeySecondLenght;

                transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetSecondPosition, fracJourney);
            }
        }

        void elevateHalfway()
        {
            if (transform.localPosition == targetHalfwayPosition)
            {
                startTime = Time.time;
                elevating = false;
                halfwayelevated = true;
            }
            else
            {
                float distCovered = (Time.time - startTime) * (elevateSpeed / 2);
                float fracJourney = distCovered / journeyHalfLenght;

                transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetHalfwayPosition, fracJourney);
            }
        }

        void elevateToTop()
        {
            if(Vector3.Distance(transform.localPosition, targetPosition) < 0.1f)
            {
                secondwayelevated = false;
                elevated = true;
            }
            else
            {
                float distCovered = (Time.time - startTime) * (elevateSpeed / 2);
                float fracJourney = distCovered / journeyLength;

                transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, fracJourney);
            }
        }


        void OnTriggerEnter()
        {
            if(!elevating)
            {
                startTime = Time.time;
                playerController.enabled = false;
                elevating = true;
                invisWalls.SetActive(true);
            }
        
        }

    }

}
