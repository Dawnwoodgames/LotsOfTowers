using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LotsOfTowers.UI {
	public sealed class LevelSelectController : MonoBehaviour {
		private CameraController camera;
		private GameObject currentSelectedInput;
		private GameObject[] chapters;
		private EventSystem eventSystem;
		private MenuController menu;
		private GameObject[] mounts;

		public void Awake() {
			this.camera = Camera.main.gameObject.GetComponent<CameraController>();
			this.chapters = GetComponentsInChildren<Canvas>().Select(c => c.gameObject).ToArray();
			this.eventSystem = FindObjectOfType<EventSystem>();
			this.menu = FindObjectOfType<MenuController>();
			this.mounts = chapters.Select(c => c.transform.FindChild("Mounting Point").gameObject).ToArray();
		}

		public void Update() {
			if (mounts.Contains(camera.mount.gameObject)) {
				// Camera is looking at level select
				if (currentSelectedInput.transform.parent != eventSystem.currentSelectedGameObject.transform.parent) {
					menu.SetActiveMenu(eventSystem.currentSelectedGameObject.transform.parent.gameObject);
				}
				((RectTransform)camera.mount).anchoredPosition = eventSystem.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition;
			}

			currentSelectedInput = eventSystem.currentSelectedGameObject;
		}
	}
}