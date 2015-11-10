using UnityEngine;
using LotsOfTowers.Actors;
using LotsOfTowers.ToolTip;

namespace LotsOfTowers.Triggers
{
	public class OnesiePickup : MonoBehaviour
	{
		// Private variables
		private Player actor;
		private float y;
        public GameObject tooltip;

        // Public variables
        public Onesie Onesie;

		public void Awake() {
			this.y = transform.position.y;
		}

		public void OnTriggerEnter(Collider collider) {
			actor = collider.gameObject.GetComponent<Player>();
            if (collider.tag == "Player")
                Tooltip.ShowTooltip(tooltip, "Onesie.Chicken", false, new string[] { "Jump" });

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
