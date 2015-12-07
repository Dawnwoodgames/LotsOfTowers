using UnityEngine;
using System.Collections;
using LotsOfTowers.Interaction.Trigger;

public class WaterPuzzle_Temporary_Script : MonoBehaviour {

	void OnGUI () {
		GUI.Label(new Rect (10, 10, 320, 60), "" + BuoyGateTrigger.BuoysTriggered);
	}
}