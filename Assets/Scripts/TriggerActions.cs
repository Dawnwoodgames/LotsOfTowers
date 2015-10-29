using UnityEngine;
using System.Collections;

public class TriggerActions : MonoBehaviour {

	public GameObject tooltip;

	void OnTriggerEnter(Collider other){
		Debug.Log (other);
		GameObject c = Instantiate (tooltip) as GameObject;
		c.GetComponent<ModalPanel>().Tooltip("Press Space to jump","Jump to close this tooltip",new KeyCode[]{KeyCode.Space},false);
	}
}