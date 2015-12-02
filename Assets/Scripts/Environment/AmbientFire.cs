using UnityEngine;

namespace LotsOfTowers.Environment {
	[RequireComponent(typeof(Light))]
	public sealed class AmbientFire : MonoBehaviour {
		private Light light;

		public float maxIntensity = 1.5f;
		public float minIntensity = 0.1f;

		public void Awake() {
			this.light = GetComponent<Light>();
		}

		public void Update() {
			light.intensity = Random.Range(minIntensity, maxIntensity);
		}
	}
}