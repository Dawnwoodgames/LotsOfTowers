using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction
{
    public class AssociatedWheelObject : MonoBehaviour
    {
        public GameObject floor;
        public GameObject[] cypherWheel;
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
                if (this.gameObject.tag == "Fracture")
                {
                    isSpinning = true;
                    floor.transform.Rotate(Vector3.forward * 15 * Time.deltaTime);
                } else {
                    isSpinning = true;
                    foreach (GameObject cWheel in cypherWheel)
                        cWheel.transform.Rotate(Vector3.left * 90 * rotateAmount);
                    StartCoroutine(DelaySpin());
                }
            }
        }

        private IEnumerator DelaySpin()
        {
            yield return new WaitForSeconds(1);
            isSpinning = false;
        }
    }
}