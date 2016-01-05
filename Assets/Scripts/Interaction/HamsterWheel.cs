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

        private bool isPlayerRunning;

        void Start()
        {
            rotateTriggerScript = rotateTrigger.GetComponent<WheelRotateTrigger>();
        }

        void FixedUpdate()
        {
            isPlayerRunning = rotateTriggerScript.GetPlayerRunning();

            if (isPlayerRunning)
                if (rotateSpeed < 15)
                    rotateSpeed += speedGain;

            if (!isPlayerRunning)
                if (rotateSpeed > 0)
                    rotateSpeed -= speedLoss * Time.deltaTime;

            if (rotateSpeed > 1)
                gameObject.transform.Rotate(Vector3.forward * rotateSpeed);
        }

        public float GetRotateSpeed() { return rotateSpeed; }
        public bool GetIsPlayerRunning() { return isPlayerRunning; }
    }
}