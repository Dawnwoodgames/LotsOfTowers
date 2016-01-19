using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
    public class BreakableMirror : MonoBehaviour
    {
        private void OnTriggerStay(Collider coll)
        {
            if (coll.tag == "Player")
                if (Input.GetButtonDown("Submit"))
                    BreakObject();
        }

        private void BreakObject()
        {
            transform.gameObject.AddComponent<Rigidbody>();
        }
    }
}