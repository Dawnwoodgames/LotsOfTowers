using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Tooltip : MonoBehaviour{

	private static Dictionary<string,string[]> tooltips = new Dictionary<string,string[]> () {
		{ "Jump", new string[]{ "Press the Jump button to Jump", "Close this dialog by jumping" } },
		{ "Movement", new string[]{ "Press the arrow keys, wasd or use a analog stick to move", "Try moving a little" } },
		{ "Onesie_chicken", new string[]{ "This onesie lets you jump higher", "Jump!" } }
	};

	public static void ShowTooltip(GameObject tooltip, string name, bool autoClose, string[] possibleCloseKeys){
		if (!Convert.ToBoolean(PlayerPrefs.GetInt ("Tutorial_"+name))) {
			GameObject c = Instantiate (tooltip) as GameObject;
			c.GetComponent<ModalPanel> ().Tooltip (tooltips[name][0], tooltips[name][1], possibleCloseKeys, autoClose);
			PlayerPrefs.SetInt ("Tutorial_"+name, Convert.ToInt32 (true));
		}
	}
}