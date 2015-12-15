using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction
{
    public class AssociatedWheelWater : MonoBehaviour
    {
        public HamsterWheel wheel;
        public GameObject water;
        public GameObject nut;
        public ParticleSystem floatingWaterSystem;

        private bool waterFloats = false;
        private bool nutIsForced = false;

        // Update is called once per frame
        void Update()
        {
            if (wheel.GetRotateSpeed() >= 15)
            {
                if (!waterFloats)
                    floatingWaterSystem.Play();

                // rise water if limit is not reached
                if (water.transform.localPosition.y < 1.05f)
                    water.transform.Translate(Vector3.up * 0.07f * Time.deltaTime);
                else if (!nutIsForced)
                {
                    nut.GetComponent<Rigidbody>().AddForce(Vector3.up * 20f * Time.deltaTime, ForceMode.Impulse);
                    nutIsForced = true;
                }

                waterFloats = true;
            }
            else if (wheel.GetRotateSpeed() < 15)
            {
                waterFloats = false;
                floatingWaterSystem.Stop();
            }
        }
    }
}