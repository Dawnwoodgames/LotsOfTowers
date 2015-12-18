using UnityEngine;
using System.Collections;

public class Captain : MonoBehaviour {

    private Nut nut;
    private bool nutDelivered = false;
    private Vector3 endMarker;
    private float speed = 1f;
    private float startTime;
    private float journeyLength;

	void Start () {
        endMarker = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z);
        nut = GameObject.Find("Nut").GetComponent<Nut>();
        journeyLength = Vector3.Distance(transform.position, endMarker);
	}
	
	void Update () {
        if (nutDelivered)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(transform.position, endMarker, fracJourney);
        }
	
	}

    private void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "Player" && Input.GetButtonDown("Submit"))
        {
            if (nut.pickedUp)
            {
                nutDelivered = true;
                startTime = Time.time;
            }
        }
    }
}
