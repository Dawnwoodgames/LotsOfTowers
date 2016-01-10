using UnityEngine;

namespace Nimbi.CameraControl
{
	public class CameraFollowScript : MonoBehaviour
    {
        private Transform startTransform;
		private GameObject focus;

		public void Start()
        {
            startTransform = gameObject.transform;
            focus = GameObject.FindGameObjectWithTag("Player");
		}

		void Update()
		{
            gameObject.transform.position = new Vector3(focus.transform.position.x / 1.2f, focus.transform.position.y + 0.5f, focus.transform.position.z / 1.2f);
        }

        public Transform getStartTransform() { return startTransform; }

		public void SetCameraFocus(GameObject _focus) { focus = _focus; }
	}
}