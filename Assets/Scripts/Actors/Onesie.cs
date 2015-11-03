using UnityEngine;

namespace LotsOfTowers.Actors
{
	public class Onesie : ScriptableObject
	{
		/*
			Every public field will show up in the editor when configuring onesies,
			default values for each field can be set below
		 */

		public bool canMoveObjects = false;
		public int jumpCount = 1;
		public float movementSpeed = 1;
		public float jumpPower = 5;

		public static Onesie Load(string name)
		{
			return Resources.Load("Onesie" + name) as Onesie;
		}
	}
}