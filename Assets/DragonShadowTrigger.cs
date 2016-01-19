using UnityEngine;
using System.Collections;
using Nimbi.Actors;

public class DragonShadowTrigger : MonoBehaviour
{
	public Light dragonLight;
	public Transform scaryGuy;
	public bool isCompleted = false;
	private bool showDragonShadow = false;

	// Update is called once per frame
	void OnTriggerStay(Collider coll)
	{
		if (coll.tag == "Player")
		{
			if (coll.GetComponent<Player>().Onesie.name == "Dragon" && dragonLight.isActiveAndEnabled)
			{
				showDragonShadow = true;
			}
		}
	}

	void Update()
	{
		if(showDragonShadow)
		{
			//Let scaryguy run
			//scaryGuy.LERPDIEDERP

			isCompleted = true;
		}
	}
}
