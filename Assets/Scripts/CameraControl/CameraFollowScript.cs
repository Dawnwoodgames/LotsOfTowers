using UnityEngine;

namespace LotsOfTowers.CameraControl
{
	public class CameraFollowScript : MonoBehaviour
	{
		private GameObject player;

		public void Start()
		{
			this.player = GameObject.FindGameObjectWithTag("Player");
		}

		void Update()
		{
			this.transform.position = new Vector3(
				player.transform.position.x,
				player.transform.position.y,
				player.transform.position.z
			);
		}
	}
}