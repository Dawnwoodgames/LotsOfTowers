using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction.Triggers
{
    public class MaskRotationTrigger : MonoBehaviour
    {
        float targetRotation = 360f; //Rotation to stop at
        float distanceToSlow = 50f; //How far from target to start slowing down
        float minDistance = 1f; //How far from target to stop completely

        private float curAngle = 0f;
        private float startAngle = 0f;
        private float startTime = 0f;

        public void Update()
        {
            float t = (Time.time - startTime);
            curAngle = Mathf.Lerp(startAngle, targetRotation, t);
        }



    }

  }

