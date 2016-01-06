using UnityEngine;
using UnityEngine.UI;
using LotsOfTowers.Actors;
using LotsOfTowers.Audio;

namespace LotsOfTowers.Framework
{
	public class HeadsUpDisplayScript : MonoBehaviour
	{
		private GameObject skillsUi;
        private Player player;
        private AudioManager audioManager;

		void Awake()
		{
			skillsUi = GameObject.Find("Skills");
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        void FixedUpdate()
        {
            ShowActiveSkill(player.Onesie.name);
        }

        public void ShowActiveSkill(string _onesie)
        {
            switch (_onesie)
            {
                case "OnesieElephant":
                    skillsUi.GetComponent<Image>().sprite = Resources.Load("HUD/OnesieElephant", typeof(Sprite)) as Sprite;
                    break;
                case "OnesieDragon":
                    skillsUi.GetComponent<Image>().sprite = Resources.Load("HUD/OnesieDragon", typeof(Sprite)) as Sprite;
                    break;
                case "OnesieHamster":
                    skillsUi.GetComponent<Image>().sprite = Resources.Load("HUD/OnesieHamster", typeof(Sprite)) as Sprite;
                    break;
                default:
                    skillsUi.GetComponent<Image>().sprite = Resources.Load("HUD/" + "OnesieIdle", typeof(Sprite)) as Sprite;
                    break;
            }
        }
	}
}