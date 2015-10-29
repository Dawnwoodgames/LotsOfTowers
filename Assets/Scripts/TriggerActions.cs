using UnityEngine;
using System.Collections;
using System;

public class TriggerActions : MonoBehaviour {

	public GameObject tooltip;

	void OnTriggerEnter(Collider other){
		Tooltip.ShowTooltip (tooltip, "Jump",false,new string[]{"Jump"});

	}
}