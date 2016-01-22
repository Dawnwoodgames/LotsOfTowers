using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
    public class FireLightTrigger : MonoBehaviour
    {
        public Light dragonLight;

        void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Fire")
            {
                dragonLight.enabled = true;
            }
        }
    }
}
