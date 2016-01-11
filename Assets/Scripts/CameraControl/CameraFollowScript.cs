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
            Debug.Log(cameraObject.transform.localPosition.z);
            if (Input.GetAxis("Mouse Wheel") != 0) {
                cameraObject.transform.localPosition = new Vector3(0, 0, Mathf.Max(-15,
                    Mathf.Min(cameraObject.transform.localPosition.z + Input.GetAxis("Mouse Wheel"), -5)));
            }

            transform.position = new Vector3(focus.transform.position.x, focus.transform.position.y + 0.5f, focus.transform.position.z);
        }

        public Transform getStartTransform() { return startTransform; }

		public void SetCameraFocus(GameObject _focus) { focus = _focus; }
	}
}