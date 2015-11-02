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
			cameraDistance = 7f;
			Debug.DrawRay(player.transform.position, transform.TransformDirection(-Vector3.forward)*7, Color.magenta);
			hits = Physics.RaycastAll(player.transform.position, transform.TransformDirection(-Vector3.forward), 7f).OrderBy(h=>h.distance).ToArray();
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
				} else if (hit.collider.tag != "Player" && !cameraMoved) {
					cameraMoved = true;
					cameraDistance = Vector3.Distance (hit.point, player.transform.position);
				}
			}
			GetComponent<LotsOfTowers.CameraControl.MainCameraScript> ().camBehindPlayer = cameraDistance;
			Vector3 rot = transform.rotation.eulerAngles;
			rot = new Vector3(Mathf.Lerp(rot.x,30f+30f*(7f-cameraDistance)/7f,Time.deltaTime*2f), rot.y, rot.z);
			transform.rotation = Quaternion.Euler(rot);
		}
	}
}