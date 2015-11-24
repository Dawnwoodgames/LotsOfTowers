using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LotsOfTowers.UI {
	public sealed class MenuController : MonoBehaviour {
		private CameraController camera;
		private EventSystem eventSystem;
		private Text[] labels;
		private GameObject[] menus;
		
		public ColorBlock colors; // Colors used for all UI elements in this controller
		public Font font; // Font used for all text elements in this controller

		public void Awake() {
			this.camera = FindObjectOfType<CameraController>();
			this.eventSystem = FindObjectOfType<EventSystem>();
			this.font = (font == null) ? Resources.GetBuiltinResource<Font>("Arial.ttf") : font;
			this.labels = GetComponentsInChildren<Text>();
			this.menus = GetComponentsInChildren<Canvas>().Select(c => c.gameObject).ToArray();

			foreach (Text label in labels) {
				label.font = font;
				label.text = label.text.Localize();
			}
		}

		public void SetActiveMenu(GameObject menu) {
			if (menus.Contains(menu)) {
				camera.mount = GameObject.Find(name + "/" + menu.name + "/Mounting Point").transform;
			}
		}

		public void Start() {
			SetActiveMenu(menus.FirstOrDefault());

			Button b = GetComponentInChildren<Button> ();
			Debug.Log ("normal: " + b.colors.normalColor);
			Debug.Log ("highlight: " + b.colors.highlightedColor);
			Debug.Log ("pressed: " + b.colors.pressedColor);
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