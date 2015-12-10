using UnityEngine;
using System.Collections;
using LotsOfTowers.Interaction.Triggers;
using LotsOfTowers.Actors;
using System;

namespace LotsOfTowers.Interaction
{
	public class SeesawLibra : MonoBehaviour
	{
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
		private bool puzzleFinished = false;

		//Start values elephant jump lerp
		private Vector3 elephantStartPosition;
		private Vector3 elephantSecondPosition;

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


        private Vector3 elephantSecondJumpStartPosition;
        private Vector3 elephantSecondJumpTargetPosition;
        private float elephantSecondJumpStartTime;
        private float elephantSecondJumpStartDistance;
        private bool elephantJumpFromSecondPlatformStartValuesSet = false;
        private bool elephantSecondJumpFinished = false;
        
        private bool elephantLaunchedNimbi = false;

        private bool playerLaunchedUp = false;
        private bool playerOnFinishPlatform = false;

        private bool startValuesForLaunchingPlayersSet = false;
        private float targetJumpHeightForPlayerLaunch;


        void Start()
		{
			player = GameObject.FindGameObjectWithTag("Player");
			playerController = player.GetComponent<PlayerController>();

			boardStartTrigger = boardStart.GetComponent<SeesawLibraBoardTrigger>();
			boardEndTrigger = boardEnd.GetComponent<SeesawLibraBoardTrigger>();

			//Nice code bra
			elephantSecondPosition = new Vector3(GameObject.Find("HelpNimbiFromHere").transform.localPosition.x, GameObject.Find("HelpNimbiFromHere").transform.localPosition.y + 1f, GameObject.Find("HelpNimbiFromHere").transform.localPosition.z);
        }

		void FixedUpdate()
		{
            if (!puzzleFinished)
            {
                if (!getOnesiePartFinished)
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
                                if (!player.GetComponent<Player>().Onesie.isElephant)
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

                //Check if the player is standing on the begin platform
                else if (boardStartTrigger.isPlayerOnTrigger())
                {
                    StartCoroutine(WaitForBalance(0.5f));
                }
                else if (boardEndTrigger.isPlayerOnTrigger())
                {
                    board.GetComponent<Rigidbody>().isKinematic = true;
                    playerController.DisableMovement();

                    if (!player.GetComponent<Player>().Onesie.isElephant)
                    {
                        // Elephant jumps which will launch you to the next platform.
                        if (!elephantSecondJumpFinished)
                        {
                            ElephantJumpFromSecondPlatform(); // Needs to fit the scene.
                        }
                        else
                        {
                            if (!elephantLaunchedNimbi)
                            {
                                ElephantSecondJumpFlipBoard();
                            }
                            else
                            {
                                LaunchPlayerToFinishPlatform();
                                puzzleFinished = true;
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.Log("DONE");
                playerController.EnableMovement();
            }
		}
        
        private void LaunchPlayerToFinishPlatform()
        {
            player.GetComponent<Rigidbody>().AddForce(Vector3.up * 15, ForceMode.VelocityChange);
            player.GetComponent<Rigidbody>().AddForce(Vector3.left * 2, ForceMode.VelocityChange);
            playerOnFinishPlatform = true;
        }

        private void ElephantJumpFromSecondPlatform()
        {
            if (elephantJumpFromSecondPlatformStartValuesSet)
            {
                if ((int)elephant.transform.position.x == (int)elephantSecondJumpTargetPosition.x)
                {
                    elephantSecondJumpFinished = true;
                }
                else
                {
                    float distCovered = (Time.time - elephantSecondJumpStartTime) * 1f;
                    float fracJourney = distCovered / elephantSecondJumpStartDistance;
                    elephant.transform.position = Vector3.Lerp(elephant.transform.position, elephantSecondJumpTargetPosition, fracJourney);
                }
            }
            else
            {
                SetElephantJumpFromSecondPlatformStartValues();
            }
        }
        private void SetElephantJumpFromSecondPlatformStartValues()
        {
            elephantSecondJumpStartPosition = elephant.transform.position;
            elephantSecondJumpStartTime = Time.time;
            elephantSecondJumpStartDistance = Vector3.Distance(elephant.transform.position, boardStart.transform.position);
            elephantSecondJumpTargetPosition = new Vector3(elephant.transform.position.x - (elephantSecondJumpStartDistance * 2), elephant.transform.position.y, elephant.transform.position.z);
            elephantJumpFromSecondPlatformStartValuesSet = true;
        }

        private void ElephantSecondJumpFlipBoard()
        {
            Vector3 to = new Vector3(20f, lerpBoardStartRotation.y, lerpBoardStartRotation.z);
            if (Mathf.Round(board.transform.eulerAngles.x) != 20)
            {
                board.transform.eulerAngles = new Vector3(board.transform.eulerAngles.x + 100 * Time.deltaTime, board.transform.eulerAngles.y, board.transform.eulerAngles.z);
            }
            else
            {
                //board.transform.eulerAngles = to;
                elephantLaunchedNimbi = true;
            }
        }

        
        IEnumerator WaitForBalance(float amount)
		{
			yield return new WaitForSeconds(amount);
			// Elephant walks of and enables moving for player so he can walk to the other side
			ElephantWalkOff();
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
				Vector3 to = new Vector3(0, lerpBoardStartRotation.y, lerpBoardStartRotation.z);
				if (Mathf.Round(board.transform.eulerAngles.x) != 0)
				{
					board.transform.eulerAngles = new Vector3(board.transform.eulerAngles.x + 25 * Time.deltaTime, board.transform.eulerAngles.y, board.transform.eulerAngles.z);
				}
				else
				{
					//board.transform.eulerAngles = to;
					EvenBoardLerpFinished = true;
					boardLerpValuesSet = false;
				}
			}
		}
		private void FlipBoardToInvertedRotation()
		{
			if (!boardLerpValuesSet)
			{
				SetBoardLerpStartValues();
			}

			if (!flippingFirstSequenceFinished)
			{
				if (Mathf.Round(board.transform.eulerAngles.x) != 340)
				{
					board.transform.eulerAngles = new Vector3(board.transform.eulerAngles.x - 100 * Time.deltaTime, board.transform.eulerAngles.y, board.transform.eulerAngles.z);
				}
				else
				{
					flippingFirstSequenceFinished = true;
					boardLerpValuesSet = false;
				}
			}
		}
		//** Board Flipping end **//

		#region Elephant related methods
		private void SetElephantJumpStartValues()
		{
			elephantStartPosition = elephant.transform.position;
			elephantJumpStartTime = Time.time;
			elephantJumpStartDistance = Vector3.Distance(elephant.transform.position, boardEnd.transform.position);
			elephantJumpTargetPosition = new Vector3(elephant.transform.position.x + (elephantJumpStartDistance * 1.5f), elephant.transform.position.y, elephant.transform.position.z);
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
					if ((int)elephant.transform.position.x == (int)elephantJumpTargetPosition.x)
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

		private void ElephantWalkOff()
		{
			//Let the elephant walk off the board
			if ((int)elephant.transform.localPosition.z != (int)elephantSecondPosition.z)
			{
				//Add smooth walk...... GOD DAMNIT NAV MESH U MOTHAFUCKA
				elephant.transform.localPosition = elephantSecondPosition;
			}

			if (board.transform.eulerAngles.x < 20)
			{
				//Rotate board
				board.transform.eulerAngles = new Vector3(board.transform.eulerAngles.x + 20 * Time.deltaTime, board.transform.eulerAngles.y, board.transform.eulerAngles.z);

				playerController.EnableMovement();
			}
			else
			{
				board.GetComponent<Rigidbody>().isKinematic = false;
			}
		}
		#endregion Elephant

	}

}

