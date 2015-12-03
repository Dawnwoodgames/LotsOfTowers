using UnityEngine;
using System.Collections;
using System;

namespace LotsOfTowers.Interaction
{
    public class AssociatedWheelCipher : MonoBehaviour
    {
        public GameObject[] associatedCiphers;
        public int rotateAmount;

        private HamsterWheel wheel;
        private bool isSpinning = false;

        void Start()
        {
            wheel = GetComponent<HamsterWheel>();
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
                foreach (GameObject cWheel in associatedCiphers)
                    cWheel.transform.Rotate(Vector3.left * 90 * rotateAmount);
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