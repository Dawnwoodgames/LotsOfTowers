using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction.Triggers
{
    public class RotateTrigger : MonoBehaviour
    {

        public GameObject wheel;
        public bool x, y, z, negative;
        public float speed;
        private Vector3 rotation;
        public Vector3 totalRotation;
        private bool stopped;
        public bool hamsterRequired;

        private bool inTrigger = false;

        void Start()
        {
            rotation = new Vector3();
            totalRotation = new Vector3();
        }

        void Update()
        {
            if (inTrigger && !stopped)
            {
                rotation = new Vector3(speed * (x ? 1 : 0), speed * (y ? 1 : 0), speed * (z ? 1 : 0))*(negative?-1:1);
                totalRotation += rotation;
                wheel.transform.Rotate(rotation.x, rotation.y, rotation.z);
            }
        }

        private void OnTriggerStay(Collider coll)
        {
            if (coll.tag == "Player")
            {
                if (hamsterRequired && coll.gameObject.GetComponent<Player>().Onesie.type == OnesieType.Hamster)
                    inTrigger = true;
                else if (!hamsterRequired)
                    inTrigger = true;
                else
                    inTrigger = false;
            }
        }

        private void OnTriggerExit(Collider coll)
        {
            if (coll.tag == "Player")
                inTrigger = false;
        }

        public bool GetPlayerRunning() { return inTrigger; }
        public bool InTrigger() { return inTrigger; }

        public void Stop()
        {
            this.stopped = true;
        }
    }
}