using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ModalPanel : MonoBehaviour {

	public Text message;
	public Text hint;
	//public Image iconImage;
	public KeyCode closeKey;

	void Awake(){
		closeKey = KeyCode.Escape;
	}

	void Update(){
		if (Input.GetKeyDown (closeKey))
			ClosePanel(0f);
	}
		
	public void Tooltip (string message, string hint, KeyCode closeKey, bool autoClose) {
		this.closeKey = closeKey;
		//this.iconImage.sprite = icon;
		this.message.text = message;
		this.hint.text = "(" + hint + ")";

		if (autoClose)
			ClosePanel (5f);
	}

	void ClosePanel (float delay) {
		Destroy (gameObject, delay);
	}
}