using UnityEngine;

namespace LotsOfTowers.CameraControl
{
	public class CameraFollowScript : MonoBehaviour
	{
		private GameObject player;

		public void Start()
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}

		void Update()
		{
            gameObject.transform.position = new Vector3(player.transform.position.x / 1.5f, player.transform.position.y + 3, player.transform.position.z / 1.5f);
		}
	}
}