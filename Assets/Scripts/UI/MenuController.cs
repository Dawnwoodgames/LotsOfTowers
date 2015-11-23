using UnityEngine;
using UnityEngine.UI;
using SmartLocalization;

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

		// Event handles used by the menu
		public void ChangeLanguage(string language) {
			GameManager.Instance.Language = language;
			GameManager.Instance.LoadLevel(Application.loadedLevel);
		}

		public void QuitApplication() {
			GameManager.Instance.Quit();
		}
	}
}