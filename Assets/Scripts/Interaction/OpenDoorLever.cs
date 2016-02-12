using UnityEngine;
using System.Collections;

using Nimbi.Framework;

namespace Nimbi.Interaction
{

    public class OpenDoorLever : MonoBehaviour
    {
        public GameObject targetDoor;
        public GameObject wind;
        public Material material;

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
            material.color = Color.red;
        }

        void Update()
        {
            if (inTrigger && Input.GetButtonDown("Submit"))
            {
                if(doorOpen)
                {
                    CloseDoor();
                    GetComponent<Animator>().SetBool("GoingDown", false);
                    material.color = Color.red;
                }
                else
                {
                    OpenDoor();
                    GetComponent<Animator>().SetBool("GoingDown", true);
                    material.color = Color.green;
                }
            }
        }

        void OnTriggerEnter(Collider coll)
        {
            if (coll.gameObject.tag == "Player")
            {
                inTrigger = true;
            }
        }
        void OnTriggerExit(Collider coll)
        {
            if (coll.gameObject.tag == "Player")
            {
                inTrigger = false;
            }
        }

        // Open the door to the given position.
        private void OpenDoor()
        {
            targetDoor.transform.localPosition = new Vector3(targetX, targetY, targetZ); // Move position
            targetDoor.transform.Rotate(new Vector3(0, 90, 0)); // Rotate, so it `opens`
            wind.transform.position = new Vector3(transform.position.x, transform.position.y, 16.5f);
            doorOpen = true;
        }

        private void CloseDoor()
        {
            targetDoor.transform.position = new Vector3(defaultX, defaultY, defaultZ); // Move position
            targetDoor.transform.Rotate(new Vector3(0, 90, 0)); // Rotate, so it `closes`
            wind.transform.position = new Vector3(transform.position.x, transform.position.y, 11.75f);
            doorOpen = false;
        }


    }
}