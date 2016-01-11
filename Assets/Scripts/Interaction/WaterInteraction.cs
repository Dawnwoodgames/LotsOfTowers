using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{
    public class WaterInteraction : MonoBehaviour
    {

        void Start()
        {

        }

        private void OnTriggerStay(Collider coll)
        {
            if (!coll.GetComponent<Nimbi.Actors.Player>().Onesie.isHeavy)
                coll.GetComponent<Rigidbody>().AddForce(Vector3.up, ForceMode.Impulse);
        }
    }
}