using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LotsOfTowers.Interaction.Triggers {
	[RequireComponent(typeof(Rigidbody))]
	public sealed class WeightTrigger : TriggerBehaviour {
		private List<Rigidbody> objects;
		private Rigidbody rigidBody;
		
		public float FallDelay; // How long it will take before this object will fall after breaching it's threshold
		public float MassThreshold; // Max mass this object can carry without falling

		public float CarryWeight { get { return objects.Select(g => g.mass).Sum(); } }

		public void Awake() {
			this.objects = new List<Rigidbody>();
			this.rigidBody = GetComponent<Rigidbody>();
			this.rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
			this.rigidBody.isKinematic = true;
		}

		public override IEnumerator TriggerOn(GameObject source) {
			Rigidbody body = source.GetComponent<Rigidbody>();
			float t = 0;

			if (body != null) {
				objects.Add(body);
			}

			if (CarryWeight >= MassThreshold) {
				while (t < FallDelay) {
					t += Time.smoothDeltaTime;
					yield return null;
				}

				rigidBody.isKinematic = false;
			}
		}

		public override IEnumerator TriggerOff(GameObject source) {
			Rigidbody body = source.GetComponent<Rigidbody>();
			
			if (body != null) {
				objects.Remove(body);
			}

			if (CarryWeight < MassThreshold) {
				rigidBody.isKinematic = true;
			}

			yield return null;
		}
	}
}