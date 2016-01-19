﻿using UnityEngine;
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


        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            inTrigger = false;

            
        }

        // Update is called once per frame
        void Update()
        {
            if (inTrigger && scaryStatue.isScary)
            {
                isPushed = true;
                    player.GetComponent<Rigidbody>().AddForce(Vector3.left * pushBackRate, ForceMode.VelocityChange);
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