using UnityEngine;
using System.Collections;

namespace Nimbi.Audio
{
    public class AudioManager : MonoBehaviour {

        private static AudioManager instance;

        public GameObject soundEffects;
        public GameObject backgroundSounds;

        public AudioClip backgroundMusic;

        public AudioClip switchOnesieToDefaultSoundFile;
        public AudioClip switchOnesieToElephantSoundFile;
        public AudioClip switchOnesieToHamsterSoundFile;

        public AudioClip[] enviromentSounds;
        
        public AudioClip onesieSwitchDefaultSound { get; set; }
        public AudioClip onesieSwitchElephantSound { get; set; }
        public AudioClip onesieSwitchHamsterSound { get; set; }

        private int lastrnd;
        private bool playing = false;

        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (AudioManager)Instantiate(Resources.Load("Prefabs/AudioManager"));
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
            onesieSwitchElephantSound = switchOnesieToElephantSoundFile;
            onesieSwitchHamsterSound = switchOnesieToHamsterSoundFile;

            playRandomSong();
        }

        void Update()
        {
            if (!playing)
            {
                StartCoroutine(PlayMusic());
                playing = true;
            }
        }

        IEnumerator PlayMusic()
        {
            yield return new WaitForSeconds(17);
            playRandomSong();
        }

        private int createRandomNumberForSound() {
            return Random.Range(0, (enviromentSounds.Length - 1));
        }

        private void playRandomSong()
        {
            int rnd = createRandomNumberForSound();

            while (rnd == lastrnd)
            {
                rnd = createRandomNumberForSound();
            }

            lastrnd = rnd;

            backgroundSounds.GetComponent<AudioSource>().clip = enviromentSounds[rnd];
            backgroundSounds.GetComponent<AudioSource>().Play();
            playing = false;
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

