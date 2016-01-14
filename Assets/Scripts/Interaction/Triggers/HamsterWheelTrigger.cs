using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
    public class HamsterWheelTrigger : MonoBehaviour
    {

        public GameObject wheel;

        private bool playerRunning = false;

        void Start()
        {

        }

        void Update()
        {
            if (playerRunning)
                wheel.transform.Rotate(3f, 0, 0);
        }

        private void OnTriggerStay(Collider coll)
        {
            if (coll.tag == "Player")
                playerRunning = true;
        }

        private void OnTriggerExit(Collider coll)
        {
            if (coll.tag == "Player")
                playerRunning = false;
        }

        public bool GetPlayerRunning() { return playerRunning; }
    }
}