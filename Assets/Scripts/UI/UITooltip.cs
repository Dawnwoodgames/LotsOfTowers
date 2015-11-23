using UnityEngine;
using UnityEngine.UI;

namespace LotsOfTowers.UI {
	[RequireComponent(typeof(Image))]
	public sealed class UITooltip : MonoBehaviour {
		public static readonly int TooltipHeight = (int)(Screen.height * 0.2f); // 20% of screen height
		public static readonly int TooltipWidth = (int)(Screen.width * 0.8f); // 80% of screen width

		public float duration;

		public void Awake() {
			GetComponent<Image>().rectTransform.sizeDelta = new Vector2(TooltipWidth, TooltipHeight);
			GetComponent<Image>().rectTransform.position = new Vector3(0, Screen.height / 2 - TooltipHeight, 0);
		}

		public void Update() {
			duration -= Time.smoothDeltaTime;

			if (duration < 0) {
				Destroy(gameObject);
			}
		}
	}
}