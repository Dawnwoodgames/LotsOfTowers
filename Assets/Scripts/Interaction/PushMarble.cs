using UnityEngine;
using Nimbi.Actors;

namespace Nimbi.Interaction {
    public class PushMarble : MonoBehaviour {

        private Player player;
        private Rigidbody rb;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionStay(Collision coll)
        {
            if (player.Onesie.type == OnesieType.Hamster)
                rb.isKinematic = false;
            else
                rb.isKinematic = true;
        }
    }
}