﻿using UnityEngine;
using System.Collections;
using System;

namespace LotsOfTowers.CameraControl
{
    public class MainCameraScript : MonoBehaviour
    {
		public static readonly float MouseSensitivity = 8;

        private GameObject centerObject;
        private Vector3 playerPosition;

        public float camBehindPlayer = 7f;
        public float camUpFromPlayer = 3f;
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
			    transform.localPosition = new Vector3(transform.localPosition.x, camUpFromPlayer, Mathf.Lerp(transform.localPosition.z,-camBehindPlayer, Time.deltaTime*2f));
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
                zoomedOut = true;
                gameObject.transform.localPosition = new Vector3(0, 4, gameObject.transform.position.z - 40);
            }
            if (Input.GetButtonUp("CameraOverview"))
            {
                zoomedOut = false;
                playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
                gameObject.transform.localPosition = new Vector3(0, 4, -5);
            }
        }
    }
}