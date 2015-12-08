using UnityEngine;
using System.Collections;
using LotsOfTowers.Actors;

namespace LotsOfTowers.Interaction.Triggers
{
    public class LibraTrigger : MonoBehaviour
    {
        public GameObject libra;
        public GameObject elephant;
        public bool playerOnLibra = false;
        public bool elephantReadyToLaunch = false;
        public bool playerReadyToLaunch = false;
        private GameObject player;
        private float regularMass;

        void Update()
        {
            if (player != null && player.GetComponent<Player>().Onesie.type == OnesieType.Elephant && player.GetComponent<Rigidbody>().mass != regularMass)
            {
                player.GetComponent<Rigidbody>().mass = elephant.GetComponent<Rigidbody>().mass;
                elephant.GetComponent<ElephantTrigger>().StartMoving();
            }
        }

        private void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
            {
                regularMass = coll.GetComponent<Rigidbody>().mass;
                player = coll.gameObject;
                playerOnLibra = true;
                coll.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                libra.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }
    }
}