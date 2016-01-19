using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{
    public class RotatingMirror : MonoBehaviour
    {

        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision coll)
        {
            if (coll.gameObject.tag == "Player")
                rb.isKinematic = false;
        }

        private void OnCollisionExit(Collision coll)
        {
            if (coll.gameObject.tag == "Player")
                rb.isKinematic = true;
        }
    }
}