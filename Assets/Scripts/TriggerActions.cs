using UnityEngine;
using System.Collections;

public class TriggerActions : MonoBehaviour {

	public GameObject tooltip;
	private bool jumpTooltipAppeared = false;

	void OnTriggerEnter(Collider other){
		if (!jumpTooltipAppeared) {
			GameObject c = Instantiate (tooltip) as GameObject;
			c.GetComponent<ModalPanel> ().Tooltip ("Press Space to jump", "Jump to close this tooltip", new string[]{ "Jump" }, false);
		}
		jumpTooltipAppeared = true;
	}
}