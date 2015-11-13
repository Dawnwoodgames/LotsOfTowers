using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

    public GameObject player;
    public Vector3 unlockedPosition;
    public GameObject key;
    private bool inTrigger = false;

    void Update()
    {
        if (Input.GetButtonDown("Submit") && inTrigger)
            if (player.GetComponentInChildren<MirrorKey>() != null)
                OpenDoor(key);
    }
	
	private void OnTriggerStay(Collider coll) { inTrigger = true; }
    private void OnTriggerExit() { inTrigger = false; }

    private void OpenDoor(GameObject key)
    {
        this.gameObject.transform.localPosition = unlockedPosition;
        Destroy(key);
    }
}
