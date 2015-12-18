using UnityEngine;
using UnityEngine.UI;

namespace LotsOfTowers.UI {
	[RequireComponent(typeof(Image))]
	public sealed class UITooltip : MonoBehaviour {
		private string axis;
		private Image image;

		public float duration;

		public void Awake() {
			this.axis = string.Empty;
			this.image = GetComponent<Image>();
			this.image.enabled = false;
		}

		public void SetAxis(string axis) {
			this.axis = axis;
		}

		public void SetSprite(Sprite sprite) {
			image.GetComponent<RectTransform>().localPosition =
				new Vector3(0, Screen.height / -2 + sprite.rect.size.y, 0);
			image.GetComponent<RectTransform>().sizeDelta =
				new Vector2(sprite.rect.size.x, sprite.rect.size.y);
			image.sprite = sprite;
			image.enabled = true;
		}

		public void Update() {
			if (axis != string.Empty && Input.GetAxis(axis) != 0) {
				Destroy(gameObject);
			}
		}
	}
}