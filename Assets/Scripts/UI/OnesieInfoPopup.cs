using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.UI
{

	public class OnesieInfoPopup : MonoBehaviour
	{
		public GameObject elephantTip;
		public GameObject hamsterTip;
		public GameObject dragonTip;

		private OnesieType onesieType;
		private bool waiting = true;

		public void ShowPopup(OnesieType type, float waitingTime)
		{
			switch (type)
			{
				case OnesieType.Elephant:
					elephantTip.SetActive(true);
					break;
				case OnesieType.Hamster:
					hamsterTip.SetActive(true);
					break;
				case OnesieType.Dragon:
					dragonTip.SetActive(true);
					break;
				default:
					break;
			}

			StartCoroutine("WaitForIt", waitingTime);
		}

		IEnumerator WaitForIt(float waitingTime)
		{
			yield return new WaitForSeconds(waitingTime);
			waiting = false;
		}


		public void Update()
		{
			if (Input.GetButtonDown("Submit") && !waiting)
			{
				if (elephantTip.activeSelf || hamsterTip.activeSelf || dragonTip.activeSelf)
				{
					elephantTip.SetActive(false);
					hamsterTip.SetActive(false);
					dragonTip.SetActive(false);
					waiting = true;
				}
			}
		}
	}
}