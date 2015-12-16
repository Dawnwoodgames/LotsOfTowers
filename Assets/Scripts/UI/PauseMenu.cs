using UnityEngine;

namespace LotsOfTowers.UI {
	[RequireComponent(typeof(Canvas))]
	public sealed class PauseMenu : MonoBehaviour {
		private Canvas canvas;
		private bool wasCancelPressed;

		public void Awake() {
			GameManager.Instance.CursorEnabled = false;
			this.canvas = GetComponent<Canvas>();
			this.wasCancelPressed = false;
		}
		
		public void Disable() {
			canvas.enabled = false;
			Time.timeScale = 1;
		}

		public void Enable() {
			canvas.enabled = true;
			GameManager.Instance.HideFader();
			Time.timeScale = 0;
		}

		public void Update() {
			if (wasCancelPressed && !Input.GetButton("Cancel")) {
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
		}
	}
}