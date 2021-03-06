﻿using UnityEngine;
using System.Collections;
using Nimbi.Actors;
using Nimbi.Framework;
using UnityEngine.SceneManagement;

namespace Nimbi.Interaction.Triggers
{
    public class LevelFourToFiveTransition : MonoBehaviour
    {
        public GameObject PushNimbiOffHere;
        public GameObject PushOffObject;
        public GameObject disableThisCollider;

        private Player player;
        private PlayerController playerController;

        private bool triggered = false;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            playerController = player.gameObject.GetComponent<PlayerController>();
        }

        void FixedUpdate()
        {
            if(triggered)
            {
                disableThisCollider.GetComponent<MeshCollider>().enabled = false;

                if (!PushOffObject.activeInHierarchy)
                {
                    PushOffObject.SetActive(true);
                }
                else
                {
                    PushOffObject.transform.position = Vector3.MoveTowards(PushOffObject.transform.position, player.gameObject.transform.position, Time.deltaTime * 50);
                }   
            }
        }

        void OnTriggerEnter(Collider col)
        {
            if(col.tag == "Player")
            {
                triggered = true;
                playerController.DisableMovement();
            }

            if(col.name == PushOffObject.name)
            {
                Destroy(GameObject.Find("DeathTrigger"));
                StartCoroutine(TransiteToTowerFive(1f));
            }
           
        }
        
        IEnumerator TransiteToTowerFive(float amount)
        {
            yield return new WaitForSeconds(amount);
            TransitionToTowerFive();
        }
       
        private void TransitionToTowerFive()
        {
            UnityAnalytics.CompleteLevel(SceneManager.GetActiveScene().name, Mathf.RoundToInt(Time.time - GameManager.Instance.levelStart));
            GameManager.Instance.LoadLevel(6);
        }

    }

}
