using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
    public class HamsterWheelTrigger : MonoBehaviour
    {

        public GameObject wheel;
        public bool x, y, z, negative;
        public float speed;
        private Vector3 rotation;
        public Vector3 totalRotation;
        private bool stopped;

        private bool playerRunning = false;

        void Start()
        {
            rotation = new Vector3();
            totalRotation = new Vector3();
        }

        void Update()
        {
            if (playerRunning && !stopped)
            {
                rotation = new Vector3(speed * (x ? 1 : 0), speed * (y ? 1 : 0), speed * (z ? 1 : 0))*(negative?-1:1);
                totalRotation += rotation;
                wheel.transform.Rotate(rotation.x, rotation.y, rotation.z);
            }
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

        public void Stop()
        {
            this.stopped = true;
        }
    }
}