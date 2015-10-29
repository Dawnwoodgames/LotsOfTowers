using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ModalPanel : MonoBehaviour {

	public Text message;
	public Text hint;
	//public Image iconImage;
	public KeyCode[] closeKeys;

	void Awake(){
		closeKeys = new KeyCode[]{KeyCode.Escape};
	}

	void Update(){
		foreach(KeyCode key in closeKeys)
			if (Input.GetKeyDown (key))
				ClosePanel(0f);
	}
		
	public void Tooltip (string message, string hint, KeyCode[] possibleCloseKeys, bool autoClose) {
		this.closeKeys = possibleCloseKeys;
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