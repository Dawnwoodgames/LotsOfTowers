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
			ClosePanel();
	}

	// Yes/No/Cancel: A string, a Yes event, a No event and Cancel event
	public void Tooltip (string message, string closeKey) {
		this.closeKey = closeKey;
		//this.iconImage.sprite = icon;

		this.message.text = message;

		//this.iconImage.gameObject.SetActive (false);
	}

	void ClosePanel () {
		Destroy (gameObject);
	}
}
