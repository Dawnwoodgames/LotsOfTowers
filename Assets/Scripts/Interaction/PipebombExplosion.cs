using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction
{
    public class PipebombExplosion : MonoBehaviour
    {
        public GameObject blockade;
        public GameObject pipeBomb;

        private GameObject player;
        private bool nearPipebomb;
        

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if(nearPipebomb && Input.GetButton("Submit") && player.GetComponent<Player>().Onesie.isHeavy)
            {
                print("Spuug maar lekker watertje!");
                ActivateExplosion();
            }
               
        }

        void ActivateExplosion()
        {
            if (player.GetComponent<Player>().HoldingWater)
            {
                player.GetComponent<Player>().HoldingWater = false;
                Destroy(pipeBomb);
                Destroy(blockade);
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

