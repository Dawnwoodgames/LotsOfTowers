using UnityEngine;
using System.Collections;

namespace Nimbi.Platform
{
    public class FaceCamera : MonoBehaviour
    {
        public bool x, y, z;
        private float startX, startY, startZ;
        private float newX, newY, newZ;

        void Start()
        {
            startX = transform.rotation.eulerAngles.x;
            startY = transform.rotation.eulerAngles.y;
            startZ = transform.rotation.eulerAngles.z;
        }
        // Update is called once per frame
        void Update()
        {
            transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
            if (!x)
                newX = startX;
            else
                newX = transform.rotation.eulerAngles.x;

            if (!y)
                newY = startY;
            else
                newY = transform.rotation.eulerAngles.y;

            if (!z)
                newZ = startZ;
            else
                newZ = transform.rotation.eulerAngles.z;

            transform.rotation = Quaternion.Euler(newX, newY, newZ);
        }
    }
}