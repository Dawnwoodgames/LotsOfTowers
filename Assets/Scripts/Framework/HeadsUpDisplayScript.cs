using UnityEngine;
using UnityEngine.UI;

namespace LotsOfTowers.Framework
{
	public class HeadsUpDisplayScript : MonoBehaviour
	{
		private GameObject skillsUi;

		void Awake()
		{
			skillsUi = GameObject.Find("Skills");
		}

        public void ShowActiveSkill(string _onesie)
        {
            switch (_onesie)
            {
                case "Elephant":
                    skillsUi.GetComponent<Image>().sprite = Resources.Load("HUD/OnesieElephant", typeof(Sprite)) as Sprite;
                    break;
                case "Dragon":
                    skillsUi.GetComponent<Image>().sprite = Resources.Load("HUD/OnesieDragon", typeof(Sprite)) as Sprite;
                    break;
                case "Hamster":
                    skillsUi.GetComponent<Image>().sprite = Resources.Load("HUD/OnesieHamster", typeof(Sprite)) as Sprite;
                    break;
                default:
                    skillsUi.GetComponent<Image>().sprite = Resources.Load("HUD/" + "OnesieIdle", typeof(Sprite)) as Sprite;
                    break;
            }
        }
	}
}