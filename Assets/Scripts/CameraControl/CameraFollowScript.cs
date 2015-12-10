using UnityEngine;

namespace LotsOfTowers.CameraControl
{
	public class CameraFollowScript : MonoBehaviour
    {
		public float distanceAwayFromCenter = 1.2f;

        private Transform startTransform;
        private GameObject player;

		public void Start()
        {
            startTransform = gameObject.transform;
            player = GameObject.FindGameObjectWithTag("Player");
		}

		void Update()
		{
            gameObject.transform.position = new Vector3(player.transform.position.x / distanceAwayFromCenter, player.transform.position.y + 0.5f, player.transform.position.z / distanceAwayFromCenter);
        }

        public Transform getStartTransform() { return startTransform; }
    }
}