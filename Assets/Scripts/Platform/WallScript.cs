using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace LotsOfTowers.Platform
{
	public class WallScript : MonoBehaviour
	{
		public GameObject player;
		private RaycastHit[] hits = null;
		private float goalAlpha = 0.2f;
		List<GameObject> oldHits;
		private float cameraDistance;

		private void Update()
		{
			cameraDistance = 5f;
            Debug.DrawRay(player.transform.position, transform.TransformDirection(-Vector3.forward) * cameraDistance, Color.magenta);
            hits = Physics.SphereCastAll(player.transform.position, 0.5f, transform.TransformDirection(-Vector3.forward), cameraDistance * 1.1f).OrderBy(h => h.distance).ToArray();
			if (oldHits != null)
			{
				foreach (GameObject hit in oldHits)
				{
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
			bool cameraMoved = false;
			foreach (RaycastHit hit in hits)
			{
				oldHits.Add(hit.collider.gameObject);
				Renderer r = hit.collider.GetComponent<Renderer>();
				if (r && hit.collider.tag == "Wall") {
					Color color = r.material.color;
					color.a = Mathf.Lerp (color.a, goalAlpha, 0.8f * Time.deltaTime);
					r.material.color = color;
				}
			}

            hits = Physics.RaycastAll(player.transform.position, transform.TransformDirection(-Vector3.forward), cameraDistance * 1.1f).OrderBy(h => h.distance).ToArray();
            foreach (RaycastHit hit in hits)
            {
               if (hit.collider.tag != "Player" && hit.collider.tag != "Trigger" && !cameraMoved)
                {
                    cameraMoved = true;
                    cameraDistance = Vector3.Distance(hit.point, player.transform.position);
                }
            }
            if (!GetComponent<LotsOfTowers.CameraControl.MainCameraScript>().zoomedOut)
            {
                GetComponent<LotsOfTowers.CameraControl.MainCameraScript>().camBehindPlayer = cameraDistance;
                Vector3 rot = transform.rotation.eulerAngles;
                rot = new Vector3(Mathf.Lerp(rot.x, GetComponent<LotsOfTowers.CameraControl.MainCameraScript>().camRotate + 20f * (5f - cameraDistance) / 5f, Time.deltaTime * 2f), rot.y, rot.z);
                transform.rotation = Quaternion.Euler(rot);
            }
		}
	}
}