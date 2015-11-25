using UnityEngine;
using UnityEngine.UI;

namespace LotsOfTowers.ToolTip
{
	public class ModalPanel : MonoBehaviour
	{
		public Text message;
		public Text hint;
		//public Image iconImage;
		public string[] closeKeys;

		void Awake()
		{
			closeKeys = new string[] { "Escape" };
		}

		void Update()
		{
			foreach (string key in closeKeys)
				if (Input.GetButtonDown(key) || Input.GetAxisRaw(key) != 0)
					ClosePanel(0f);
		}

		public void Tooltip(string message, string hint, string[] possibleCloseKeys, bool autoClose)
		{
			this.closeKeys = possibleCloseKeys;
			//this.iconImage.sprite = icon;
			this.message.text = message;
			this.hint.text = "(" + hint + ")";

			if (autoClose)
				ClosePanel(5f);
		}

		void ClosePanel(float delay)
		{
			Destroy(gameObject, delay);
		}
	}
}
