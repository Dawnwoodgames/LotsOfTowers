using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction
{

    public class CaveGateTrigger : MonoBehaviour
    {
        private Player player;
        private bool inTrigger = false;
        public ScaryStatue scaryStatue;

        public float pushBackRate = 10;


        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        void Update()
        {
            if (inTrigger && scaryStatue.isScary)
            {
                player.GetComponent<Rigidbody>().AddForce(Vector3.right * pushBackRate, ForceMode.VelocityChange);
            }
        }

        //If Nimbi is going to Touch the Invisible Wall
        private void OnTriggerStay(Collider coll)
        {
            if (coll.attachedRigidbody)
            {
                Debug.Log("Peg hitted");
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