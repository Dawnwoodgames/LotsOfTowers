using UnityEngine;
using System.Collections;
using System;

public class TriggerActions : MonoBehaviour {

	public GameObject tooltip;
	private bool jumpTooltipAppeared = false;

	void Start(){
		jumpTooltipAppeared = Convert.ToBoolean(PlayerPrefs.GetInt ("JumpTutorial"));
	}

	void OnTriggerEnter(Collider other){
		if (!jumpTooltipAppeared) {
			GameObject c = Instantiate (tooltip) as GameObject;
			c.GetComponent<ModalPanel> ().Tooltip ("Press the jump button to jump", "Jump to close this tooltip", new string[]{ "Jump" }, false);
			jumpTooltipAppeared = true;
			PlayerPrefs.SetInt ("JumpTutorial", Convert.ToInt32 (jumpTooltipAppeared));
		}

	}
}