using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction
{
    public class MaskHappyTrigger : MonoBehaviour
    {
        public bool isHappy;

        void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
            {
                isHappy = true;
            }
        }
     
    }

}
