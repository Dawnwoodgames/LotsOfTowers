using UnityEngine;
using System.Collections;
using Nimbi.Interaction.Triggers;

namespace Nimbi.Interaction
{
    [RequireComponent(typeof(HamsterWheelTrigger))]
    public class HamsterWheelRotateObject : MonoBehaviour
    {
        public GameObject item;
        public float rotateSpeed;
        private HamsterWheelTrigger wheel;

        void Start()
        {
            wheel = GetComponent<HamsterWheelTrigger>();
            rotateSpeed = (wheel.GetNegativeRotate()) ? -rotateSpeed : rotateSpeed;
        }

        void Update()
        {
            if (wheel.GetPlayerRunning())
                item.transform.Rotate(0, rotateSpeed, 0);
        }
    }
}