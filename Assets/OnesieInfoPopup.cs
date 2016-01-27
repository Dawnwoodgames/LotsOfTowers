using UnityEngine;
using System.Collections;
using Nimbi.Actors;

public class OnesieInfoPopup : MonoBehaviour {

	public GameObject elephantTip;
	public GameObject hamsterTip;
	public GameObject dragonTip;

	private OnesieType onesieType;
	
	public void ShowPopup(OnesieType type)
	{
		switch (type)
		{
			case OnesieType.Elephant:
				elephantTip.SetActive(true);
				break;
			case OnesieType.Hamster:
				hamsterTip.SetActive(true);
				break;
			default:
				break;
		}
	}

	public void Update()
	{
		if(Input.GetButtonDown("Submit"))
		{
			if(elephantTip.activeSelf || hamsterTip.activeSelf || dragonTip.activeSelf)
			{
				elephantTip.SetActive(false);
				hamsterTip.SetActive(false);
				dragonTip.SetActive(false);
			}
		}
	}
}
