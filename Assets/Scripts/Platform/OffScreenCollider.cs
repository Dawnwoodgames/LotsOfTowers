using UnityEngine;
using LotsOfTowers.Framework;

namespace LotsOfTowers.Platform
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
					collision.gameObject.transform.position = GameManager.Instance.SpawnPoint.position;

					//Reset the camera rotation
					GameObject.FindGameObjectWithTag("MainCamera").transform.parent.localEulerAngles = new Vector3();
				}
				catch (System.Exception)
				{
					throw;
				}
			}
		}
	}
}
