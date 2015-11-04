using UnityEngine;
using System.Collections;

public class ChickenTrigger : MonoBehaviour {

    public GameObject tooltipCanvas;
    private bool alreadyShown = false;

	void Start () {
        tooltipCanvas.SetActive(false);
	}

    void OnTriggerEnter(Collider collider) {
        if (!alreadyShown && collider.tag == "Player")
        {
            tooltipCanvas.SetActive(true);
            alreadyShown = true;
        }

    }
}