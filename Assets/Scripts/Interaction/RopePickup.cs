using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{
    public class RopePickup : MonoBehaviour
    {

        private bool inTrigger;
        private bool pickedUp;
        public GameObject player;
        public bool connected;

        void OnTriggerEnter(Collider coll)
        {
            if(coll.tag == "Player")
            {
                inTrigger = true;
            }
            else if(!connected && coll.gameObject.GetComponent<RopeAnchor>() != null)
            {
                transform.parent = coll.transform.parent;
                transform.position = coll.transform.position;
                connected = true;
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
                transform.localPosition = new Vector3(0,0.5f,0);
            }
        }
    }
}