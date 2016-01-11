using UnityEngine;
using Nimbi.Framework;
using System.Linq;

namespace Nimbi.Platform
{
	public class OffScreenCollider : MonoBehaviour
	{
		public GameObject[] resetObjects; 
		
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

			else
			{
				collision.transform.localPosition = resetObjects.FirstOrDefault(go => go.GetInstanceID() == collision.gameObject.GetInstanceID()).transform.localPosition;
			}
		}
	}
}
