using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction.Triggers
{
    public class DragonTrigger : MonoBehaviour {

        public GameObject dragon;
        public float horizonSpeed;
        public float verticalSpeed;
        public Transform[] walkspots;
        public float aplitude;

        private bool isWalking;
        private int nextPosition;

        //Fields for Scary Statue
        public ScaryStatue scaryStatue;
        private bool inTrigger = false;
        
        void Start() {
            
        }

        void FixedUpdate() {
            BraveDragon();
        }

        private void BraveDragon()
        {
            if (!scaryStatue.isScary)
            {
               if(isWalking)
                {
                    dragon.GetComponent<Animator>().SetBool("isWalking", true);
                }
                    
               
            }
        }

        private void OnTriggerStay(Collider coll)
        {
           if(coll.attachedRigidbody)
            {
                inTrigger = true;
            }
        }
    }
}
