using UnityEngine;
using UnityEngine.UI;

namespace LotsOfTowers.UI {
	[RequireComponent(typeof(Image))]
	public sealed class UITooltip : MonoBehaviour {
		public static readonly int FadeSpeed = 4;
		public static readonly int TooltipHeight = (int)(Screen.height * 0.2f); // 20% of screen height
		public static readonly int TooltipWidth = (int)(Screen.width * 0.8f); // 80% of screen width

		private Image image;

		public float duration;

		public void Awake() {
			this.image = GetComponent<Image>();

			image.rectTransform.sizeDelta = new Vector2(TooltipWidth, TooltipHeight);
			image.rectTransform.position = new Vector3(0, Screen.height / 2 - TooltipHeight, 0);
		}

		public void Update() {
			duration -= Time.smoothDeltaTime;
			image.color = new Color(image.color.r, image.color.g, image.color.b, 0.25f * Mathf.Sin (FadeSpeed * duration) + 0.75f);

			if (duration < 0) {
				Destroy(gameObject);
			}
		}
	}
}