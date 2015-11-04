using UnityEngine;
using LotsOfTowers.Actors;

namespace LotsOfTowers.Triggers
{
	public class OnesiePickup : MonoBehaviour
	{
		// Private variables
		private Actor actor;
		private float y;

		// Public variables
		public Onesie Onesie;

		public void Awake() {
			this.y = transform.position.y;
		}

		public void OnTriggerEnter(Collider collider) {
			actor = collider.gameObject.GetComponent<Actor>();

			if (actor.HasFreeSlots && actor.AddOnesieToFirstFreeSlot(Onesie))
			{
				Destroy(gameObject);
			}
		}
		
		public void OnTriggerStay(Collider collider) {
			if (actor != null) {
				if (Input.GetAxis ("Onesie 1") > 0 || Input.GetAxis ("Onesie 2") > 0 || Input.GetAxis ("Onesie 3") > 0) {
					Onesie = actor.AddOnesie(Input.GetAxis("Onesie 1") > 0 ? 0 :
                         (Input.GetAxis("Onesie 2") > 0 ? 1 : (Input.GetAxis("Onesie 3") > 0 ? 2 : -1)), Onesie);

					if (Onesie == null) {
						Destroy(gameObject);
					}
				}
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