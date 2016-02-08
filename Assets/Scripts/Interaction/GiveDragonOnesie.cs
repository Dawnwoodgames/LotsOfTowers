using UnityEngine;
using System.Collections;
using Nimbi.Actors;
using Nimbi.UI;

namespace Nimbi.Interaction.Triggers
{
    public class GiveDragonOnesie : MonoBehaviour
    {

        //Aanpasje voor bob
        public Onesie dragonOnesie;

        private GameObject player;

        private bool abletoGiveOnesie = false;
        private bool dragonInTrigger = false;
        private bool playerInTrigger = false;
        private bool onesieGiven = false;

        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Submit") && dragonInTrigger && playerInTrigger)
            {
                if (!onesieGiven && abletoGiveOnesie)
                {
                    showOnesiePopupAndGiveOnesie();
                }
            }
        }


        private void showOnesiePopupAndGiveOnesie()
        {
            player.GetComponent<Player>().AddOnesie(dragonOnesie);
            GameObject.Find("CenterFocus").GetComponent<OnesieInfoPopup>().ShowPopup(OnesieType.Dragon, 0);
            onesieGiven = true;
        }

        private void OnTriggerEnter(Collider coll)
        {
            if(coll.tag == "Daisy")
            {
                dragonInTrigger = true;
                abletoGiveOnesie = true;
            }
            if(coll.tag == "Player")
            {
                playerInTrigger = true;
            }
        }

    }

}

