using UnityEngine;
using System.Collections;
using Nimbi.Actors;
using Nimbi.Interaction.Triggers;

namespace Nimbi.Interaction
{
    public class PipebombExplosion : MonoBehaviour
    {
        public GameObject pipeBomb;
        public StoneExplosionScript stoneExplosion;

        private GameObject player;
        private bool nearPipebomb;
        private bool exploded;

        
        

        void Update()
        {
            if(!exploded && nearPipebomb && Input.GetButton("Submit") && player.GetComponent<Player>().Onesie.isHeavy)
            {
                ActivateExplosion();
            }
               
        }

        void ActivateExplosion()
        {
            if (player.GetComponent<Player>().HoldingWater)
            {
                exploded = true;
                player.GetComponent<Player>().HoldingWater = false;
                stoneExplosion.Explode();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                nearPipebomb = true;
                player = other.gameObject;
            }


        }

        void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
                nearPipebomb = false;
        }
    }

}

