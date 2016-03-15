using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
    public class TutorialStartArea : MonoBehaviour
    {

        private Nimbi.CameraControl.MainCameraScript maincamera;

        void Start()
        {
            maincamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl.MainCameraScript>();
            maincamera.cameraEnabled = false;
        }
    }
}