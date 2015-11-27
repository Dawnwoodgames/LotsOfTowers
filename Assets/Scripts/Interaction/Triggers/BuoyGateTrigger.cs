using UnityEngine;

namespace LotsOfTowers.Interaction.Trigger {
	public sealed class BuoyGateTrigger : MonoBehaviour {

		public void Update() {
			Debug.DrawRay (transform.position, Vector3.left);
			Debug.DrawRay (transform.position, Vector3.right);
		}
	}
}