using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
    public class InfoCameraTutorial : MonoBehaviour
    {
        private Transform camera;

        // Use this for initialization
        void Start()
        {
            camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(camera);
        }
    }
}