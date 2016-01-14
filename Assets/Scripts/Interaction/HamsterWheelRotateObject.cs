using UnityEngine;
using System.Collections;
using Nimbi.Interaction.Triggers;

namespace Nimbi.Interaction
{
    [RequireComponent(typeof(HamsterWheelTrigger))]
    public class HamsterWheelRotateObject : MonoBehaviour
    {

        public GameObject item;
        private HamsterWheelTrigger wheel;

        void Start()
        {
            wheel = GetComponent<HamsterWheelTrigger>();
        }

        void Update()
        {
            if (wheel.GetPlayerRunning())
                item.transform.Rotate(0, 1f, 0);
        }
    }
}