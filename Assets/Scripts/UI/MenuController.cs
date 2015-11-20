using UnityEngine;
using UnityEngine.UI;

namespace LotsOfTowers.UI {
	public sealed class MenuController : MonoBehaviour {
		private Text[] labels;

		public void Awake() {
			this.labels = GetComponentsInChildren<Text>();
		}

		public void Start() {
			foreach (Text label in labels) {
				label.text = label.text.Localize();
			}
		}
	}
}