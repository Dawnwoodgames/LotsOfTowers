using UnityEngine;
using System.Collections;
using Nimbi.Actors;
using Nimbi.Interaction.Triggers;


namespace Nimbi.Interaction
{
    public class CheatingMaskTrigger : MonoBehaviour
    {

        private bool inTrigger;
        private GameObject player;
        public MaskRotationTrigger cheatingMask;

        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            
        }

        // Update is called once per frame
        void Update()
        {
            if(inTrigger && cheatingMask.isCheating)
            {
                CheckNimbiState();
            }
        }

        private void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
            {
                Debug.Log("Peg is hitted");
                inTrigger = true;
            }
        }

        //If Nimbi gets Pushed back!
        private void OnTriggerExit()
        {
            inTrigger = false;
        }


        public void CheckNimbiState()
        {
            if (!player.GetComponent<Player>().Onesie.isHeavy)
            {
                cheatingMask.PushNimbiAway();
            }
            else
            {
                cheatingMask.isSpinning = false;
                cheatingMask.rotationSpeed = 0;
                cheatingMask.CheckMaskPosition();
            }
           
            
        }
    }




}
