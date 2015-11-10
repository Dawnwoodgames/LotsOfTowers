using UnityEngine;

namespace LotsOfTowers.CameraControl
{
	public class CameraFollowScript : MonoBehaviour
	{
		private GameObject player;
        public float verticalOffset;

		public void Start()
		{
			this.player = GameObject.FindGameObjectWithTag("Player");
		}

		void Update()
		{
			this.transform.position = new Vector3(
				player.transform.position.x,
				player.transform.position.y+verticalOffset,
				player.transform.position.z
			);
		}
	}
}