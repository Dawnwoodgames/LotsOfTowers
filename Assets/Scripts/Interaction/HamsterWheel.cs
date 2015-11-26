using UnityEngine;
using System.Collections;
using LotsOfTowers.Interaction.Triggers;

namespace LotsOfTowers.Interaction
{
    public class HamsterWheel : MonoBehaviour
    {

        public GameObject rotateTrigger;
        public GameObject waterBasket;
        public ParticleSystem floatingWaterSystem;
        private bool waterFloats = false;

        private WheelRotateTrigger rotateTriggerScript;
        private bool playerRunning = false;
        private float rotateSpeed = 0f;
        private int speedGain = 1;
        private int speedLoss = 5;

        // Use this for initialization
        void Start()
        {
            rotateTriggerScript = rotateTrigger.GetComponent<WheelRotateTrigger>();
        }

        // Update is called once per frame
        void Update()
        {
            if (rotateTriggerScript.GetPlayerRunning())
                if (rotateSpeed < 60)
                    rotateSpeed += speedGain;

            if (!rotateTriggerScript.GetPlayerRunning())
                if (rotateSpeed > 0)
                    rotateSpeed -= speedLoss * Time.deltaTime;

            if (rotateSpeed > 20)
            {
                if (!waterFloats)
                    floatingWaterSystem.Play();
                if (waterBasket.transform.localPosition.y < 1.05f)
                    waterBasket.transform.Translate(Vector3.up * 0.07f * Time.deltaTime);
                waterFloats = true;
            }
            else if (rotateSpeed < 20)
            {
                waterFloats = false;
                floatingWaterSystem.Stop();
            }
            gameObject.transform.Rotate(Vector3.up * rotateSpeed);
        }

        public float GetRotateSpeed() { return rotateSpeed; }
    }
}