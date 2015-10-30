using UnityEngine;
using LotsOfTowers.Actors;

namespace LotsOfTowers.Triggers {
	public sealed class OnesiePickup : MonoBehaviour {
		public Onesie Onesie;

		public void OnCollisionEnter(Collision collision) {
			Actor actor = collision.gameObject.GetComponent<Actor>();

			if (actor != null) {
				actor.AddOnesie(Onesie);
				Destroy(gameObject);
			}
		}
	}
}