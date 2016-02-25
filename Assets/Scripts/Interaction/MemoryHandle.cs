using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{
    public class MemoryHandle : MonoBehaviour
    {

        public GameObject rock;
        public GameObject handle;
        private bool inTrigger;

        private Vector3 endRotation;


        private bool isActivated;

        void Start()
        {
            endRotation = new Vector3(314, 0, 0);
        }

        void Update()
        {
            if(inTrigger && Input.GetButtonDown("Submit") && !isActivated)
            {
                endRotation = endRotation + new Vector3(55, 0, 0);
                isActivated = true;
                if (rock!= null)
                    Destroy(rock);
            }

            handle.transform.localRotation = Quaternion.Slerp(handle.transform.localRotation, Quaternion.Euler(endRotation), 0.2f);
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