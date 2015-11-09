using UnityEngine;
using UnityEngine.UI;

namespace LotsOfTowers.Framework
{
	public class HeadsUpDisplayScript : MonoBehaviour
	{

		// GameObjects
		public GameObject skillsUi;
		private GameObject skillOne, skillTwo, skillThree;

		// public variables
		public int equipSlot = 0;

		// private variables
		private Color opacitySkillOne, opacitySkillTwo, opacitySkillThree;
		private float displayTime = 3;
		private float timer = 0;

		// Use this for initialization
		void Start()
		{
			skillsUi = GameObject.Find("Skills");
			skillOne = GameObject.Find("One");
			skillTwo = GameObject.Find("Two");
			skillThree = GameObject.Find("Three");
			skillOne.SetActive(false);
			skillTwo.SetActive(false);
			skillThree.SetActive(false);
			skillsUi.SetActive(false);
		}

		// Update is called once per frame
		void Update()
		{
			if (skillsUi.activeSelf)
			{
				HideSkillUiInSeconds();
			}

			if (Input.GetButton("ShowUi"))
				skillsUi.SetActive(true);
		}

		private void HideSkillUiInSeconds()
		{
			timer += Time.deltaTime;

			if (timer >= displayTime)
			{
				skillsUi.SetActive(false);
				timer = 0;
			}
		}

		public void AttachOnesieToSkillSlot(int slot, string name)
		{
			switch (slot)
			{
				case 0:
					skillOne.GetComponent<Image>().sprite = Resources.Load("HUD/" + name, typeof(Sprite)) as Sprite;
					if (!skillOne.activeSelf) skillOne.SetActive(true);
					break;
				case 1:
					skillTwo.GetComponent<Image>().sprite = Resources.Load("HUD/" + name, typeof(Sprite)) as Sprite;
					if (!skillTwo.activeSelf) skillTwo.SetActive(true);
					break;
				case 2:
					skillThree.GetComponent<Image>().sprite = Resources.Load("HUD/" + name, typeof(Sprite)) as Sprite;
					if (!skillThree.activeSelf) skillThree.SetActive(true);
					break;
				default:
					break;
			}
		}
	}
}