using UnityEngine;

public class ToggleMainLightTrigger : MonoBehaviour {

	public Light mainLight;

	void OnTriggerStay(Collider coll)
	{
		if (coll.tag == "Player")
		{
			mainLight.enabled = false;
		}
	}

	void OnTriggerExit(Collider coll)
	{
		if(coll.tag == "Player")
		{
			mainLight.enabled = true;
		}
	}

}
