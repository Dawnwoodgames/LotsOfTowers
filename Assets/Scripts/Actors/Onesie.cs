using UnityEngine;

namespace LotsOfTowers.Actors {
	public class Onesie : ScriptableObject {

		/*
			Every public field will show up in the editor when configuring onesies,
			default values for each field can be set below
		 */

		public bool canMoveObjects = false;
		public bool isHeavy = false;
		public int jumpCount = 0;
		public float movementSpeed = 5;
		public float jumpPower = 10;
		public OnesieType type = OnesieType.Human;

		public static Onesie Load(string name) {
			return Resources.Load("Onesie" + name) as Onesie;
		}
	}

	public enum OnesieType {
		Human,

		Elephant,
		Hamster
	}
}