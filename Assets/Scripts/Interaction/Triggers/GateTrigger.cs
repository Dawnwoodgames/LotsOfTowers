using UnityEngine;
using System.Collections;
using LotsOfTowers.Actors;

namespace LotsOfTowers.Interaction.Triggers
{
	public class GateTrigger : MonoBehaviour
	{
		public GameObject DownwardsObject; // Object that will be moved downwards
		private Player player;
		
		void Update()
		{
			if (DownwardsObject.transform.rotation.x != 0f)
			{
				DownwardsObject.transform.rotation = Quaternion.Slerp(DownwardsObject.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 2);
			}
		}
	}
}
