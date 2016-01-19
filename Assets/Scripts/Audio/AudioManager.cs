using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Nimbi.Audio
{
    public class AudioManager : MonoBehaviour {

        private static AudioManager instance;

        public GameObject soundEffects;
        public GameObject backgroundMusic;

        public AudioClip backgroundMusicTowerOne;
        public AudioClip backgroundMusicTowerTwo;
        public AudioClip backgroundMusicTowerThree;
        public AudioClip backgroundMusicTowerFour;
        public AudioClip backgroundMusicTowerFive;

        public AudioClip switchOnesieToDefaultSoundFile;
        public AudioClip switchOnesieToDragonSoundFile;
        public AudioClip switchOnesieToElephantSoundFile;
        public AudioClip switchOnesieToHamsterSoundFile;

        public AudioClip onesieSwitchDefaultSound { get; set; }
        public AudioClip onesieSwitchDragonSound { get; set; }
        public AudioClip onesieSwitchElephantSound { get; set; }
        public AudioClip onesieSwitchHamsterSound { get; set; }

        private bool playing = false;
        private string currentScene = "";

        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (Instantiate(Resources.Load("Prefabs/AudioManager")) as GameObject).GetComponent<AudioManager>();
                }
                return instance;
            }
        }

        public void Awake()
        {
            if (FindObjectsOfType<AudioManager>().Length > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                AudioManager.instance = this;
            }

            DontDestroyOnLoad(this);
        }

        void Start()
        {
            DontDestroyOnLoad(this);

            onesieSwitchDefaultSound = switchOnesieToDefaultSoundFile;
            onesieSwitchDragonSound = switchOnesieToDragonSoundFile;
            onesieSwitchElephantSound = switchOnesieToElephantSoundFile;
            onesieSwitchHamsterSound = switchOnesieToHamsterSoundFile;
        }

        private void playMusic()
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Tower 1":
                    backgroundMusic.GetComponent<AudioSource>().clip = backgroundMusicTowerOne;
                    backgroundMusic.GetComponent<AudioSource>().Play();
                    playing = true;
                    break;
                case "Tower 2":
                    backgroundMusic.GetComponent<AudioSource>().clip = backgroundMusicTowerTwo;
                    backgroundMusic.GetComponent<AudioSource>().Play();
                    playing = true;
                    break;
                case "Tower 3":
                    backgroundMusic.GetComponent<AudioSource>().clip = backgroundMusicTowerThree;
                    backgroundMusic.GetComponent<AudioSource>().Play();
                    playing = true;
                    break;
                case "Tower 4":
                    backgroundMusic.GetComponent<AudioSource>().clip = backgroundMusicTowerFour;
                    backgroundMusic.GetComponent<AudioSource>().Play();
                    playing = true;
                    break;
                case "Tower 5":
                    backgroundMusic.GetComponent<AudioSource>().clip = backgroundMusicTowerFive;
                    backgroundMusic.GetComponent<AudioSource>().Play();
                    playing = true;
                    break;
                default:
                    backgroundMusic.GetComponent<AudioSource>().clip = backgroundMusicTowerOne;
                    backgroundMusic.GetComponent<AudioSource>().Play();
                    playing = true;
                    break;
            }
        }

        void Update()
        {
            if(!playing)
            {
                playMusic();
            }

            if (currentScene != SceneManager.GetActiveScene().name)
            {
                currentScene = SceneManager.GetActiveScene().name;
                playing = false;
            }
        }
        
        public AudioClip GetOnesieSwitchSound(string onesieString)
        {
            if(onesieString.Contains("Elephant"))
            {
                return onesieSwitchElephantSound;
            }
            else if(onesieString.Contains("Hamster"))
            {
                return onesieSwitchHamsterSound;
            }
            else if (onesieString.Contains("Dragon"))
            {
                return onesieSwitchDragonSound;
            }
            else
            {
                return onesieSwitchDefaultSound;
            }
        }

        public void PlaySoundeffect(AudioClip sound)
        {
            soundEffects.GetComponent<AudioSource>().clip = sound;
            soundEffects.GetComponent<AudioSource>().Play();
        }
    }

}

