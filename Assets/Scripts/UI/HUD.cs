using Nimbi.Actors;
using Nimbi.Framework;
using Nimbi.Interaction;
using UnityEngine;
using UnityEngine.UI;

namespace Nimbi.UI {
    public sealed class HUD : MonoBehaviour {
        private LevelSlider levelSlider;
        private WaterMazeManager mazeManager;
        private Player player;

        public GameObject buoyCounter;
        public Text buoyCounterLeftDigit, buoyCounterRightDigit;

        public Image dragonSlot, elephantSlot, hamsterSlot;

        public Sprite dragonSlotDisabled;
        public Sprite dragonSlotEmpty;
        public Sprite dragonSlotEnabled;

        public Sprite elephantSlotDisabled;
        public Sprite elephantSlotEmpty;
        public Sprite elephantSlotEnabled;

        public Sprite hamsterSlotDisabled;
        public Sprite hamsterSlotEmpty;
        public Sprite hamsterSlotEnabled;

        public void Awake() {
            this.buoyCounter.SetActive(false);
            this.mazeManager = FindObjectOfType<WaterMazeManager>();
            this.player = FindObjectOfType<Player>();
            Invalidate();
        }

        private void Invalidate() {
            // Main HUD elements
            dragonSlot.sprite = player.HasOnesie(OnesieType.Dragon) ? (player.Onesie.type == OnesieType.Dragon ?
                dragonSlotEnabled : dragonSlotDisabled) : dragonSlotEmpty;
            elephantSlot.sprite = player.HasOnesie(OnesieType.Elephant) ? (player.Onesie.type == OnesieType.Elephant ?
                elephantSlotEnabled : elephantSlotDisabled) : elephantSlotEmpty;
            hamsterSlot.sprite = player.HasOnesie(OnesieType.Hamster) ? (player.Onesie.type == OnesieType.Hamster ?
                hamsterSlotEnabled : hamsterSlotDisabled) : hamsterSlotEmpty;

            // Buoy HUD elements
            if (mazeManager == null) {
                buoyCounter.SetActive(true);
            } else {
                if (levelSlider == null) {
                    levelSlider = GameObject.Find("LevelSlider/Segment1").GetComponent<LevelSlider>();
                }

                string digits = string.Format("{0:00}", mazeManager.GatesOpened);

                buoyCounter.SetActive(levelSlider.InTrigger);
                buoyCounterLeftDigit.text = digits[0].ToString();
                buoyCounterRightDigit.text = digits[1].ToString();
            }
        }

        public void Update() {
            Invalidate();
        }
    }
}