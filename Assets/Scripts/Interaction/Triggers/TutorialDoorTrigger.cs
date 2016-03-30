using UnityEngine;
using System.Collections;
using System;

namespace Nimbi.Interaction.Triggers
{
    public class TutorialDoorTrigger : MonoBehaviour
    {
        public GameObject infoBoard, bell;
        public Transform gate;
        public bool doorBellPickedUp = false;
        public float degreesPerSecond, rotationDegreesAmount;

        private bool doorUnlocked = false;
        private Quaternion startRotation, endRotation;
        private float time = 0;
        private float totalRotation;

        void Start()
        {
            startRotation = new Quaternion(0, 0, 0, 1);
            endRotation = new Quaternion(0, 0, 0, 1);
        }

        private void Update()
        {
            if (doorUnlocked)
            {
                if (Mathf.Abs(totalRotation) < Mathf.Abs(rotationDegreesAmount))
                    OpenDoor();
                else
                {
                    Destroy(this);
                }
            }
        }

        private void OpenDoor()
        {
            float currentAngle = gate.rotation.eulerAngles.y;
            gate.rotation = Quaternion.AngleAxis(currentAngle + (Time.deltaTime * degreesPerSecond), Vector3.up);
            totalRotation += Time.deltaTime * degreesPerSecond;
        }

        private void OnTriggerStay(Collider coll)
        {
            if (coll.tag == "Player" && Input.GetButton("Submit"))
            {
                if (doorBellPickedUp)
                {
                    doorUnlocked = true;
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl.MainCameraScript>().cameraEnabled = true;
                    bell.transform.parent = GameObject.Find("FirstPlatform").transform;
                    bell.transform.localPosition = new Vector3(-0.85f, 0.14f, 4.85f);
                }

                else {
                    infoBoard.SetActive(true);
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl.MainCameraScript>().cameraEnabled = true;
                }
            }
        }
        
        private void OnTriggerExit(Collider coll)
        {
            if (coll.tag == "Player")
                infoBoard.SetActive(false);
        }
    }
}