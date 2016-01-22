using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction.Triggers{
public class biggetjeopspitscript : MonoBehaviour {

	ParticleSystem Vuurtje;
	private Player player;
	Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
		Vuurtje = gameObject.GetComponentInChildren<ParticleSystem> ();
		rigidbody = gameObject.GetComponentInChildren<Rigidbody> ();
		Vuurtje.Stop ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {


	}

	private void OnTriggerStay(Collider coll){
			if (Input.GetButtonDown("Submit") && player.Onesie.type == OnesieType.Dragon) {
			Vuurtje.Play ();
			rigidbody.useGravity = true;
		}
	}
}
}