using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction.Triggers
{
    public class MaskRotationTrigger : MonoBehaviour
    {

        public float rotationSpeed = 10f;
        public float rotateBackSpeed = 10f;
        public float pushBackRate = 30;

        private GameObject player;
        public bool isCheating;
        private Quaternion startRotation;
        private int rotationCount;

        public bool isSpinning;
        public bool isScary;
        private bool inTrigger;


        void Start()
        {
            GameObject.FindGameObjectWithTag("Player");
            startRotation = transform.localRotation;
        }

        public void Update()
        {

            if (rotationSpeed > 0)
            {
                isSpinning = true;
                transform.Rotate(rotationSpeed, 0, 0);
            }

            rotationSpeed -= Time.deltaTime / 1f;


            if (rotationSpeed <= 0)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, startRotation, 0.1f);
                if (Mathf.Abs(transform.localRotation.eulerAngles.x - startRotation.eulerAngles.x) < 0.1f)
                    rotationSpeed = 10;
            }

        }

        public void PushNimbiAway()
        {
            player.GetComponent<Rigidbody>().AddForce(Vector3.right * pushBackRate, ForceMode.VelocityChange);
        }
    }
}