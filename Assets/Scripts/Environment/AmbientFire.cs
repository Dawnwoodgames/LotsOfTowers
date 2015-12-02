using UnityEngine;

namespace LotsOfTowers.Environment {
	[RequireComponent(typeof(Light))]
	public sealed class AmbientFire : MonoBehaviour {
		private Light light;

		public float maxIntensity = 0.9f;
		public float minIntensity = 0.7f;

		public void Awake() {
			this.light = GetComponentInChildren<Light>();
			if (light == null) {
				throw new MissingComponentException("Light Component required for AmbientFire");
			}
		}

		public void Update() {
			light.intensity = Random.Range(minIntensity, maxIntensity);
		}
	}
}