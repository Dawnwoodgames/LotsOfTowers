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

		void Start()
		{
			player = GameObject.FindGameObjectWithTag("Player");
			playerController = player.GetComponent<PlayerController>();

			boardStartTrigger = boardStart.GetComponent<SeesawLibraBoardTrigger>();
			boardEndTrigger = boardEnd.GetComponent<SeesawLibraBoardTrigger>();

			//Nice code bra
			elephantSecondPosition = new Vector3(GameObject.Find("HelpNimbiFromHere").transform.localPosition.x - 1, GameObject.Find("HelpNimbiFromHere").transform.localPosition.y + 1.65f, GameObject.Find("HelpNimbiFromHere").transform.localPosition.z);
        }

		void FixedUpdate()
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
			else if(boardStartTrigger.isPlayerOnTrigger())
			{
				Debug.Log("We are finished with part one, join us on the next episode on NIMBI!");

				StartCoroutine(WaitForBalance(0.5f));
			}
			else if(boardEndTrigger.isPlayerOnTrigger())
			{
				board.GetComponent<Rigidbody>().isKinematic = true;		
				playerController.DisableMovement();

				if (!player.GetComponent<Player>().Onesie.isElephant)
				{
					// Elephant jumps which will launch you to the next platform.
					if (elephant.transform.localPosition.x > 0.5f)
					{
						elephant.transform.localPosition += Vector3.left;
					}
					else
					{
						StartCoroutine(WaitForPlayerLaunch(0.5f));
					}
				}
				else if(!puzzleFinished)
				{
					Debug.Log("Take off your onesie to continue!!");
				}
			}
		}

		IEnumerator WaitForBalance(float amount)
		{
			yield return new WaitForSeconds(amount);

			// Elephant walks of and enables moving for player so he can walk to the other side
			ElephantWalkOff();
		}

		IEnumerator WaitForPlayerLaunch(float amount)
		{
			yield return new WaitForSeconds(amount);

			// Elephant walks of and enables moving for player so he can walk to the other side
			LaunchPlayerUp();
		}

		private void LaunchPlayerUp()
		{
			//First turn board
			if ((int)board.transform.eulerAngles.x != 20)
			{
				board.transform.eulerAngles = new Vector3(board.transform.eulerAngles.x + 20 * Time.deltaTime, board.transform.eulerAngles.y, board.transform.eulerAngles.z);
			}
			else
			{
				Debug.Log("You did it! Enjoy being stuck at this part cause the box is too damn high");
				puzzleFinished = true;
				playerController.EnableMovement();
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

		#region Elephant related methods
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

		private void ElephantWalkOff()
		{
			//Let the elephant walk off the board
			if (elephant.transform.localPosition != elephantSecondPosition)
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

