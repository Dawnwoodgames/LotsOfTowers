using UnityEngine;

namespace LotsOfTowers.UI {
	[RequireComponent(typeof(Canvas))]
	public sealed class PauseMenu : MonoBehaviour {
		private Canvas canvas;
		private bool wasCancelPressed;

		public void Awake() {
			this.canvas = GetComponent<Canvas>();
			this.wasCancelPressed = false;
		}
		
		public void Disable() {
			canvas.enabled = false;
			Time.timeScale = 1;
		}

		public void Enable() {
			canvas.enabled = true;
			Time.timeScale = 0;
		}

		public void Update() {
			if (wasCancelPressed && !Input.GetButton("Cancel")) {
				// Key up
				if (canvas.enabled) {
					Disable();
				} else {
					Enable();
				}
				wasCancelPressed = false;
			} else if (Input.GetButton("Cancel")) {
				// Key down
				wasCancelPressed = true;
			}
		}
	}
}