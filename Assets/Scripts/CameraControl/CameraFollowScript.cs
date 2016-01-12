using UnityEngine;

namespace Nimbi.CameraControl
{
	public class CameraFollowScript : MonoBehaviour
    {
        private GameObject cameraObject;
        private Transform startTransform;
		private GameObject focus;

		public void Start()
        {
            cameraObject = GetComponentInChildren<Camera>().gameObject;
            startTransform = gameObject.transform;
            focus = GameObject.FindGameObjectWithTag("Player");
		}

		void Update()
		{

            transform.position = new Vector3(focus.transform.position.x, focus.transform.position.y + 0.5f, focus.transform.position.z);
        }

        public Transform getStartTransform() { return startTransform; }

		public void SetCameraFocus(GameObject _focus) { focus = _focus; }
	}
}