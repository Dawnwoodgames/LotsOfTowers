using UnityEngine;
using Nimbi.Actors;

namespace Nimbi.Interaction
{
	public class OnesiePickup : MonoBehaviour
	{
		// Private variables
		private Player actor;
		private float y;

        // Public variables
        public Onesie Onesie;

		public void Awake() {
			this.y = transform.position.y;
		}

		public void OnTriggerEnter(Collider collider) {
			actor = collider.gameObject.GetComponent<Player>();

			if (actor != null && Onesie != null)
			{
                actor.AddOnesie(Onesie);
                Destroy(gameObject);
			}
		}

		public void Update() {
			transform.position = new Vector3(
				transform.position.x,
				y + Mathf.Sin(Time.timeSinceLevelLoad) / 2,
				transform.position.z
			);
		}
	}
}
