using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
    public class TutorialDoorTrigger : MonoBehaviour
    {
        public GameObject infoBoard;
        public GameObject door;
        public bool doorBellPickedUp = false;

        private bool openingDoor = false;
        private Quaternion startRotation, endRotation;
        private float time = 0;

        void Start()
        {
            startRotation = new Quaternion(0, 0, 0, 1);
            endRotation = new Quaternion(0, 0, 0, 1);
            startRotation = Quaternion.Euler(0, 0, 0);
            endRotation = Quaternion.Euler(0, 1, 0);
        }

        private void Update()
        {
            if (openingDoor)
            {
                Destroy(GameObject.Find("DoorBell"));
                door.transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
                time += 0.03f;
            }
        }

        private void OnTriggerStay(Collider coll)
        {
            if (coll.tag == "Player" && Input.GetButton("Submit"))
            {
                if (doorBellPickedUp)
                {
                    openingDoor = true;
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl.MainCameraScript>().cameraEnabled = true;
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