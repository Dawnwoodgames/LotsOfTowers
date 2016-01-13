using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction.Triggers
{
    public class DragonTrigger : MonoBehaviour {

        private ScaryStatue scaryStatue;
        private bool inTrigger;
        private Vector3 jumpSpeed;

        // Use this for initialization
        void Start() {
  
            scaryStatue = GameObject.Find("Scary-Statue").GetComponent<ScaryStatue>();
            inTrigger = false;
            jumpSpeed = new Vector3(transform.position.x + 2f, transform.position.y + 2f, transform.position.z + 1f);
        }

        // Update is called once per frame
        void Update() {
            BraveDragon();
        }

        private void BraveDragon()
        {
            if (!scaryStatue.isScary)
            {
                //Check if Statue is still scary!
                transform.position = Vector3.Lerp(transform.position, jumpSpeed, 2);
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
