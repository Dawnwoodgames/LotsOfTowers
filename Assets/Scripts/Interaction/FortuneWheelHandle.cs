using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction
{
    public class FortuneWheelHandle : MonoBehaviour
    {
        public bool isActivated;
        public GameObject lever;
        public MaskRotationTrigger mask;

        private bool inTrigger;
        private Vector3 endRotation;

        void Start()
        {
            endRotation = new Vector3(314, 0, 0);
        }


        void Update()
        {
            //Animate Lever down.
            if (inTrigger && Input.GetButtonDown("Submit") && !isActivated)
            {
                endRotation = endRotation + new Vector3(55, 0, 0);
                isActivated = true;
                mask.isActivated = true;
                mask.rotationSpeed = 10f;
            }

     
            lever.transform.localRotation = Quaternion.Slerp(lever.transform.localRotation, Quaternion.Euler(endRotation), 0.2f);
                
                
        }


        //Animate Lever Up.
        public void returnLever()
        {
            endRotation = endRotation - new Vector3(55, 0, 0);
            isActivated = false;
        }

        void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
            {
                inTrigger = true;
            }
        }


    }
}


