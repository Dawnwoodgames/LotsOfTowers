using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Nimbi.UI {
    [RequireComponent(typeof(Canvas))]
    public class PauseMenu : MonoBehaviour {
        private Canvas canvas;
        private EventSystem eventSystem;
        private bool wasCancelPressed;
		private bool wasDisabled;

        public void Awake() {
            GameManager.Instance.CursorEnabled = false;
            this.canvas = GetComponent<Canvas>();
            this.eventSystem = FindObjectOfType<EventSystem>();
            this.wasCancelPressed = false;
        }

        public void Disable() {
            canvas.enabled = false;
            eventSystem.SetSelectedGameObject(null);
            Time.timeScale = 1;
			wasDisabled = true;
        }

        public void Enable() {
            canvas.enabled = true;
            eventSystem.SetSelectedGameObject(GetComponentInChildren<Button>().gameObject);
            GameManager.Instance.HideFader();
            Time.timeScale = 0;
        }

        public void Update() {
            if (wasCancelPressed && !Input.GetButton("Cancel") && !GameManager.Instance.LoadingScreenVisible) {
                // Key up
                if (canvas.enabled) {
                    Disable();
                    GameManager.Instance.CursorEnabled = false;
                } else {
                    Enable();
                    GameManager.Instance.CursorEnabled = true;
                }
                wasCancelPressed = false;
            } else if (Input.GetButton("Cancel")) {
                // Key down
                wasCancelPressed = true;
            }
			if (wasDisabled) {
				GameManager.Instance.CursorEnabled = false;
				wasDisabled = false;
			}
        }
    }
}