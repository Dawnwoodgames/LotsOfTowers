using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Nimbi.UI {
	public class LevelSelectController : MonoBehaviour {
		private new CameraController camera;
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
            
            // Enable Menu, Intro & Tower 1
            PlayerPrefs.SetInt("bIsLevelAvailable0", 1);
            PlayerPrefs.SetInt("bIsLevelAvailable1", 1);
            PlayerPrefs.SetInt("bIsLevelAvailable2", 1);
            if (Debug.isDebugBuild) { // Enable all towers when in editor
                PlayerPrefs.SetInt("bIsLevelAvailable3", 1);
                PlayerPrefs.SetInt("bIsLevelAvailable4", 1);
                PlayerPrefs.SetInt("bIsLevelAvailable5", 1);
            }

            int levelIndex = 1;

			foreach (Button button in GetComponentsInChildren<Button>()) {
				// Assign functions to each button in the menu
				if (PlayerPrefs.GetInt("bIsLevelAvailable" + levelIndex, 0) > 0) {
					button.interactable = true;
				} else {
					button.interactable = false;
				}

				levelIndex++;
			}
		}

		public void Update() {
			if (mounts.Contains(camera.mount.gameObject)) {
				// Camera is looking at level select
				if (eventSystem.currentSelectedGameObject == null) {
					return;
				}

				if (currentSelectedInput.transform.parent != eventSystem.currentSelectedGameObject.transform.parent) {
					menu.SetActiveMenu(eventSystem.currentSelectedGameObject.transform.parent.gameObject);
				}
				((RectTransform)camera.mount).anchoredPosition = eventSystem.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition;
			}

			currentSelectedInput = eventSystem.currentSelectedGameObject;
		}
	}
}