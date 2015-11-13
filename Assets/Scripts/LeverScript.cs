using UnityEngine;
using System.Collections;

public class LeverScript : MonoBehaviour {

    public GameObject pressurePlate;
    private GameObject player;
    private bool inTrigger = false;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	void Update () {
        if (Input.GetButtonDown("Submit") && inTrigger)
        {
            //Destroy(pressurePlate.GetComponent<PressurePlateScript>());
            Debug.Log(pressurePlate.name + " inactive");
        }
    }

    private void OnTriggerStay(Collider coll) { inTrigger = true; }
    private void OnTriggerExit() { inTrigger = false; }
}
