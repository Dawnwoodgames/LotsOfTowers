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
            startRotation = transform.rotation;
        }

        public void Update()
        {

            if(rotationSpeed > 0)
            {
                transform.Rotate(rotationSpeed, 0, 0);
            }

            rotationSpeed -= Time.deltaTime / 2f;


            if (rotationSpeed <= 0)
            {
                
            }

        }
    }



}


