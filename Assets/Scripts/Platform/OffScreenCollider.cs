using UnityEngine;

namespace LotsOfTowers.Platform
{
	public class OffScreenCollider : MonoBehaviour
	{
		public Transform spawnPoint;

		void OnCollisionEnter(Collision collision)
		{
			if(collision.gameObject.tag == "Player")
			{
				try
				{
					//Player fell off the stage, reset him to the spawn point
					collision.gameObject.transform.position = spawnPoint.position;
				}
				catch (System.Exception)
				{
					throw;
				}
			}
		}
	}
}
