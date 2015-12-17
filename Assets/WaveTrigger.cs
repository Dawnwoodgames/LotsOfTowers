using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction.Triggers
{
    public class WaveTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
            {
                coll.GetComponent<Rigidbody>().AddForce(transform.forward * 20f, ForceMode.Impulse);
            }
        }
    }
}