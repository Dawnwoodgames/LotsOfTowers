using UnityEngine;
using System.Collections;
using Nimbi.Actors;

public class Actordisable : MonoBehaviour {

	private PlayerController Playercontroller;

	float timeLeft = 15f;
	// Use this for initialization
	void Start () {
		Playercontroller = FindObjectOfType<PlayerController> ();
		Playercontroller.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		timeLeft -= Time.deltaTime;
		if ( timeLeft < 0 )
		{
			Playercontroller.enabled = true;
		}
	}
}