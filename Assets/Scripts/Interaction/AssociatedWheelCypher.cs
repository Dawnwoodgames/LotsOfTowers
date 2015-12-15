using UnityEngine;
using System.Collections;
using System;

namespace LotsOfTowers.Interaction
{
    public class AssociatedWheelCypher : MonoBehaviour
    {
        public GameObject[] associatedCyphers;
        public int rotateAmount;

        private HamsterCypherWheel wheel;
        private bool isSpinning = false;

        void Start()
        {
            wheel = GetComponent<HamsterCypherWheel>();
        }

        void Update()
        {
            if (wheel.GetRotateSpeed() >= 18)
                RotateObject();
        }

        private void RotateObject()
        {
            if (!isSpinning)
            {
                isSpinning = true;
                foreach (GameObject cWheel in associatedCyphers)
                    cWheel.transform.Rotate(Vector3.left * -90 * rotateAmount);
                StartCoroutine(DelaySpin());
            }
        }

        private IEnumerator DelaySpin()
        {
            yield return new WaitForSeconds(1.5f);
            isSpinning = false;
        }
    }
}