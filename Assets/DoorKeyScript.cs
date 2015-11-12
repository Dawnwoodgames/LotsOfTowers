using UnityEngine;
using System.Collections;

public class DoorKeyScript : MonoBehaviour {

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}

    private void OnTriggerEnter(Collider coll)
    {
        this.gameObject.transform.SetParent(player.transform);
        this.gameObject.transform.localPosition = new Vector3(0, .08f, 0);
    }
}
