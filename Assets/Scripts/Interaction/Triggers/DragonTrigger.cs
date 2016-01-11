using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction.Triggers
{
    public class DragonTrigger : MonoBehaviour {

        private ScaryStatue scaryStatue;
        private bool inTrigger;

        // Use this for initialization
        void Start() {
            scaryStatue = GameObject.Find("Scary-Statue").GetComponent<ScaryStatue>();
            inTrigger = false;
        }

        // Update is called once per frame
        void Update() {
            if   (Input.GetButtonDown("Submit") && inTrigger && scaryStatue.isScary)
            {

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
