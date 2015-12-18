using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction.Triggers
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