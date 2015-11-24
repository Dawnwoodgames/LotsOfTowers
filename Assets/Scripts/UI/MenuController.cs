using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LotsOfTowers.UI {
	public sealed class MenuController : MonoBehaviour {
		private CameraController camera;
		private GameObject currentActiveMenu;
		private EventSystem eventSystem;
		private Text[] labels;
		private GameObject[] menus;

		public ColorBlock colors = ColorBlock.defaultColorBlock;
		public Font font = Resources.GetBuiltinResource<Font>("Arial.ttf");

		public void Awake() {
			this.camera = FindObjectOfType<CameraController>();
			this.eventSystem = FindObjectOfType<EventSystem>();
			this.labels = GetComponentsInChildren<Text>();
			this.menus = GetComponentsInChildren<Canvas>().Select(c => c.gameObject).ToArray();

			GetComponentsInChildren<Selectable>().ToList().ForEach(s => s.colors = colors);

			foreach (Text label in labels) {
				label.font = font;
				label.text = label.text.Localize();
			}
		}

		public void SetActiveMenu(GameObject menu) {
			if (menus.Contains(menu)) {
				camera.mount = GameObject.Find(name + "/" + menu.name + "/Mounting Point").transform;
				currentActiveMenu = menu;
				eventSystem.SetSelectedGameObject(menu.GetComponentsInChildren<Selectable>().FirstOrDefault().gameObject);
			}
		}

		public void Start() {
			SetActiveMenu(menus.FirstOrDefault());
		}

		public void Update() {
			if (eventSystem.currentSelectedGameObject == null && currentActiveMenu != null) {
				eventSystem.SetSelectedGameObject(currentActiveMenu.GetComponentsInChildren<Selectable>().FirstOrDefault().gameObject);
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