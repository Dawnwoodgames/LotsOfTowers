using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{
    public class MemoryHandle : MonoBehaviour
    {

        public GameObject rock;
        private bool inTrigger;

        void Update()
        {
            if(inTrigger && Input.GetButtonDown("Submit"))
            {
                if(rock!= null)
                    Destroy(rock);
            }
        }

        void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
                inTrigger = true;
        }

        void OnTriggerExit(Collider coll)
        {
            if (coll.tag == "Player")
                inTrigger = false;
        }
    }
}