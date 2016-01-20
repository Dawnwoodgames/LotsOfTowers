using UnityEngine;
using System.Collections;

public class FireLightTrigger : MonoBehaviour {

	public Light dragonLight;
		
	// Update is called once per frame
	void OnTriggerEnter(Collider coll)
	{
		if(coll.tag == "Fire")
		{
			dragonLight.enabled = true;
		}
	}
}
