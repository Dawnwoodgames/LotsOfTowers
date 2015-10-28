using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ModalPanel : MonoBehaviour {

	public Text message;
	//public Image iconImage;
	public string closeKey;

	void Awake(){
		closeKey = "x";
	}

	void Update(){
		if (Input.GetKeyDown (closeKey))
			ClosePanel(0f);
	}

	// Yes/No/Cancel: A string, a Yes event, a No event and Cancel event
	public void Tooltip (string message, string closeKey, bool autoClose) {
		this.closeKey = closeKey;
		//this.iconImage.sprite = icon;

		this.message.text = message;

		//this.iconImage.gameObject.SetActive (false);
		if (autoClose)
			ClosePanel (5f);
	}

	void ClosePanel (float delay) {
		Destroy (gameObject, delay);
	}
}
