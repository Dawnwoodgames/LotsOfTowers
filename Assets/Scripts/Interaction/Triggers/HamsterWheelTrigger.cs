using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
    public class HamsterWheelTrigger : MonoBehaviour
    {

        public GameObject wheel;
        public bool negativeRotate;

        private bool playerRunning = false;
        private float rotateSpeed = 3f;

        void Start()
        {
            rotateSpeed = (negativeRotate) ? -rotateSpeed : rotateSpeed;
        }

        void Update()
        {
            if (playerRunning)
                wheel.transform.Rotate(rotateSpeed, 0, 0);
        }

        private void OnTriggerStay(Collider coll)
        {
            if (coll.tag == "Player" && coll.GetComponent<Actors.Player>().Onesie.type == Actors.OnesieType.Hamster)
                playerRunning = true;
        }

        private void OnTriggerExit(Collider coll)
        {
            if (coll.tag == "Player")
                playerRunning = false;
        }

        public bool GetPlayerRunning() { return playerRunning; }
        public bool GetNegativeRotate() { return negativeRotate; }
    }
}