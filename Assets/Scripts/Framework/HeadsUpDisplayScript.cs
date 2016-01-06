using UnityEngine;
using UnityEngine.UI;
using LotsOfTowers.Actors;
using LotsOfTowers.Audio;

namespace LotsOfTowers.Framework
{
	public class HeadsUpDisplayScript : MonoBehaviour
    {
        private Sprite[] sprites;
        private Player player;
        private GameObject skillsUi;

		public void Awake()
		{
			this.skillsUi = GameObject.Find("Skills");
            this.sprites = new Sprite[] {
                Resources.Load<Sprite>("HUD/OnesieIdle"),
                Resources.Load<Sprite>("HUD/OnesieElephant"),
                Resources.Load<Sprite>("HUD/OnesieHamster"),
                Resources.Load<Sprite>("HUD/OnesieDragon")
            };
            this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        public void ShowActiveSkill(string _onesie)
        {
            switch (_onesie)
            {
                case "OnesieElephant":
                    skillsUi.GetComponent<Image>().sprite = sprites[1];
                    break;
                case "OnesieDragon":
                    skillsUi.GetComponent<Image>().sprite = sprites[3];
                    break;
                case "OnesieHamster":
                    skillsUi.GetComponent<Image>().sprite = sprites[2];
                    break;
                default:
                    skillsUi.GetComponent<Image>().sprite = sprites[0];
                    break;
            }
        }

        public void Update()
        {
            ShowActiveSkill(player.Onesie.name);
        }
    }
}