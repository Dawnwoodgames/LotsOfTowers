using UnityEngine;
using System.Collections;

using LotsOfTowers.Framework;

namespace LotsOfTowers.Interaction
{

    public class OpenDoorLever : MonoBehaviour
    {
        public GameObject targetDoor;

        public float targetX;
        public float targetY;
        public float targetZ;

        private bool inTrigger;
        private bool doorOpen = false;

        private float defaultX;
        private float defaultY;
        private float defaultZ;

        void Start()
        {
            defaultX = targetDoor.transform.position.x;
            defaultY = targetDoor.transform.position.y;
            defaultZ = targetDoor.transform.position.z;
        }

        void Update()
        {
            if (inTrigger && Input.GetButtonDown("Submit"))
            {
                if(doorOpen)
                {
                    CloseDoor();
                }
                else
                {
                    OpenDoor();
                }
            }
        }

        private void OnCollisionEnter(Collision coll)
        {
            if (coll.gameObject.tag == "Player")
            {
                inTrigger = true;
            }
        }
        private void OnCollisionExit()
        {
            inTrigger = false;
        }

        // Open the door to the given position.
        private void OpenDoor()
        {
            targetDoor.transform.localPosition = new Vector3(targetX, targetY, targetZ); // Move position
            targetDoor.transform.Rotate(new Vector3(0, 90, 0)); // Rotate, so it `opens`
            doorOpen = true;
        }

        private void CloseDoor()
        {
            targetDoor.transform.position = new Vector3(defaultX, defaultY, defaultZ); // Move position
            targetDoor.transform.Rotate(new Vector3(0, 90, 0)); // Rotate, so it `closes`
            doorOpen = false;
        }


    }
}