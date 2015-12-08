using UnityEngine;

namespace LotsOfTowers.UI {
	[RequireComponent(typeof(RectTransform))]
	public sealed class UIComponent : MonoBehaviour {
		private Canvas canvas;
		private float originHeight, originWidth;
		private RectTransform rectTransform;

		public float height, width; // Dimensions of this components, px or %
		public bool realtime; // Whether or not this component should update every frame
		public bool responsive; // Whether or not this component is responsive

		public void Awake() {
			// Get component references and fix values
			this.canvas = GetComponentInParent<Canvas>();
			this.height = responsive ? Mathf.Max(0, Mathf.Min(height, 100)) : Mathf.Max(0, height);
			this.rectTransform = GetComponent<RectTransform>();
			this.width = responsive ? Mathf.Max(0, Mathf.Min(width, 100)) : Mathf.Max(0, width);
			// Get reference height and width
			this.originHeight = (canvas.renderMode == RenderMode.ScreenSpaceOverlay) ? Screen.height : canvas.GetComponent<RectTransform> ().sizeDelta.y;
			this.originWidth = (canvas.renderMode == RenderMode.ScreenSpaceOverlay) ? Screen.width : canvas.GetComponent<RectTransform> ().sizeDelta.x;
			// Apply settings
			Invalidate();
		}

		public void Invalidate() {
			rectTransform.sizeDelta = new Vector3(
				responsive ? originWidth / 100 * width : width,
				responsive ? originHeight / 100 * height : height
			);
		}

		public void Update() {
			if (realtime) {
				Invalidate();
			}
		}
	}
}