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

        public Onesie elephantOnesie;

        private SeesawLibraBoardTrigger boardStartTrigger;
        private SeesawLibraBoardTrigger boardEndTrigger;
        private GameObject player;
        private PlayerController playerController;

        private bool getOnesiePartFinished = false;

        //Start values elephant jump lerp
        private Vector3 elephantStartPosition;
        private Vector3 elephantJumpTargetPosition;
        private bool elephantJumpFinished = false;
        private bool elephantJumpStartValuesSet = false;
        private float elephantJumpStartDistance;
        private float elephantJumpStartTime;

        // Start values for boardlerprotation
        private bool boardLerpValuesSet = false;
        private bool flippingFirstSequenceFinished = false;
        private float lerpBoardStartTime;
        private Vector3 lerpBoardStartRotation;
        
        // Onesie related
        private bool hasElephantOnesie;
        private bool EvenBoardLerpFinished = false;


        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerController = player.GetComponent<PlayerController>();
            
            boardStartTrigger = boardStart.GetComponent<SeesawLibraBoardTrigger>();
            boardEndTrigger = boardEnd.GetComponent<SeesawLibraBoardTrigger>();
            
        }

        void FixedUpdate()
        {
            if(!getOnesiePartFinished)
            {
                // First up; the elephant has to jump from the platform.
                if (!elephantJumpFinished)
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
                        if (!hasElephantOnesie)
                        {
                            GiveElephantOnesie();
                        }
                        else
                        {
                            if (player.GetComponent<Player>().Onesie.type != OnesieType.Elephant)
                            {
                                Debug.Log("POPUP: Press 2/(Y) to put on your onesie!");
                            }
                            else
                            {
                                if (!EvenBoardLerpFinished)
                                {
                                    EvenBoard();
                                }
                                else
                                {
                                    getOnesiePartFinished = true;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.Log("We are finished with part one, join us on the next episode on NIMBI!");

                // Elephant walks of
                // Board flips other way
                // You walk to the other end
                // Take off onesie
                // Elephant jumps which will launch you to the next platform.


            }
            
        }

        //** Onesie related **//
        private void GiveElephantOnesie()
        {
            player.GetComponent<Player>().AddOnesieToFirstFreeSlot(elephantOnesie);
            hasElephantOnesie = true;
        }



        //** Board Flipping Start **//
        private void SetBoardLerpStartValues()
        {
            lerpBoardStartTime = Time.time;
            lerpBoardStartRotation = new Vector3(board.transform.rotation.x, board.transform.rotation.y, board.transform.rotation.z);
            boardLerpValuesSet = true;
        }
        private void EvenBoard()
        {
            if (!boardLerpValuesSet)
            {
                SetBoardLerpStartValues();
            }

            if (!EvenBoardLerpFinished)
            {
                Debug.Log(lerpBoardStartRotation);

                Vector3 to = new Vector3(0, lerpBoardStartRotation.y, lerpBoardStartRotation.z);
                if (Mathf.Round(board.transform.eulerAngles.x) != 0)
                {
                    board.transform.eulerAngles = new Vector3(board.transform.eulerAngles.x + 25 * Time.deltaTime, board.transform.eulerAngles.y, board.transform.eulerAngles.z);
                }
                else
                {
                    board.transform.eulerAngles = to;
                    EvenBoardLerpFinished = true;
                    boardLerpValuesSet = false;
                }
            }





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
        //** Board Flipping end **//

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
            playerController.DisableMovement(); // Disable the player controller. This is a controlled event
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

