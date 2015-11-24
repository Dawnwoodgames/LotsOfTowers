using UnityEngine;
using System.Collections;

using LotsOfTowers.Framework;

namespace LotsOfTowers.Interaction
{

    public class OpenDoorLever : MonoBehaviour
    {
        public GameObject targetDoor;
        public OpenDoorDirection doorDirection;

        private bool inTrigger;

        void Update()
        {
            if (Input.GetButtonDown("Submit"))
            {
                Debug.Log("HH");
            }
            

            if (Input.GetButtonDown("Submit") && inTrigger)
            {
                Debug.Log("H");
            }
        }

        private void OnCollisionEnter(Collision coll)
        {
            if (coll.gameObject.tag == "Player")
            {
                inTrigger = true;
            }
        }



    }
}