using UnityEngine;
using System.Collections.Generic;

namespace LotsOfTowers.Actors {
	public sealed class Actor : MonoBehaviour {
		// Private fields
		private Onesie onesie;
		private List<Onesie> onesies;

		// Properties
		public int JumpCount {
			get { return onesie.jumpCount; }
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
	}
}