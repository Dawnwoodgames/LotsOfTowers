using UnityEngine;
using System.Collections;
using System;

namespace LotsOfTowers.CameraControl
{
    public class MainCameraScript : MonoBehaviour
    {
		public static readonly float MouseSensitivity = 8;

        private GameObject centerObject;
        private Vector3 playerPosition;

        public float camBehindPlayer = 5f;
        public float camUpFromPlayer = 3f;
        public float camRotate = 20f;
        public bool zoomedOut;

        void Start()
        {
            centerObject = GameObject.Find("CenterFocus");
        }

        void Update()
        {
            CameraInput();
            CameraDepth();
        }

        private void CameraDepth()
		{
            if (!zoomedOut)
                transform.localPosition = new Vector3(transform.localPosition.x, camUpFromPlayer, Mathf.Lerp(transform.localPosition.z, -camBehindPlayer, Time.deltaTime * 2f));
		}

        private void CameraInput()
        {
			if (Input.GetMouseButton (1) && Input.GetAxis("Mouse X") != 0) {
				// Player is dragging mouse (right button)
				centerObject.transform.Rotate(0, Input.GetAxis("Mouse X") * MouseSensitivity, 0);
			} else {
				centerObject.transform.Rotate (0, Input.GetAxis("CameraRotate") * 2, 0);
			}

            if (Input.GetButtonDown("CameraOverview"))
            {
                Debug.Log(gameObject.transform.rotation);
                zoomedOut = true;
                gameObject.transform.localRotation = new Quaternion(0, 0, 0, 1);
                gameObject.transform.localPosition = new Vector3(0, camUpFromPlayer, -40);
            }
            if (Input.GetButtonUp("CameraOverview"))
            {
                zoomedOut = false;
                gameObject.transform.localRotation = new Quaternion(Mathf.Lerp(0, 0.2f, Time.deltaTime * 2f), 0, 0, 1);
                transform.localPosition = new Vector3(transform.localPosition.x, camUpFromPlayer, Mathf.Lerp(transform.localPosition.z, -camBehindPlayer, Time.deltaTime * 2f));
            }
        }
    }
}