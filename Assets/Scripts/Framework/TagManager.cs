using System.Collections.Generic;
using UnityEngine;

namespace LotsOfTowers.Framework {
	public class TagManager : MonoBehaviour {
		private List<TagBundle> bundles;

		public void Awake() {
			this.bundles = new List<TagBundle>();
		}

		public void Add(TagBundle bundle) {
			GameObject target = bundle.gameObject;

			if (bundle.Movable) {
				// Object can be pushed around, so it requires a Collider & RigidBody
				if (target.GetComponent<Collider>() == null) {
					target.AddComponent<BoxCollider>();
				}

				if (target.GetComponent<Rigidbody>() == null) {
					Rigidbody r = target.AddComponent<Rigidbody>();

					r.constraints = RigidbodyConstraints.FreezeRotation;
					r.isKinematic = true; // TODO: should be set according to game state, not to a static default
				}
			}

			bundles.Add(bundle);
			target.tag = "Managed";
		}

		public void Update() {
		}

		public void Remove(TagBundle bundle) {
			bundle.gameObject.tag = "Untagged";
			bundles.Remove(bundle);
		}
	}
}