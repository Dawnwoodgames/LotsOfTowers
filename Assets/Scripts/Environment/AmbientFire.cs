using UnityEngine;

namespace LotsOfTowers.Environment {
	public sealed class AmbientFire : MonoBehaviour {
		private new Light light;

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