using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{
    public class Wipwap : MonoBehaviour
    {

        private bool inTrigger;

        void Start()
        {

        }

        void Update()
        {

        }

        void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
                inTrigger = true;
        }

        void OnTriggerExit(Collider coll)
        {
            if (coll.tag == "Player")
                inTrigger = false;
        }
    }
}