using UnityEngine;
using UnityEngine.UI;

namespace Nimbi.UI {
	[RequireComponent(typeof(Slider))]
	public class AudioSlider : MonoBehaviour {
		private Slider slider;

		public void Awake() {
			this.slider = GetComponent<Slider>();
		}

		public void Start() {
			slider.value = AudioListener.volume * 100;
			slider.wholeNumbers = true;
		}

		public void Update() {
			AudioListener.volume = slider.value / 100;
            PlayerPrefs.SetFloat("AudioVolume", AudioListener.volume);
			slider.value = AudioListener.volume * 100;
		}
	}
}