using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Audio
{
    public class AudioManager : MonoBehaviour {

        public GameObject soundEffects;
        public GameObject backgroundSounds;

        public AudioClip switchOnesieToDefaultSoundFile;
        public AudioClip switchOnesieToElephantSoundFile;
        public AudioClip switchOnesieToHamsterSoundFile;

        public AudioClip[] enviromentSounds;
        
        public AudioClip onesieSwitchDefaultSound { get; set; }
        public AudioClip onesieSwitchElephantSound { get; set; }
        public AudioClip onesieSwitchHamsterSound { get; set; }

        void Start()
        {
            DontDestroyOnLoad(this);

            onesieSwitchDefaultSound = switchOnesieToDefaultSoundFile;
            onesieSwitchElephantSound = switchOnesieToElephantSoundFile;
            onesieSwitchHamsterSound = switchOnesieToHamsterSoundFile;
        }

        void Update()
        {
            
        }

        public void PlaySoundeffect(AudioClip sound)
        {
            soundEffects.GetComponent<AudioSource>().clip = sound;
            soundEffects.GetComponent<AudioSource>().Play();
        }
    }

}

