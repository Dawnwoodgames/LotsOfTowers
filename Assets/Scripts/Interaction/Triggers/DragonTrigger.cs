﻿using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction.Triggers
{
    public class DragonTrigger : MonoBehaviour {

        public float horizonSpeed;
        public float verticalSpeed;
        public float aplitude;
        private Vector3 flyPosition;

        //Fields for Scary Statue
        public ScaryStatue scaryStatue;
        private bool inTrigger = false;
        
        void Start() {
            flyPosition = transform.position;
        }

        void FixedUpdate() {
            BraveDragon();
        }

        private void BraveDragon()
        {
            if (!scaryStatue.isScary)
            {
                //Check if Statue is still scary!
                flyPosition.x += horizonSpeed;
                flyPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * aplitude;
                transform.position = flyPosition;
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
