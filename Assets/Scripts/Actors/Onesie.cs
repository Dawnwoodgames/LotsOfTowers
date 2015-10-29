using UnityEngine;

namespace LotsOfTowers.Actors {
	public sealed class Onesie : ScriptableObject {

		/*
			Every public field will show up in the editor when configuring onesies,
			default values for each field can be set below
		 */

		public int jumpCount = 1;

		public static Onesie Load(string name) {
			return Resources.Load("Onesie" + name) as Onesie;
		}
	}
}