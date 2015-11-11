using LotsOfTowers.Actors;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LotsOfTowers.Framework {
	public sealed class TagManager : MonoBehaviour {
		private List<TagBundle> bundles;
		private Player player;

		private bool playerCanMoveObjects;

		public void Awake() {
			this.bundles = new List<TagBundle>();
			this.player = FindObjectOfType<Player>();
		}

		public void Add(TagBundle bundle) {
			GameObject target = bundle.gameObject;

			if (bundle.Movable) { // Object can be pushed around, so it requires a Collider & RigidBody
				if (target.GetComponent<Collider>() == null) {
					target.AddComponent<BoxCollider>();
				}

				if (target.GetComponent<Rigidbody>() == null) {
					Rigidbody r = target.AddComponent<Rigidbody>();

					r.constraints = RigidbodyConstraints.FreezeRotation;
					r.isKinematic = (player != null) ? !player.CanMoveObjects : true;
				}
			}

			bundles.Add(bundle);
			target.tag = "Managed";
		}

		public void Remove(TagBundle bundle) {
			bundle.gameObject.tag = "Untagged";
			bundles.Remove(bundle);
		}

		public void Update() {
			if (player == null) {
				// Player isn't registered, try to find it
				player = FindObjectOfType<Player> ();
			} else {
				if (playerCanMoveObjects != player.CanMoveObjects) {
					bundles.Where(b => b.Movable).ToList().ForEach(
						b => b.gameObject.GetComponent<Rigidbody>().isKinematic = !player.CanMoveObjects
					);
					playerCanMoveObjects = player.CanMoveObjects;
				}
			}
		}
	}
}