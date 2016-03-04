using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
    public class NutForce : MonoBehaviour
    {

        public GameObject nut;
        private bool inTrigger;

        void Update()
        {
            if (inTrigger)
            {
                nut.GetComponent<Rigidbody>().useGravity = true;
                nut.GetComponent<Rigidbody>().isKinematic = false;
                nut.GetComponent<Rigidbody>().AddForce(new Vector3(-1, 0, -.5f) * 2f, ForceMode.Impulse);
            }
        }

        void OnTriggerEnter(Collider coll)
        {
            if (coll.name == "Nut")
                inTrigger = true;
        }

        void OnTriggerExit(Collider coll)
        {
            if (coll.name == "Nut")
                inTrigger = false;
        }
    }
}