using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction
{
    public class MaskHappyTrigger : MonoBehaviour
    {
        public bool isHappy;

        private bool inTrigger;

        void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
            {
                Debug.Log("Happy is hitted");
                inTrigger = true;
                isHappy = true;
            }
        }
     
    }

}
