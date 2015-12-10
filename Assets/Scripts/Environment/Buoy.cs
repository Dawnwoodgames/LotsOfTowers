using UnityEngine;

namespace LotsOfTowers.Environment {
	public sealed class Buoy : MonoBehaviour {
		private float internalClock;
		private new Light light;
		private float originalY;
		private float randomness;

		public bool Red {
			get { return light.color.r > 0; }
		}

		public void Awake() {
			this.internalClock = 0;
			this.light = GetComponentInChildren<Light>();
			light.intensity = 1;
			this.originalY = transform.localPosition.y;
			this.randomness = Random.Range(0f, 1f);
		}

		public void Update() {
			internalClock += Time.deltaTime;
			light.intensity = 0.75f + Mathf.Sin(2 * internalClock) * 0.75f;
			transform.localPosition = new Vector3(transform.localPosition.x, originalY + Mathf.Sin(randomness + internalClock) * 0.25f, transform.localPosition.z);
		}
	}
}