using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
    public class DestroyInteraction : MonoBehaviour
    {
        public GameObject targetObject;

        private bool objectHit = false;

        private void OnTriggerEnter(Collider coll)
        {
            if (coll.transform.parent.gameObject == targetObject)
                objectHit = true;
        }

        private void OnTriggerExit() { objectHit = false; }

        public bool GetObjectHit() { return objectHit; }
    }
}