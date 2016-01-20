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
        private RotateTrigger trigger;
        public RopePickup rope;
        public GameObject waterval;
        public GameObject door;

        void Start()
        {
            startPosition = stopje.transform.position;
            trigger = GetComponent<RotateTrigger>();
        }

        void Update()
        {
            if(!active && (trigger.totalRotation.x%360 >= 180 && trigger.totalRotation.x % 360 <= 230) && rope.connected)
            {
                trigger.Stop();
                active = true;
                waterval.SetActive(true);
                door.GetComponent<HingedDoor>().Open();
            }
            else if (active && heightIncrease + startPosition.y > stopje.transform.position.y)
            {
                stopje.transform.position += Vector3.up * Time.smoothDeltaTime * 2;
            }
        }
    }
}