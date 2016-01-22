using UnityEngine;
using System.Collections;
using Nimbi.Interaction.Triggers;

namespace Nimbi.Interaction
{
    [RequireComponent(typeof(RotateTrigger))]
    public class HamsterWheelRotateObject : MonoBehaviour
    {
        public GameObject item;
        public float rotateSpeed;
        private RotateTrigger wheel;

        void Start()
        {
            wheel = GetComponent<RotateTrigger>();
            rotateSpeed = (wheel.negative) ? -rotateSpeed : rotateSpeed;
        }

        void Update()
        {
            if (wheel.GetPlayerRunning())
                item.transform.Rotate(0, rotateSpeed, 0);
        }
    }
}