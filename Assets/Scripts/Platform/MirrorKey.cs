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
            Vector3 hitTarget = mirror.transform.position - player.transform.position;
            hitTarget.y = 1;
            RaycastHit[] hits = Physics.RaycastAll(player.transform.position + Vector3.up, hitTarget, 20);
            Vector3 mirrorNormal = new Vector3(0,0,0);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.tag == "Mirror")
                {
                    mirrorNormal = hit.normal;
                }
            }
            float oldY = transform.position.y;
            Vector3 newPosition = mirror.transform.position + Vector3.Reflect(transform.position - mirror.transform.position, mirrorNormal);
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
        bool throughMirror = true;
        for (int hor = 0;hor <= 1;hor++)
            for(int vert = 0;vert<=1;vert++)
            {
                if (!throughMirror)
                    continue;
                bool mirrorfound = false;
                RaycastHit[] hits = Physics.RaycastAll(transform.position, Camera.main.ViewportToWorldPoint(new Vector3(hor, vert, Camera.main.nearClipPlane)) - transform.position, 20);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.tag == "Mirror")
                    {
                        mirrorfound = true;
                    }

                }
                throughMirror = mirrorfound;
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