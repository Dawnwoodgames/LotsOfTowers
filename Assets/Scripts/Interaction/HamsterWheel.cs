using UnityEngine;
using System.Collections;
using LotsOfTowers.Interaction.Triggers;

namespace LotsOfTowers.Interaction
{
    public class HamsterWheel : MonoBehaviour
    {
        public GameObject rotateTrigger;

        private WheelRotateTrigger rotateTriggerScript;
        private float rotateSpeed = 0f;
        private int speedGain = 1;
        private int speedLoss = 3;

        void Start()
        {
            rotateTriggerScript = rotateTrigger.GetComponent<WheelRotateTrigger>();
        }

        void Update()
        {
            if (rotateTriggerScript.GetPlayerRunning())
                if (rotateSpeed < 15)
                    rotateSpeed += speedGain;

            if (!rotateTriggerScript.GetPlayerRunning())
                if (rotateSpeed > 0)
                    rotateSpeed -= speedLoss * Time.deltaTime;

            if (rotateSpeed > 1)
                gameObject.transform.Rotate(Vector3.forward * rotateSpeed);
        }

        public float GetRotateSpeed() { return rotateSpeed; }
    }
}