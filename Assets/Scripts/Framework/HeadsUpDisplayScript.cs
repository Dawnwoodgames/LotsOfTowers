using UnityEngine;
using UnityEngine.UI;

namespace LotsOfTowers.Framework
{
	public class HeadsUpDisplayScript : MonoBehaviour
	{
		public GameObject skillsUi;

		void Awake()
		{
			skillsUi = GameObject.Find("Skills");
		}

        public void ShowActiveSkill(int skillSlot)
        {
            switch (skillSlot)
            {
                case 0:
                    skillsUi.GetComponent<Image>().sprite = Resources.Load("HUD/OnesieDragon", typeof(Sprite)) as Sprite;
                    break;
                case 1:
                    skillsUi.GetComponent<Image>().sprite = Resources.Load("HUD/OnesieElephant", typeof(Sprite)) as Sprite;
                    break;
                case 2:
                    skillsUi.GetComponent<Image>().sprite = Resources.Load("HUD/OnesieHamster", typeof(Sprite)) as Sprite;
                    break;
                default:
                    skillsUi.GetComponent<Image>().sprite = Resources.Load("HUD/" + "OnesieIdle", typeof(Sprite)) as Sprite;
                    break;
            }
        }
	}
}