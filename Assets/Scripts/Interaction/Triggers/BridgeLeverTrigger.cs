using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction.Triggers
{
    public class BridgeLeverTrigger : MonoBehaviour
    {
        public GameObject bridge;
        public GameObject handle;
        public float bridgeSpeed = 0.2f;

        private Vector3 startPosition;
        private Vector3 endRotation;
        private Vector3 endPosition;

        private bool inTrigger;
        private bool isActivated;

        void Start()
        {
            endRotation = new Vector3(314, 0, 0);
            endPosition = new Vector3(0.06480163f, -17.818f, 6.500424f);
        }

        void Update()
        {
            if (inTrigger && Input.GetButtonDown("Submit") && !isActivated)
            {
                endRotation = endRotation + new Vector3(55, 0, 0);
                isActivated = true;
                if (bridge != null)
                {
                    bridge.transform.localPosition = Vector3.Lerp(startPosition, endPosition, Time.time * bridgeSpeed);
                    
                }
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



