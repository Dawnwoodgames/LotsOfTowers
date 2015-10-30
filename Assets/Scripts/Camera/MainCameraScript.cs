using UnityEngine;
using System.Collections;
using System;

namespace LotsOfTowers.Camera
{
    public class MainCameraScript : MonoBehaviour
    {

        GameObject centerObject;

        private bool _inputLocked;

        public float camUpFromPlayer = 3f;
        public float camBehindPlayer = 7f;

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
            transform.localPosition = new Vector3(transform.localPosition.x, camUpFromPlayer, -camBehindPlayer);
        }

        private void CameraInput()
        {
            centerObject.transform.Rotate(0, Input.GetAxis("RightJoystickX") * 2, 0);
            if (Input.GetKey("e"))
                centerObject.transform.Rotate(Vector3.down * 2);
            if (Input.GetKey("q"))
                centerObject.transform.Rotate(Vector3.up * 2);

            if (Input.GetKey("r"))
            {
                if (!isRotateLocked())
                {
                    centerObject.transform.Rotate(0, -90, 0);
                    LockRotate();
                }
            }
        }

        private void LockRotate()
        {
            _inputLocked = true;
            Invoke("UnlockRotate", .2f);
        }

        private void UnlockRotate()
        {
            _inputLocked = false;
        }

        private bool isRotateLocked()
        {
            return _inputLocked;
        }
    }
}