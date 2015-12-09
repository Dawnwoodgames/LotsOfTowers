using LotsOfTowers.Actors;
using UnityEngine;

namespace LotsOfTowers.Interaction {
	public sealed class ChargeTrigger : MonoBehaviour {
		private bool connected;
		private Player player;
		private float x, y, z;

		public float ChargeRate = 20; // How much charge the player will get per second

		public bool HasPlayerMoved {
			get {
				return player != null && (x != player.transform.position.x ||
                      y != player.transform.position.y || z != player.transform.position.z);
			}
		}
		
		public void FixedUpdate() {
			if (connected && HasPlayerMoved) {
				player.StaticCharge += ChargeRate * Time.smoothDeltaTime;
				x = player.transform.position.x;
				y = player.transform.position.y;
				z = player.transform.position.z;

                player.GetComponent<Player>().PlayParticles();
			}
		}

		public void OnCollisionEnter(Collision collision) {
			connected = collision.gameObject.GetComponent<Player>() != null;
			player = collision.gameObject.GetComponent<Player>();

			if (player != null) {
				x = player.transform.position.x;
				y = player.transform.position.y;
				z = player.transform.position.z;
			}
		}

		public void OnCollisionExit(Collision collision) {
			if (collision.gameObject.GetComponent<Player>() == player) {
				connected = false;
			}
		}
	}
}