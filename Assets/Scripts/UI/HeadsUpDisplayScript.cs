using UnityEngine;
using UnityEngine.UI;
using Nimbi.Actors;

namespace Nimbi.UI {
    public class HeadsUpDisplayScript : MonoBehaviour {
        private bool isUsingController;
        private Onesie onesie;
        private Player player;
        private Sprite[] sprites;
        private GameObject skillsUi;

        public void Awake() {
            this.player = FindObjectOfType<Player>();
            this.skillsUi = GameObject.Find("HUD/Skills");
            this.sprites = new Sprite[] {
                Resources.Load<Sprite>("HUD/OnesieIdle"),
                Resources.Load<Sprite>("HUD/OnesieIdleController"),
                Resources.Load<Sprite>("HUD/OnesieElephant"),
                Resources.Load<Sprite>("HUD/OnesieElephantController"),
                Resources.Load<Sprite>("HUD/OnesieHamster"),
                Resources.Load<Sprite>("HUD/OnesieHamsterController"),
                Resources.Load<Sprite>("HUD/OnesieDragon"),
                Resources.Load<Sprite>("HUD/OnesieDragonController")
            };
        }

        public void ShowActiveSkill(string _onesie) {
            switch (_onesie) {
                case "OnesieElephant":
                    skillsUi.GetComponent<Image>().sprite = sprites[GameManager.Instance.JoystickConnected ? 3 : 2];
                    break;
                case "OnesieDragon":
                    skillsUi.GetComponent<Image>().sprite = sprites[GameManager.Instance.JoystickConnected ? 7 : 6];
                    break;
                case "OnesieHamster":
                    skillsUi.GetComponent<Image>().sprite = sprites[GameManager.Instance.JoystickConnected ? 5 : 4];
                    break;
                default:
                    skillsUi.GetComponent<Image>().sprite = sprites[GameManager.Instance.JoystickConnected ? 1 : 0];
                    break;
            }
        }

        public void Update() {
            if (isUsingController != GameManager.Instance.JoystickConnected || onesie != player.Onesie) {
                isUsingController = GameManager.Instance.JoystickConnected;
                onesie = player.Onesie;
                ShowActiveSkill(player.Onesie.name);
            }
        }
    }
}