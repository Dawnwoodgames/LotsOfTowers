using UnityEngine;

namespace LotsOfTowers.Environment {
	public sealed class Buoy : MonoBehaviour {
		private float internalClock;
		private Light light;
		private float originalY;
		private float randomness;

		public void Awake() {
			this.internalClock = 0;
			this.light = GetComponentInChildren<Light>();
			this.originalY = transform.position.y;
			this.randomness = Random.Range(0f, 1f);
		}

		public void Update() {
			internalClock += Time.deltaTime;
			light.intensity = 0.75f + Mathf.Sin(2 * internalClock) * 0.75f;
			transform.position = new Vector3(transform.position.x, originalY + Mathf.Sin(randomness + internalClock) * 0.25f, transform.position.z);
		}
	}
}