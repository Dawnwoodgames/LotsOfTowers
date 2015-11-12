using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

    private GameObject player;
    public Vector3 unlockedPosition;
    private bool inTrigger = false;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}

    void Update()
    {
        if (Input.GetButtonDown("Submit"))
            if (player.GetComponentInChildren<DoorKeyScript>() != null)
                OpenDoor(player.GetComponentInChildren<DoorKeyScript>().gameObject);
    }
	
	private void OnTriggerStay(Collider coll) { inTrigger = true; }
    private void OnTriggerExit() { inTrigger = false; }

    private void OpenDoor(GameObject key)
    {
        this.gameObject.transform.localPosition = unlockedPosition;
        Destroy(key);
    }
}
