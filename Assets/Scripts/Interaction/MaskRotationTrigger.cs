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
        public FortuneWheelHandle handle;
        public Transform endPosition;

        //Public values for chaning in Editor
        public float rotationSpeed = 10f;
        public float rotateBackSpeed = 10f;
        public float pushBackRate = 30;


        //Public Booleans So we can check states in other classes.
        public bool isCheating;

        public bool isSpinning; //Is Our wheel spinning? Or do we need to change it to an fast spinning bool instead.
        public bool isScary = true;
        public bool isActivated;

        //Private Objects for Inner Class purposes only
        private GameObject player;
        private Quaternion startRotation;
        private int rotationCount;
        private Vector3 layDownRotation;


        private bool inTrigger;


        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            startRotation = transform.localRotation;
            layDownRotation = new Vector3(0, 338.0217f, 89);
            
        }

        public void Update()
        {
            if (isActivated)
            {
                if (rotationSpeed > 0)
                {
                    isCheating = true;
                    transform.Rotate(rotationSpeed, 0, 0);
                }

                rotationSpeed -= Time.deltaTime / 1f;


                if (rotationSpeed <= 0 && !happyTrigger.isHappy)
                {
                    transform.localRotation = Quaternion.Slerp(transform.localRotation, startRotation, 0.1f);
                    if (Mathf.Abs(transform.localRotation.eulerAngles.x - startRotation.eulerAngles.x) < 0.1f)
                    {
                        isActivated = false;
                        handle.returnLever();
                    }
                }
                if(happyTrigger.isHappy && !isScary)
                {
                    DropDown();
                }
            }

        }


        public void CheckMaskPosition()
        {
            if (happyTrigger.isHappy == true)
            {
                isScary = false;
            }
        }

        public void DropDown()
        {
            completeDoor.transform.rotation = Quaternion.Slerp(completeDoor.transform.rotation, Quaternion.Euler(layDownRotation), 0.2f);
        }

        public void PushNimbiAway()
        {
            if (isScary && isCheating)
                //player.GetComponent<Rigidbody>().AddForce(-transform.forward * pushBackRate, ForceMode.Impulse);
                player.transform.position = endPosition.transform.position;
        }
    }
}