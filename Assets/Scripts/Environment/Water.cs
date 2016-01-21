using Nimbi.Actors;
using UnityEngine;

namespace Nimbi.Environment {
	public class Water : MonoBehaviour {

        private Player player;
        private Rigidbody playerRigidbody;
        private RigidbodyConstraints swimConstraints;
        private bool playerIsElephant, playerSwimming;
        private float playerPositionY;
        public bool lowWater = false;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            playerRigidbody = player.GetComponent<Rigidbody>();

            swimConstraints = RigidbodyConstraints.FreezeRotation;
        }

		public void Update()
        {
            if (playerSwimming)
            {
                if (!lowWater)
                {
                    swimConstraints = (playerIsElephant) ? RigidbodyConstraints.FreezeRotation : RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
                    if (!playerIsElephant)
                        player.transform.position = new Vector3(player.transform.position.x, playerPositionY - (GetComponent<Collider>().bounds.extents.y / 2) + .3f, player.transform.position.z);
                }
                else
                    swimConstraints = RigidbodyConstraints.FreezeRotation;
            }
        }

        private void OnTriggerEnter(Collider coll)
        {
			if (coll.tag == "Player")
            {
                playerSwimming = true;
                playerPositionY = player.transform.position.y;
            }
        }

        private void OnTriggerStay(Collider coll)
        {
            playerIsElephant = (player.Onesie.type == OnesieType.Elephant) ? true : false;
            playerRigidbody.constraints = swimConstraints;
        }

		private void OnTriggerExit(Collider coll)
        {
			if (coll.tag == "Player")
            {
                playerSwimming = false;
            }
		}
	}
}