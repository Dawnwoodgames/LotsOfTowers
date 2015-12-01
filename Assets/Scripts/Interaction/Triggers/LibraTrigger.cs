using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction.Triggers
{
    public class LibraTrigger : MonoBehaviour
    {
        public GameObject libra;
        public GameObject elephant;
        public bool playerOnLibra = false;
        public bool elephantReadyToLaunch = false;

        private void Start()
        {
            
        }

        private void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
            {
                playerOnLibra = true;
                coll.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                libra.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }
    }
}