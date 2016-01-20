using Nimbi.Actors;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nimbi.VFX {
    [RequireComponent(typeof(Animator))]
    public sealed class OnesieSwitch : MonoBehaviour {
        private Animator animator;
        private Player player;
        private List<MeshRenderer> renderers;

        public void Awake() {
            this.animator = GetComponent<Animator>();
            this.player = FindObjectOfType<Player>();
            this.renderers = GetComponentsInChildren<MeshRenderer>().ToList();

            SetVisible(false);
        }

        public void Play(int onesieIndex) {
            StartCoroutine(PlayCoroutine(onesieIndex));
        }

        private IEnumerator PlayCoroutine(int onesieIndex) {
            SetVisible(true);
            animator.Play("VFX");

            yield return new WaitForSeconds(0.06f);

            player.SwitchOnesie(onesieIndex);

            while (animator.GetCurrentAnimatorStateInfo(0).IsName("VFX")) {
                yield return null;
            }

            SetVisible(false);
        }

        public void SetVisible(bool visible) {
            renderers.ForEach(r => r.enabled = visible);
        }
    }
}