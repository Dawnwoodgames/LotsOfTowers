using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction.Triggers
{
    public class MaskRotationTrigger : MonoBehaviour
    {
        bool restoreRotation = false;
        public float rotationSpeed = 10f;
        public float rotateBackSpeed = 10f;

        private bool isCheating;
        private Quaternion startRotation;
        private int rotationCount;


        void Start()
        {
            startRotation = transform.localRotation;
        }

        public void Update()
        {

            if(rotationSpeed > 0)
            {
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
    }



}