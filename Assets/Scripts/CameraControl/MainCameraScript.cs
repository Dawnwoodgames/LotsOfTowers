using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace LotsOfTowers.CameraControl
{
    public class MainCameraScript : MonoBehaviour
    {
		public static readonly float MouseSensitivity = 3;

        

        public float camRotate = 20f;
        public bool zoomedOut;
        public float maxCameraDistance;
        public float verticalRatio;

        private float currentCameraDistance;
        private GameObject centerObject;
        private Vector3 playerPosition;
        private RaycastHit[] hits = null;
        private float goalAlpha = 0.2f;
        private List<GameObject> oldHits;
        private Vector3 rayDirection;

        void Start()
        {
            centerObject = GameObject.Find("CenterFocus");
            currentCameraDistance = maxCameraDistance;
        }

        void Update()
        {
            CameraInput();
            InvisibleWalls();
            WallCollision();
            CameraPosition();
        }

        private void CameraPosition()
		{
            if (!zoomedOut)
            {
                float cameraPosition;
                if (-transform.localPosition.z > currentCameraDistance)
                    cameraPosition = -currentCameraDistance;
                else
                    cameraPosition = Mathf.Lerp(transform.localPosition.z, -currentCameraDistance, Time.deltaTime * 2f);

                Debug.Log(cameraPosition);
                transform.localPosition = new Vector3(transform.localPosition.x, -cameraPosition * verticalRatio, cameraPosition);
            }
        }

        private void CameraInput()
        {
			if (Input.GetMouseButton (1) && Input.GetAxis("Mouse X") != 0) {
				// Player is dragging mouse (right button)
				centerObject.transform.Rotate(0, Input.GetAxis("Mouse X") * MouseSensitivity, 0);
			} else {
				centerObject.transform.Rotate (0, Input.GetAxis("CameraRotate"), 0);
			}

            if (Input.GetButtonDown("CameraOverview"))
            {
                Debug.Log(gameObject.transform.rotation);
                zoomedOut = true;
                gameObject.transform.localRotation = new Quaternion(0, 0, 0, 1);
                gameObject.transform.localPosition = new Vector3(0, 40f*verticalRatio, -40);
            }
            if (Input.GetButtonUp("CameraOverview"))
            {
                zoomedOut = false;
                transform.localRotation = new Quaternion(0.2f, 0, 0, 1);
            }
        }

        private void InvisibleWalls()
        {
            rayDirection = transform.TransformDirection(Quaternion.AngleAxis(camRotate, new Vector3(-1, 0, 0)) * new Vector3(0, verticalRatio, -1));
            Debug.DrawRay(centerObject.transform.position,  rayDirection*maxCameraDistance, Color.magenta);

            hits = Physics.SphereCastAll(centerObject.transform.position, 0.5f, rayDirection, maxCameraDistance * 1.1f);
            if (oldHits != null)
            {
                foreach (GameObject hit in oldHits)
                {
                    if (hit == null)
                    {
                        continue;
                    }

                    bool found = false;
                    foreach (RaycastHit newHit in hits)
                    {
                        if (hit == newHit.collider.gameObject)
                            found = true;
                    }
                    Renderer r = hit.GetComponent<Renderer>();
                    if (r && !found)
                    {
                        r.material.color = Color.white;
                    }

                }
            }
            oldHits = new List<GameObject>();
            foreach (RaycastHit hit in hits)
            {
                oldHits.Add(hit.collider.gameObject);
                Renderer r = hit.collider.GetComponent<Renderer>();
                if (r && hit.collider.tag == "Wall")
                {
                    Color color = r.material.color;
                    color.a = Mathf.Lerp(color.a, goalAlpha, 0.8f * Time.deltaTime);
                    r.material.color = color;
                }
            }
        }

        private void WallCollision()
        {
            bool cameraMoved = false;
            float cameraDistance = maxCameraDistance;
            hits = Physics.RaycastAll(centerObject.transform.position, rayDirection, maxCameraDistance * 1.1f).OrderBy(h => h.distance).ToArray();
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.tag != "Player" && hit.collider.tag != "Trigger" && hit.collider.tag != "Wall" && !cameraMoved)
                {
                    cameraMoved = true;
                    cameraDistance = hit.distance;
                    Debug.Log(cameraDistance);
                }
            }
            if (!zoomedOut)
            {
                currentCameraDistance = cameraDistance;
            }
        }
    }
}