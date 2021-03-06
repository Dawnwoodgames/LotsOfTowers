﻿using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{
    public class AssociatedWheelObject : MonoBehaviour
    {
        public GameObject[] associatedObjects;
        public GameObject targetPosition;

        private HamsterCypherWheel wheel;
        //private bool isSpinning = false;

        void Start()
        {
            wheel = GetComponent<HamsterCypherWheel>();
        }

        void Update()
        {
            if (!targetPosition.GetComponent<Nimbi.Interaction.Triggers.DestroyInteraction>().GetObjectHit())
                if (wheel.GetRotateSpeed() >= 18)
                    RotateObject();
        }

        private void RotateObject()
        {
            //isSpinning = true;
            foreach (GameObject assObject in associatedObjects)
                assObject.transform.Rotate(0, 0, 50 * Time.deltaTime);
            StartCoroutine(DelaySpin());
        }

        private IEnumerator DelaySpin()
        {
            yield return new WaitForSeconds(.3f);
            //isSpinning = false;
        }
    }
}