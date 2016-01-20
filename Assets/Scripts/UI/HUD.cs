using Nimbi.Actors;
using UnityEngine;
using UnityEngine.UI;

namespace Nimbi.UI {
    public sealed class HUD : MonoBehaviour {
        private Player player;

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
            this.player = FindObjectOfType<Player>();
            Invalidate();
        }

        private void Invalidate() {
            dragonSlot.sprite = player.hasDragonOnesie ? (player.Onesie.type == OnesieType.Dragon ?
                dragonSlotEnabled : dragonSlotDisabled) : dragonSlotEmpty;
            elephantSlot.sprite = player.hasElephantOnesie ? (player.Onesie.type == OnesieType.Elephant ?
                elephantSlotEnabled : elephantSlotDisabled) : elephantSlotEmpty;
            hamsterSlot.sprite = player.hasHamsterOnesie ? (player.Onesie.type == OnesieType.Hamster ?
                hamsterSlotEnabled : hamsterSlotDisabled) : hamsterSlotEmpty;
        }

        public void Update() {
            Invalidate();
        }
    }
}