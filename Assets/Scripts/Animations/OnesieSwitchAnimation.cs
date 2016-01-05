using UnityEngine;

namespace LotsOfTowers.Animations
{
	public class OnesieSwitchAnimation : MonoBehaviour
	{
		private static Animator animator;

		// Use this for initialization
		void Start()
		{
			try
			{
				animator = GetComponent<Animator>();
			}
			catch (System.Exception)
			{
				throw;
			}
		}

		public static void Play()
		{
			//animator.SetBool("isSwitching", true);
		}

		public static void Stop()
		{
			//animator.SetBool("isSwitching", false);
		}
	}
}
