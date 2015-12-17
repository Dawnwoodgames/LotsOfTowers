﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LotsOfTowers.Actors {
	public sealed class Player : MonoBehaviour {
		private float charge;
		private Onesie currentOnesie;
		private GameObject currentSkeleton;
		private Onesie defaultOnesie;
		private Onesie[] onesies;
		private List<GameObject> particleSystems;
		private List<GameObject> skeletons;

		public bool HasFreeSlots {
			get { return onesies[0] == null || onesies[1] == null || onesies[2] == null; }
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

		public Onesie AddOnesie(int index, Onesie onesie) {
			if (index > -1 && index < 3 && onesies.Where(o => o.name == onesie.name).Count() == 0) {
				Onesie replacedOnesie = onesies.ElementAtOrDefault(index);

				currentOnesie = currentOnesie == replacedOnesie ? onesie : currentOnesie;
				onesies[index] = onesie;

				return replacedOnesie;
			}

			return null;
		}

		public bool AddOnesieToFirstFreeSlot(Onesie onesie) {
			for (int i = 0; i < onesies.Length; i++) {
				if (onesies[i] != null) {
					continue;
				}
				onesies[i] = onesie;

				return true;
			}

			return false;
		}

		public void Awake() {
			this.defaultOnesie = Resources.Load<Onesie>("OnesieDefault");
			this.onesies = new Onesie[3];
			this.particleSystems = GameObject.Find("Nimbi/SFX").transform.Cast<Transform>()
				.Where(t => t.GetComponent<ParticleSystem>() != null)
				.Select(t => t.gameObject).ToList();
			this.skeletons = transform.Cast<Transform>()
				.Where(t => t.GetComponent<Animator>() != null)
				.Select(t => t.gameObject).ToList();

			Physics.gravity = new Vector3(0, -35, 0);
			SetSkeleton("Default");
		}

		public void SetEffectActive(string name, bool active) {
			particleSystems.Single(g => g.name == name).SetActive(active);
		}

		private void SetSkeleton(string name) {
			currentSkeleton = skeletons.Single(g => g.name == name);
		}

		public void SwitchOnesie(int index) {
			if (index > -1 && index < 3 && onesies[index] != null) {
				currentOnesie = (currentOnesie == onesies[index]) ? defaultOnesie : onesies[index];
			}
		}

		public void Update() {
			SetEffectActive("Drops", HoldingWater);
			SetEffectActive("Sparks", StaticCharge > 90);
		}
	}
}