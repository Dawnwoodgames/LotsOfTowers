using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace LotsOfTowers.Menu
{
    public class MenuScript : MonoBehaviour {

        public GameObject loadingImage;
        public GameObject playButton; // Index 1
        public GameObject levelSelectButton; // Index 2
        public GameObject settingsButton; // Index 3
        public GameObject quitButton; // Index 4

        private GameObject firstItem;
        private GameObject lastItem;
        private GameObject activeItem;

        private ArrayList buttonList = new ArrayList();

        private bool isMoving = false;

        private Color white = Color.white;
        private Color defaultColor = new Color(52 / 255f, 57 / 255f, 61 / 255f);

        // 
        void Start()
        {
            SetButtonTextsAndPopulateListArray();

            firstItem = playButton;  
            lastItem = quitButton;

            SetActiveMenuItem(firstItem);
        }
        
        void Update()
        {
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool up = v > 0;
            bool down = v < 0;
            
            if(up && !isMoving)
            {
                isMoving = true;
                MoveCursorUp();
            }

            if (down && !isMoving)
            {
                isMoving = true;
                MoveCursorDown();
            }

            if(v == 0)
            {
                isMoving = false;
            }
            
            if(CrossPlatformInputManager.GetButton("Jump"))
            {
                activeItem.GetComponent<Button>().onClick.Invoke();
            }

        }

        private void MoveCursorUp()
        {
            if (activeItem != firstItem)
            {
                int currentIndex = buttonList.IndexOf(activeItem);
                SetActiveMenuItem( (GameObject) buttonList[currentIndex - 1] );
            }
        }

        private void MoveCursorDown()
        {
            if (activeItem != lastItem)
            {
                int currentIndex = buttonList.IndexOf(activeItem);
                SetActiveMenuItem( (GameObject) buttonList[currentIndex + 1]);
            }
        }

        private void SetActiveMenuItem(GameObject button)
        {
            if (activeItem)
            {
                activeItem.GetComponentInChildren<Text>().color = defaultColor; // Reset color
            }

            button.GetComponentInChildren<Text>().color = white;
            activeItem = button;
        }
        
        private void LoadScene(int level)
        {
            loadingImage.SetActive(true);
            Application.LoadLevel(level); 
        }

        public void LoadLevelSelectScene()
        {
            //
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

        
        private void SetButtonTextsAndPopulateListArray()
        {
            if (playButton)
            {
                Text t = playButton.GetComponentInChildren<Text>();
                string playButtonText = ("menu.playbutton.text").Localize();
                t.text = playButtonText;
                buttonList.Add(playButton);
            }

            if (levelSelectButton)
            {
                Text t = levelSelectButton.GetComponentInChildren<Text>();
                string levelSelectButtonText = ("menu.levelselectbutton.text").Localize();
                t.text = levelSelectButtonText;
                buttonList.Add(levelSelectButton);
            }

            if (settingsButton)
            {
                Text t = settingsButton.GetComponentInChildren<Text>();
                string settingsButtonText = ("menu.settingsbutton.text").Localize();
                t.text = settingsButtonText;
                buttonList.Add(settingsButton);
            }

            if (quitButton)
            {
                Text t = quitButton.GetComponentInChildren<Text>();
                string quitButtonText = ("menu.quitbutton.text").Localize();
                t.text = quitButtonText;
                buttonList.Add(quitButton);
            }
        }

    }
}