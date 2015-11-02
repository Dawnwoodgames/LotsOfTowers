using UnityEngine;
using System.Collections;
using System;

namespace LotsOfTowers.Camera
{
    public class MainCameraScript : MonoBehaviour
    {
        GameObject centerObject;

        void Start()
        {
            centerObject = GameObject.Find("CenterFocus");
        }

        void Update()
        {
            CameraInput();
        }

        private void CameraInput()
        {
            centerObject.transform.Rotate(0, Input.GetAxis("CameraRotate") * 2, 0);
        }
    }
}
