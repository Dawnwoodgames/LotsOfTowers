using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
    public class ExitWater : MonoBehaviour
    {
        public GameObject water;

        private void OnTriggerStay(Collider coll)
        {
            if (coll.tag == "Player")
            {
                coll.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                water.GetComponent<Environment.Water>().lowWater = true;
            }
        }

        private void OnTriggerExit(Collider coll)
        {
            if (coll.tag == "Player")
                water.GetComponent<Environment.Water>().lowWater = false;
        }
    }
}