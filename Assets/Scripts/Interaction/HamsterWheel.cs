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
            
            gameObject.transform.Rotate(Vector3.up * rotateSpeed);
        }

        public float GetRotateSpeed() { return rotateSpeed; }
    }
}