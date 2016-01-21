using Nimbi.Actors;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nimbi.VFX {
    [RequireComponent(typeof(Animator))]
    public class OnesieSwitch : MonoBehaviour {
        private Animator animator;
        private Player player;
        private List<MeshRenderer> renderers;

        public void Awake() {
            animator = GetComponent<Animator>();
            player = FindObjectOfType<Player>();
            renderers = GetComponentsInChildren<MeshRenderer>().ToList();

            SetVisible(false, Color.clear);
        }

        public void Play(int onesieIndex) {
            StartCoroutine(PlayCoroutine(onesieIndex));
        }

        private IEnumerator PlayCoroutine(int onesieIndex) {
			Color color;

			//Change color based on onesie
			switch (onesieIndex)
			{
				default :
				case 0:
					//Elephant
					color = Color.blue;
                    break;
				case 1:
					//Hamster
					color = Color.yellow;
					break;
				case 2:
					//Dragon
					color = Color.red;
					break;
			}

			SetVisible(true, color);
            animator.Play("VFX");

            yield return new WaitForSeconds(0.06f);
			
            player.SwitchOnesie(onesieIndex);

            while (animator.GetCurrentAnimatorStateInfo(0).IsName("VFX")) {
                yield return null;
            }

            SetVisible(false, Color.clear);
        }

        public void SetVisible(bool visible, Color color) {
			//Set Color
			renderers.ForEach(r => r.material.color = color);
			//Set visibility
			renderers.ForEach(r => r.enabled = visible);
		}
	}
}