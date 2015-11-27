using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction
{
    public class AssociatedWheelNumberslot : MonoBehaviour
    {
        private HamsterWheel wheel;
        public GameObject numberSlot;

        // Use this for initialization
        void Start()
        {
            wheel = gameObject.GetComponent<HamsterWheel>();
        }

        // Update is called once per frame
        void Update()
        {
            if (wheel.GetRotateSpeed() > 10)
            {
                numberSlot.transform.Rotate(Vector3.forward * 5 * Time.deltaTime);
            }
        }
    }
}