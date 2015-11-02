using UnityEngine;
using System.Collections;
using System;

namespace LotsOfTowers.CameraControl
{
    public class MainCameraScript : MonoBehaviour
    {
        GameObject centerObject;
        Vector3 playerPosition;

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

            if (Input.GetButtonDown("CameraOverview"))
            {
                gameObject.transform.localPosition = new Vector3(0, 4, gameObject.transform.position.z - 40);
            }
            if (Input.GetButtonUp("CameraOverview"))
            {
                playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
                gameObject.transform.localPosition = new Vector3(0, 4, -5);
            }
        }
    }
}