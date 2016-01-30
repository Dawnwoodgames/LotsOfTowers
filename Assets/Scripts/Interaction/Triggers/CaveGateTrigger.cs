using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction
{

    public class CaveGateTrigger : MonoBehaviour
    {
        private Player player;
        private bool inTrigger;
        public ScaryStatue scaryStatue;
        private bool isPushed;

        public float pushBackRate = 10;


        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            inTrigger = false;

            
        }

        void Update()
        {
            if (inTrigger && scaryStatue.isScary)
            {
                isPushed = true;
                    player.GetComponent<Rigidbody>().AddForce(Vector3.right * pushBackRate, ForceMode.VelocityChange);
            }
        }

        //If Nimbi is going to Touch the Invisible Wall
        private void OnTriggerStay(Collider coll)
        {
            if (coll.attachedRigidbody)
            {
                inTrigger = true;
            }
        }


        //If Nimbi gets Pushed back!
        private void OnTriggerExit()
        {
            inTrigger = false;
        }


    }
}