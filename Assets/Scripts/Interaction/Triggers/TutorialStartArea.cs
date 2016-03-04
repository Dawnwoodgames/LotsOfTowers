using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
    public class TutorialStartArea : MonoBehaviour
    {

        private Nimbi.CameraControl.MainCameraScript camera;

        void Start()
        {
            camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl.MainCameraScript>();
            camera.cameraEnabled = false;
        }
    }
}