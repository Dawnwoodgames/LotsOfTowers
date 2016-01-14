using UnityEngine;
using System.Collections;
using Nimbi.Interaction.Triggers;

namespace Nimbi.Interaction.Triggers
{
    public class StopjeScript : MonoBehaviour
    {
        public GameObject stopje;
        public float heightIncrease;
        private bool active;
        private Vector3 startPosition;
        private HamsterWheelTrigger trigger;
        public RopePickup rope;

        void Start()
        {
            startPosition = stopje.transform.position;
            trigger = GetComponent<HamsterWheelTrigger>();
        }

        void Update()
        {
            if(!active && (trigger.totalRotation.x%360 >= 180 && trigger.totalRotation.x % 360 <= 230) && rope.connected)
            {
                trigger.Stop();
                active = true;
            }
            else if (active && heightIncrease + startPosition.y > stopje.transform.position.y)
            {
                stopje.transform.position += Vector3.up * Time.smoothDeltaTime * 2;
            }
        }
    }
}