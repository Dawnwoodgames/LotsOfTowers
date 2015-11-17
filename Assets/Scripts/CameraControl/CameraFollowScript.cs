using UnityEngine;

namespace LotsOfTowers.CameraControl
{
	public class CameraFollowScript : MonoBehaviour
    {
        private Transform startTransform;
        private GameObject player;

		public void Start()
        {
            startTransform = gameObject.transform;
            player = GameObject.FindGameObjectWithTag("Player");
		}

		void Update()
		{
            gameObject.transform.position = new Vector3(player.transform.position.x / 1.2f, player.transform.position.y + 0.5f, player.transform.position.z / 1.2f);
        }

        public Transform getStartTransform() { return startTransform; }
    }
}