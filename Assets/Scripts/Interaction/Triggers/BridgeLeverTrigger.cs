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
        private bool isMoving;

        void Start()
        {
            endRotation = new Vector3(314, 0, 0);
            endPosition = new Vector3(bridge.transform.localPosition.x, -17.808f, bridge.transform.localPosition.z);
        }

        void Update()
        {
            if (inTrigger && Input.GetButtonDown("Submit") && !isActivated)
            {
                endRotation = endRotation + new Vector3(55, 0, 0);
                isActivated = true;
                isMoving = true;
            }

            if (bridge != null && isMoving)
            {

                bridge.transform.localPosition = Vector3.MoveTowards(bridge.transform.localPosition, endPosition, bridgeSpeed);
                if (bridge.transform.localPosition == endPosition)
                {
                    isMoving = false;
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



