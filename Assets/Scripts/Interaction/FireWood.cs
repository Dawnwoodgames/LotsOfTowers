using UnityEngine;
using System.Collections;
using Nimbi.Actors;
using System.Linq;


public class FireWood : MonoBehaviour {

    public GameObject particle;

    private Player player;
    private bool hasFireContact;
    private bool isTrigger;

    

// Use this for initialization
    void Start () {
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    hasFireContact = false;
    isTrigger = false;
    }
	
	// Update is called once per frame
	void Update () {
	 if(hasFireContact == true)
        {
            print("I am on fire Baby!");
            particle.SetActive(true);

        }
	}

    void Ignite()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Fire")
        {
            hasFireContact = true;
            Ignite();
        }
    }





}
