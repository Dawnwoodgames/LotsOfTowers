using UnityEngine;
using Nimbi.Framework;
using System.Linq;
using System.Collections.Generic;

namespace Nimbi.Platform
{
	public class OffScreenCollider : MonoBehaviour
	{
		public Transform[] resetObjects;
		Dictionary<int, Vector3> startPositions = new Dictionary<int, Vector3>();

		void STart()
		{
			foreach (Transform go in resetObjects)
			{
				startPositions.Add(go.GetInstanceID(), go.localPosition);
			}
		}

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
				collision.transform.localPosition = startPositions[collision.gameObject.GetInstanceID()];
                    //resetObjects.FirstOrDefault(go => go.GetInstanceID() == collision.gameObject.GetInstanceID()).transform.localPosition;
			}
		}
	}
}
