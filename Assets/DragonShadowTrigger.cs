using UnityEngine;
using System.Collections;
using Nimbi.Actors;

public class DragonShadowTrigger : MonoBehaviour
{
	public Light dragonLight;
	public GameObject scaryGuy;

	[HideInInspector]
	public bool isCompleted = false;

	private bool showDragonShadow = false;

	// Update is called once per frame
	void OnTriggerStay(Collider coll)
	{
		if (coll.tag == "Player")
		{
			if (coll.GetComponent<Player>().Onesie.type == OnesieType.Dragon && dragonLight.isActiveAndEnabled)
			{
				showDragonShadow = true;
			}
		}
	}

	void Update()
	{
		if(showDragonShadow)
		{
			//Let scaryguy scare
			scaryGuy.SetActive(false);

			isCompleted = true;
		}
	}
}
