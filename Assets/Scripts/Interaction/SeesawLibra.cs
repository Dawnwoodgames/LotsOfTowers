using UnityEngine;
using System.Collections;
using LotsOfTowers.Interaction.Triggers;
using LotsOfTowers.Actors;

namespace LotsOfTowers.Interaction
{
    public class SeesawLibra : MonoBehaviour {

        public GameObject board;
        public GameObject boardStart;
        public GameObject boardEnd;
        public GameObject elephant;


        private SeesawLibraBoardTrigger boardStartTrigger;
        private SeesawLibraBoardTrigger boardEndTrigger;
        private GameObject player;
        private PlayerController playerController;

        //Start values elephant jump lerp
        private Vector3 elephantStartPosition;
        private bool elephantJumpFinished = false;
        private bool elephantJumpStartValuesSet = false;
        private float elephantJumpStartDistance;
        private float elephantJumpStartTime;
        private Vector3 elephantJumpTargetPosition;

        // Start values for boardlerprotation
        private bool boardLerpValuesSet = false;
        private float lerpBoardStartTime;
        private Vector3 lerpBoardStartRotation;
        private bool flippingFirstSequenceFinished = false;


        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerController = player.GetComponent<PlayerController>();
            
            boardStartTrigger = boardStart.GetComponent<SeesawLibraBoardTrigger>();
            boardEndTrigger = boardEnd.GetComponent<SeesawLibraBoardTrigger>();
            
        }

        void FixedUpdate()
        {
            // First up; the elephant has to jump from the platform.
            if(!elephantJumpFinished)
            {
                if (boardStartTrigger.isPlayerOnTrigger() && !boardEndTrigger.isElephantOnTrigger())
                {
                    ElephantJump(); // This also disables the playerController;
                }
            }
            else
            {
                // Now the elephant has jumped from the platform. Its time to flip the board towards the elephant
                if (!flippingFirstSequenceFinished)
                {
                    FlipBoardToInvertedRotation();
                }
                else
                {
                    // Ok, now we are here
                }

            }
        }

        private void SetBoardLerpStartValues()
        {
            lerpBoardStartTime = Time.time;
            lerpBoardStartRotation = new Vector3(board.transform.rotation.x, board.transform.rotation.y, board.transform.rotation.z);
            boardLerpValuesSet = true;
        }
        private void FlipBoardToStartRotation()
        {

        }
        private void FlipBoardToInvertedRotation()
        {
            if (!boardLerpValuesSet)
            {
                SetBoardLerpStartValues();
            }

            if (!flippingFirstSequenceFinished)
            {
                Vector3 to = new Vector3(-20f, lerpBoardStartRotation.y, lerpBoardStartRotation.z);
                if (Mathf.Round(board.transform.eulerAngles.x) != 340)
                {
                    board.transform.eulerAngles = new Vector3(board.transform.eulerAngles.x - 100 * Time.deltaTime, board.transform.eulerAngles.y, board.transform.eulerAngles.z);
                }
                else
                {
                    board.transform.eulerAngles = to;
                    flippingFirstSequenceFinished = true;
                    boardLerpValuesSet = false;
                }
            }
        }
       
        //** ELEPHANT RELATED START **/
        private void SetElephantJumpStartValues()
        {
            elephantStartPosition = elephant.transform.position;
            elephantJumpStartTime = Time.time;
            elephantJumpStartDistance = Vector3.Distance(elephant.transform.position, boardEnd.transform.position);
            elephantJumpTargetPosition = new Vector3(elephant.transform.position.x, elephant.transform.position.y, elephant.transform.position.z + elephantJumpStartDistance);
            elephantJumpStartValuesSet = true;
        }

        private void ElephantJumpLerp()
        {
            float distCovered = (Time.time - elephantJumpStartTime) * 1f;
            float fracJourney = distCovered / elephantJumpStartDistance;
            elephant.transform.position = Vector3.Lerp(elephant.transform.position, elephantJumpTargetPosition, fracJourney);
        }

        private void ElephantJump()
        {
            playerController.enabled = false; // Disable the player controller. This is a controlled event
            if (!elephantJumpFinished)
            {
                if (elephantJumpStartValuesSet)
                {
                    if (elephant.transform.position.z == elephantJumpTargetPosition.z)
                    {
                        elephantJumpFinished = true;
                    }
                    ElephantJumpLerp();
                }
                else
                {
                    SetElephantJumpStartValues();
                }
            }
        }
        //** ELEPHANT RELATED END **//

    }

}

