﻿using UnityEngine;
using System.Collections;
using Nimbi.Actors;
using Nimbi.Framework;
using Nimbi.Interaction.Triggers;
using UnityEngine.SceneManagement;
using Nimbi.CameraControl;

namespace Nimbi.Interaction
{
	public class CompleteFan : MonoBehaviour
	{
        public ParticleSystem fire;
        public GameObject tower;
        public LevelSlider slider;
        public FurnaceDoor furnaceDoor;
        public bool completed;
        public GameObject finishDialogOne;
        public GameObject finishDialogTwo;

        private Animator animator;
		private bool inTrigger = false;
		private bool isHeavy = false;
		private Player player;
        private int blowCount;
        private bool currentlyDown = false;
        private bool dialogueFinished = false;
        private bool completionstarted;

        private Vector3 endMarker;
        private float speed = 15f;

        void Start()
		{
			animator = GetComponentInParent<Animator>();
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            endMarker = new Vector3(slider.transform.position.x, slider.transform.position.y - 15f, slider.transform.position.z + 1f);
        }

		void OnTriggerStay(Collider coll)
		{
			if (coll.tag == "Player")
			{
				inTrigger = true;
			}
		}

		void OnTriggerExit(Collider coll)
		{
			if (coll.tag == "Player")
			{
				inTrigger = false;
			}
		}

		void Update()
		{
			isHeavy = player.Onesie.isHeavy;

            if (completed)
            {
                tower.transform.position = new Vector3(tower.transform.position.x, tower.transform.position.y, tower.transform.position.z - Time.deltaTime * 3);
				Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y + Time.deltaTime, Camera.main.transform.localPosition.z - Time.deltaTime * 3);
				player.GetComponent<PlayerController>().enabled = false;
                Camera.main.transform.parent.GetComponent<CameraFollowScript>().canZoom = false;

                MoveLevelSliderDown();

                if(!dialogueFinished)
                {
                    UnityAnalytics.FinishSegment("Tower 3 Last Floor");
                    finishDialogOne.SetActive(true);
                    StartCoroutine(ActivateSecondDialogue());
                    
                }
                if(!completionstarted)
                    StartCoroutine(CompletedLevel());

                completionstarted = true;
                
            }

			if (inTrigger && isHeavy && furnaceDoor.isDoorOpen() && !completed)
			{
				animator.SetBool("GoingDown", true);
				animator.SetBool("GoingUp", false);
                if (!currentlyDown && blowCount < 3)
                {
                    blowCount += 1;
                    fire.maxParticles += 10;
                    fire.startLifetime += 0.3f;
                    fire.startSize += 0.2f;
                }
                if (blowCount >= 3)
                {
                    completed = true;
                    UnityAnalytics.CompleteLevel(SceneManager.GetActiveScene().name, Mathf.RoundToInt(Time.time - GameManager.Instance.levelStart));
                }

                currentlyDown = true;
			}
			else
			{
				animator.SetBool("GoingDown", false);
				animator.SetBool("GoingUp", true);
                currentlyDown = false;
			}
		}
        
        // This is needed to close the ship :-).
        private void MoveLevelSliderDown()
        {
            slider.transform.position = Vector3.MoveTowards(slider.transform.position, endMarker, Time.deltaTime * speed);
        }

        IEnumerator ActivateSecondDialogue()
        {
            yield return new WaitForSeconds(4);
            finishDialogOne.SetActive(false);
            finishDialogTwo.SetActive(true);
            dialogueFinished = true;
        }

        IEnumerator CompletedLevel()
		{
			yield return new WaitForSeconds(15);
			GameManager.Instance.LoadLevel(4);
		}
	}
}