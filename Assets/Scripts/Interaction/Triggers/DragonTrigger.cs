using UnityEngine;
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
        private ScaryStatue scaryStatue;
        private bool inTrigger;


        // Use this for initialization
        void Start() {
  
            scaryStatue = GameObject.Find("Scary-Statue").GetComponent<ScaryStatue>();
            inTrigger = false;
            flyPosition = transform.position;

        }

        // Update is called once per frame
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
