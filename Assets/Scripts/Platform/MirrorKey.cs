using UnityEngine;
using System.Collections;

public class MirrorKey : MonoBehaviour {

    public GameObject player;
    public GameObject mirrorPlayer;
    public GameObject mirror;

    private bool pickedUp = false;
    private bool currentlyVisible = true;

	void Update () {
        if (pickedUp)
            return;
	    if(!SameSideAs(mirrorPlayer))
        {
            float oldY = transform.position.y;
            Vector3 newPosition = mirror.transform.position - (transform.position - mirror.transform.position);
            newPosition.y = oldY;
            transform.position = newPosition;
        }
        if (!CameraThroughMirror() && currentlyVisible)
        {
            GetComponent<Renderer>().enabled = false;
            currentlyVisible = false;
        }
        else if (CameraThroughMirror() && !currentlyVisible)
        {
            GetComponent<Renderer>().enabled = true;
            currentlyVisible = true;
        }
    }
    //Check if the object is on the same side of the mirror as the key
    private bool SameSideAs(GameObject go)
    {
        bool sameSide = true;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, go.transform.position + new Vector3(0, 1, 0) - transform.position, Vector3.Distance(transform.position,go.transform.position));
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag == "Mirror")
            {
                sameSide = false;
            }

        }
        return sameSide;
    }

    private bool CameraThroughMirror()
    {
        bool throughMirror = false;
        RaycastHit[] hits = Physics.RaycastAll(transform.position + new Vector3(0, 1, 0), Camera.main.transform.position - mirrorPlayer.transform.position, 20);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag == "Mirror")
            {
                throughMirror = true;
            }

        }
        return throughMirror;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject == mirrorPlayer)
        {
            this.gameObject.transform.SetParent(mirrorPlayer.transform);
            this.gameObject.transform.localPosition = new Vector3(0, .08f, 0);
            pickedUp = true;
        }
    }
}
