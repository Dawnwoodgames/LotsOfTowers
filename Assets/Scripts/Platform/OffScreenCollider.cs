using UnityEngine;
using Nimbi.Framework;

namespace Nimbi.Platform
{
	public class OffScreenCollider : MonoBehaviour
	{
		void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.tag == "Player")
			{
				try
				{
					//Player fell off the stage, reset him to the spawn point
					GameManager.Instance.PlayerPassOutAndRespawn(GameManager.Instance.SpawnPoint);
                }
				catch (System.Exception)
				{
					throw;
				}
			}
		}
	}
}
