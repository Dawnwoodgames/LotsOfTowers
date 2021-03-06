﻿using Nimbi.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nimbi.Actors {
    public class Player : MonoBehaviour {
        private float charge;
        private Onesie currentOnesie;
        private Skeleton currentSkeleton;
        private Onesie defaultOnesie;
        private Onesie[] onesies;
        private List<GameObject> particleSystems;
        private List<Skeleton> skeletons;

        public bool hasDragonOnesie;
        public bool hasElephantOnesie;
        public bool hasHamsterOnesie;

        public bool PlayerCanSwitchOnesie { get; set; }

        public Animator Animator
        {
            get { return currentSkeleton.GetComponent<Animator>(); }
        }

        public bool HoldingWater
        {
            get;
            set;
        }

        public Onesie Onesie
        {
            get { return currentOnesie == null ? defaultOnesie : currentOnesie; }
            set { currentOnesie = onesies.Contains(value) ? value : currentOnesie; }
        }

        public Onesie[] Onesies
        {
            get { return onesies; }
        }

        public float StaticCharge
        {
            get { return charge; }
            set { charge = Mathf.Max(0, Mathf.Min(value, 100)); }
        }

        public void AddOnesie(Onesie onesie)
        {
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

        public void Awake()
        {
            PlayerCanSwitchOnesie = true;
            defaultOnesie = Resources.Load<Onesie>("OnesieDefault");
            onesies = new Onesie[3];
            particleSystems = GameObject.Find("Nimbi/VFX").transform.Cast<Transform>()
                .Where(t => t.GetComponent<ParticleSystem>() != null)
                .Select(t => t.gameObject).ToList();
            skeletons = GetComponentsInChildren<Skeleton>().ToList();
        }

        public bool HasOnesie(OnesieType type)
        {
            if (onesies == null)
            {
                return false;
            }

            foreach (Onesie onesie in onesies)
            {
                if (onesie != null && onesie.type == type)
                {
                    return true;
                }
            }
            return false;
        }

        public void ResetRenderers()
        {
            currentSkeleton.Renderer.enabled = true;
            skeletons.Where(s => s != currentSkeleton).ToList().ForEach(s => s.Renderer.enabled = false);
        }

        public void SetEffectActive(string name, bool active) {
            particleSystems.Single(g => g.name == name).SetActive(active);
        }

        public IEnumerator SetEffectActiveForDuration(string name, float duration)
        {
            ParticleSystem effect = particleSystems.Single(g => g.name == name).GetComponent<ParticleSystem>();
            float t = 0;

            effect.gameObject.SetActive(true);
            effect.loop = true;
            effect.Play();

            while (t < duration) {
                t += Time.deltaTime;
                yield return null;
            }

            effect.gameObject.SetActive(false);
            effect.loop = false;
            effect.Stop();
        }

        private void SetSkeleton(string name)
        {
            currentSkeleton = skeletons.Single(s => s.name == "Onesie" + name);
            currentSkeleton.GetComponent<Animator>().SetTrigger("To_" + name);
            currentSkeleton.Renderer.enabled = true;
            skeletons.Where(s => s != currentSkeleton).ToList().ForEach(s => s.Renderer.enabled = false);
        }

        public void Start()
        {
            Physics.gravity = new Vector3(0, -35, 0);
            SetSkeleton("Default");

            if (hasDragonOnesie) {
                AddOnesie(Resources.Load<Onesie>("OnesieDragon"));
            }
            if (hasElephantOnesie) {
                AddOnesie(Resources.Load<Onesie>("OnesieElephant"));
            }
            if (hasHamsterOnesie) {
                AddOnesie(Resources.Load<Onesie>("OnesieHamster"));
            }
        }

        public void SwitchOnesie(int index)
        {
            if (index > -1 && index < 3 && onesies[index] != null)
            {
                try
                {
                    currentOnesie = (currentOnesie == onesies[index]) ? defaultOnesie : onesies[index];
                    AudioManager.Instance.PlaySoundeffect(AudioManager.Instance.GetOnesieSwitchSound(currentOnesie.name));
                    SetSkeleton(currentOnesie.name.Replace("Onesie", ""));
                    if (currentOnesie.type == OnesieType.Hamster)
                        GetComponent<TrailRenderer>().enabled = true;
                    else
                        GetComponent<TrailRenderer>().enabled = false;
                }
                catch (Exception ex)
                {
                    Debug.Log("Error in Player.cs: " + ex.Message + ", \r\nTrace: " + ex.StackTrace);
                    SetSkeleton("Default");
                }
            }
        }

        public void UseOnesieSpecialAbility()
        {
            if (Onesie.type == OnesieType.Dragon) {
                StartCoroutine(SetEffectActiveForDuration("Flame Breath", 0.5f));
            }
        }
    }
}