using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace LotsOfTowers.Menu
{
    public class LoadOnClick : MonoBehaviour {

        public GameObject loadingImage;
        public GameObject playButton;
        public GameObject levelSelectButton;
        public GameObject settingsButton;
        public GameObject quitButton;

        // 
        void Start()
        {
            SetButtonTexts();
        }

        private void LoadScene(int level)
        {
            loadingImage.SetActive(true);
            Application.LoadLevel(level); 
        }


        // This will open the settings scene to edit the game settings; ie: language.
        public void LoadSettingsScene()
        {
            //Application.LoadLevel();
        }

        // This will open the scene the player was last on. It will act like a "load savegame"
        public void ContinueGame()
        {
            // For now, just load scene 1, cuz we aint got nothing else.
            LoadScene(1);
        }
	    

        public void Quit()
        {
            Application.Quit();
        }


        private void SetButtonTexts()
        {
            if (playButton)
            {
                Text t = playButton.GetComponentInChildren<Text>();
                string playButtonText = ("menu.playbutton.text").Localize();
                t.text = playButtonText;
            }

            if (levelSelectButton)
            {
                Text t = levelSelectButton.GetComponentInChildren<Text>();
                string levelSelectButtonText = ("menu.levelselectbutton.text").Localize();
                t.text = levelSelectButtonText;
            }

            if (settingsButton)
            {
                Text t = settingsButton.GetComponentInChildren<Text>();
                string settingsButtonText = ("menu.settingsbutton.text").Localize();
                t.text = settingsButtonText;
            }

            if (quitButton)
            {
                Text t = quitButton.GetComponentInChildren<Text>();
                string quitButtonText = ("menu.quitbutton.text").Localize();
                t.text = quitButtonText;
            }
        }

    }
}