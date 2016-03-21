using UnityEngine;
using System.Collections;
using Nimbi;

namespace Nimbi.Interaction.Triggers
{
    public class DoorOpenTrigger : MonoBehaviour
    {

        public GameObject leftDoor;
        public GameObject rightDoor;

        private Quaternion startRotation;
        private Quaternion endRotation;

        public float rotatePerSecond, rotationAmount;


        private float leftTotalRotation;
        private float rightTotalRotation;

        private bool inTrigger;

        // Use this for initialization
        void Start()
        {
            startRotation = new Quaternion(0, 0, 0, 1);
            endRotation = new Quaternion(0, 0, 0, 1);
        }

        // Update is called once per frame
        void Update()
        {
            if (Mathf.Abs(leftTotalRotation) < Mathf.Abs(rotationAmount) && Mathf.Abs(rightTotalRotation) < Mathf.Abs(rotationAmount) && inTrigger)
            {
                OpenDoors();
            }
                
        }

        void OnTriggerEnter(Collider coll)
        {
            if(coll.tag == "Player")
            {
                inTrigger = true;
            }
        }

        void OpenDoors()
        {
            {
                //Code for Left Door because it goes in the opposite direction of the right Door
                float leftCurrentAngle = leftDoor.transform.rotation.eulerAngles.y;
                leftDoor.transform.rotation = Quaternion.AngleAxis(leftCurrentAngle + (Time.deltaTime * rotatePerSecond), Vector3.up);
                leftTotalRotation += Time.deltaTime * rotatePerSecond;


                //Right Door Code
                float rightCurrentAngle = rightDoor.transform.rotation.eulerAngles.y;
                rightDoor.transform.rotation = Quaternion.AngleAxis(rightCurrentAngle - (Time.deltaTime * rotatePerSecond), Vector3.up);
                rightTotalRotation += Time.deltaTime * rotatePerSecond;
            }     
        }
    }

}