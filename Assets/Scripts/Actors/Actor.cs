using System;
using System.Collections.Generic;
using UnityEngine;

namespace LotsOfTowers.Actors {
	public sealed class Actor : MonoBehaviour {

		public GameObject tooltip;

		// Private fields
		private Onesie onesie;
		private List<Onesie> onesies;

		// Properties
		public int JumpCount {
			get { return onesie.jumpCount; }
		}

		public Onesie[] Onesies {
			get { return onesies.ToArray(); }
		}

		// Methods
		public void Awake() {
			this.onesie = Onesie.Load("Default");
			this.onesies = new List<Onesie>(new Onesie[] { onesie });
		}

		public void Equip(Onesie onesie) {
			if (onesies.Contains(onesie)) {
				this.onesie = onesie;
			}
		}

		public void Respawn() {
			try {
				transform.position = GameManager.Instance.SpawnPoint.position;
			} catch (NullReferenceException) { }
		}

		public void Start() {
			Respawn();
			Tooltip.ShowTooltip (tooltip, "Movement",false,new string[]{"Horizontal", "Vertical"});
		}
	}
}