using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{
    public class MemoryReset : MonoBehaviour
    {
        public MemoryManager manager;
        private bool reset = false;

        void OnTriggerEnter(Collider coll)
        {
            if (!reset)
                manager.Reset();

            reset = !reset;
        }
    }
}