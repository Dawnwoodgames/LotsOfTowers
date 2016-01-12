using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Nimbi.Audio;

using Nimbi.Framework;

namespace Nimbi.Actors {
    public class Player : MonoBehaviour {
        private float charge;
        private Onesie currentOnesie;
        private Skeleton currentSkeleton;
        private Onesie defaultOnesie;
        private Onesie[] onesies;
        private List<GameObject> particleSystems;
        private List<Skeleton> skeletons;

        public Animator Animator {
            get { return currentSkeleton.GetComponent<Animator>(); }
        }

        public bool HoldingWater {
            get;
            set;
        }

        public Onesie Onesie {
            get { return currentOnesie == null ? defaultOnesie : currentOnesie; }
            set { currentOnesie = onesies.Contains(value) ? value : currentOnesie; }
        }

        public Onesie[] Onesies {
            get { return onesies; }
        }

        public float StaticCharge {
            get { return charge; }
            set { charge = Mathf.Max(0, Mathf.Min(value, 100)); }
        }

        public void AddOnesie(Onesie onesie) {
            switch (onesie.type) {
                case OnesieType.Dragon:
                    onesies[2] = onesie;
                    break;

                case OnesieType.Elephant:
                    onesies[0] = onesie;
                    break;

                case OnesieType.Hamster:
                    onesies[1] = onesie;
                    break;

                default:
                    break;
            }
        }

        public void Awake() {
            this.defaultOnesie = Resources.Load<Onesie>("OnesieDefault");
            this.onesies = new Onesie[3];
            this.particleSystems = GameObject.Find("Nimbi/VFX").transform.Cast<Transform>()
                .Where(t => t.GetComponent<ParticleSystem>() != null)
                .Select(t => t.gameObject).ToList();
            this.skeletons = GetComponentsInChildren<Skeleton>().ToList();
        }

        public bool HasOnesie(OnesieType type) {
            foreach (Onesie onesie in onesies) {
                if (onesie != null && onesie.type == type) {
                    return true;
                }
            }

            return false;
        }

        public void ResetRenderers() {
            currentSkeleton.Renderer.enabled = true;
            skeletons.Where(s => s != currentSkeleton).ToList().ForEach(s => s.Renderer.enabled = false);
        }

        public void SetEffectActive(string name, bool active) {
            particleSystems.Single(g => g.name == name).SetActive(active);
        }

        private void SetSkeleton(string name) {
            currentSkeleton = skeletons.Single(s => s.name == name);
            currentSkeleton.Renderer.enabled = true;
            skeletons.Where(s => s != currentSkeleton).ToList().ForEach(s => s.Renderer.enabled = false);
        }

        public void Start() {
            Physics.gravity = new Vector3(0, -35, 0);
            SetSkeleton("Default");
        }

        public void SwitchOnesie(int index) {
            if (index > -1 && index < 3 && onesies[index] != null) {
                try {
                    currentOnesie = (currentOnesie == onesies[index]) ? defaultOnesie : onesies[index];
                    AudioManager.Instance.PlaySoundeffect(AudioManager.Instance.GetOnesieSwitchSound(currentOnesie.name));
                    SetSkeleton(currentOnesie.name.Replace("Onesie", ""));
                } catch (Exception ex) {
                    Debug.Log("Error in Player.cs: " + ex.Message + ", \r\nTrace: " + ex.StackTrace);
                    SetSkeleton("Default");
                }
            }
        }

        public void Update() {
            SetEffectActive("Drops", HoldingWater);
            SetEffectActive("Sparks", StaticCharge > 90);
        }
    }
}