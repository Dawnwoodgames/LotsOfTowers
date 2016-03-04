using UnityEngine;
using System.Collections;
using Nimbi.CameraControl;

namespace Nimbi.CameraControl
{
    public class CameraController : MonoBehaviour
    {
        //Create an Array for our Cameras
        public Camera[] cameras;
        private int currentCameraIndex;
        public bool cameraHasSwitched = false;

        // Use this for initialization
        void Start()
        {
            currentCameraIndex = 0;

            //Turn all cameras off, except the first default one
            for (int i = 1; i < cameras.Length; i++)
            {
                cameras[i].gameObject.SetActive(false);
            }

            //If any cameras were added to the controller, enable the first one
            if (cameras.Length > 0)
            {
                cameras[0].gameObject.SetActive(true);
                Debug.Log("Main Camera is now Enabled!");
            }
        }

        // Update is called once per frame
        void Update()
        {
        }


        public void ChangeCamera()
        {
            currentCameraIndex++;
            if (currentCameraIndex < cameras.Length && !cameraHasSwitched)
            {
                cameras[currentCameraIndex - 1].gameObject.SetActive(false);
                cameras[currentCameraIndex].gameObject.SetActive(true);
            }
        }

        public void DeactivateCamera()
        {
           cameras[1].gameObject.SetActive(false);
           currentCameraIndex = 0;
           cameras[currentCameraIndex].gameObject.SetActive(true);
        }
    }
}



