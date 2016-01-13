using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{
    public class RopePickup : MonoBehaviour
    {

        private bool inTrigger;
        private bool pickedUp;
        public GameObject player;

        void OnTriggerEnter(Collider coll)
        {
            if(coll.tag == "Player")
            {
                inTrigger = true;
            }
        }

        void OnTriggerExit(Collider coll)
        {
            if (coll.tag == "Player")
            {
                inTrigger = false;
            }
        }

        void Update()
        {
            if(inTrigger && Input.GetButton("Submit"))
            {
                transform.parent = player.transform;
            }
        }
    }
}