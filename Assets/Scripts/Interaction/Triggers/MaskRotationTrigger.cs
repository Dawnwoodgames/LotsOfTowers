using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction.Triggers
{
    public class MaskRotationTrigger : MonoBehaviour
    {
        
        public float rotationSpeed = 10;
        public float acceleration = 0.02f;

        private bool isCheating = true;
        private Quaternion startRotation = Quaternion.Euler(0,0,0);
        private int rotationCount;
        

        void Start()
        {
            startRotation = transform.rotation;
        }

        public void Update()
        {
            transform.Rotate(rotationSpeed, 0, 0);
            if(transform.rotation.eulerAngles.x >= 180)
            {
                rotationCount++;
                Debug.Log(rotationCount);
            }

            if(rotationCount >= 300)
            {
                rotationSpeed -= acceleration;
                if (isCheating)
                {
                    
                }
                
            }
        }



    }

  }

