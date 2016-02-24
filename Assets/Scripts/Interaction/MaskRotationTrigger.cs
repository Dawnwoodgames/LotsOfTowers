using UnityEngine;
using System.Collections;
using Nimbi.Actors;
using Nimbi.Interaction.Triggers;

namespace Nimbi.Interaction
{
    public class MaskRotationTrigger : MonoBehaviour
    {

        public MaskHappyTrigger happyTrigger;
        public GameObject completeDoor;

        //Public values for chaning in Editor
        public float rotationSpeed = 10f;
        public float rotateBackSpeed = 10f;
        public float pushBackRate = 30;


        //Public Booleans So we can check states in other classes.
        public bool isCheating;
        public bool isSpinning;
        public bool isScary = true;

        //Private Objects for Inner Class purposes only
        private GameObject player;
        private Quaternion startRotation;
        private int rotationCount;


        private bool inTrigger;


        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            startRotation = transform.localRotation;
        }

        public void Update()
        {

            if (rotationSpeed > 0)
            {
                isSpinning = true;
                isCheating = true;
                transform.Rotate(rotationSpeed, 0, 0);
            }

            rotationSpeed -= Time.deltaTime / 1f;


            if (rotationSpeed <= 0 && !happyTrigger.isHappy)
            {              
                transform.localRotation = Quaternion.Slerp(transform.localRotation, startRotation, 0.1f);
                if (Mathf.Abs(transform.localRotation.eulerAngles.x - startRotation.eulerAngles.x) < 0.1f)
                    rotationSpeed = 10;
            }

        }


        public void CheckMaskPosition()
        {
            if (happyTrigger.isHappy == true)
            {
                completeDoor.transform.rotation = Quaternion.Euler(0, 338.0217f, 88.9f);
            }
        }


        public void PushNimbiAway()
        {
            Debug.Log("You are not Heavy enough, Wimbi!");
            player.GetComponent<Rigidbody>().AddForce(Vector3.right * pushBackRate, ForceMode.Impulse);
        }
    }
}