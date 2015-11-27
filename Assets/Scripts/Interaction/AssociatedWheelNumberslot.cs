using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction
{
    public class AssociatedWheelNumberslot : MonoBehaviour
    {
        private HamsterWheel wheel;
        public int amountOfWheels;
        public GameObject[] numberSlots;

        // Use this for initialization
        void Start()
        {
            wheel = GetComponent<HamsterWheel>();
        }

        // Update is called once per frame
        void Update()
        {
            if (wheel.GetRotateSpeed() > 10)
            {
                if (amountOfWheels == 1)
                    numberSlots[0].transform.Rotate(Vector3.forward * 5 * Time.deltaTime);
                else if (amountOfWheels == 2)
                {
                    numberSlots[0].transform.Rotate(Vector3.forward * 5 * Time.deltaTime);
                    numberSlots[1].transform.Rotate(Vector3.forward * 5 * Time.deltaTime);
                }
            }
        }
    }
}