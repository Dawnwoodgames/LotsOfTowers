using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallScript : MonoBehaviour {

	public GameObject player;
	private RaycastHit[] hits = null;
	private float goalAlpha = 0.2f;
	List<GameObject> oldHits;

	private void Update() {


		Debug.DrawRay (Camera.main.transform.position, (player.transform.position - Camera.main.transform.position), Color.magenta);

		hits = Physics.RaycastAll(Camera.main.transform.position, (player.transform.position - Camera.main.transform.position), Vector3.Distance(Camera.main.transform.position, player.transform.position));
		if (oldHits != null) {
			foreach (GameObject hit in oldHits) {
				bool found = false;
				foreach (RaycastHit newHit in hits) {
					if (hit == newHit.collider.gameObject)
						found = true;
				}
				Renderer r = hit.GetComponent<Renderer>();
				if (r && !found) {
					r.material.color = Color.white;
				}
			}
		}
		oldHits = new List<GameObject>();
		foreach (RaycastHit hit in hits) {
			oldHits.Add (hit.collider.gameObject);
			Renderer r = hit.collider.GetComponent<Renderer>();
			if (r && hit.collider.tag == "Wall") {
				Color color = r.material.color;
				color.a = Mathf.Lerp(color.a,goalAlpha,0.8f*Time.deltaTime);
				r.material.color = color;
			}
		}

	}
}