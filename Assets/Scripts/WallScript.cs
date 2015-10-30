using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {

	GameObject player;
	private RaycastHit[] hits = null;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("ThirdPersonController");
	}
	
	// Update is called once per frame
	private void Update() {
		if (hits != null) {
			foreach (RaycastHit hit in hits) {
				Renderer r = hit.collider.GetComponent<Renderer>();
				if (r) {
					r.material.color = Color.white;
				}
			}
		}

		Debug.DrawRay (Camera.main.transform.position, (player.transform.position - Camera.main.transform.position), Color.magenta);

		hits = Physics.RaycastAll(Camera.main.transform.position, (player.transform.position - Camera.main.transform.position), Vector3.Distance(Camera.main.transform.position, player.transform.position));

		foreach (RaycastHit hit in hits) {
			Renderer r = hit.collider.GetComponent<Renderer>();
			if (r && hit.collider.tag == "Wall") {
				Color color = r.material.color;
				color.a = 0.2f;
				r.material.color = color;
			}
		}

	}
}