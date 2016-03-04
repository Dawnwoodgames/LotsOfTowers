using Nimbi.Actors;
using UnityEngine;

namespace Nimbi.Interaction {
	public class ChargeTrigger : MonoBehaviour {
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
			}
		}

		public void OnTriggerEnter(Collider coll) {
			connected = coll.gameObject.GetComponent<Player>() != null;
			player = coll.gameObject.GetComponent<Player>();

			if (player != null) {
				x = player.transform.position.x;
				y = player.transform.position.y;
				z = player.transform.position.z;
			}
		}

		public void OnTriggerExit(Collider coll) {
			if (coll.gameObject.GetComponent<Player>() == player) {
				connected = false;
			}
		}
	}
}