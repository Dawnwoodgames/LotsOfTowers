using UnityEngine;
using System.Collections;

public class MirrorDoor : MonoBehaviour {

    private bool inTrigger = false;
    private bool disappearing;
    private Renderer mirrorRenderer;

    void Start()
    {
        mirrorRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if(inTrigger && Input.GetButton("Submit"))
        {
            disappearing = true;
        }

        if(disappearing)
        {
            Color color = mirrorRenderer.material.color;
            color.a -= 0.1f*Time.deltaTime;
            mirrorRenderer.material.color = color;
            if (color.a <= 0.01)
                Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        inTrigger = true;
    }

    void OnTriggerExit(Collider coll)
    {
        inTrigger = false;
    }
}
