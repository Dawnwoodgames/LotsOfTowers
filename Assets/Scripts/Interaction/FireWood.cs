using UnityEngine;
using System.Collections;
using Nimbi.Actors;
using System.Linq;

namespace Nimbi.Interaction
{
    public class FireWood : MonoBehaviour {

    public GameObject particle;
    public GameObject water;
    public GameObject cloudToSpawn;


    public GameObject BoilerLid;

    private Player player;
    private bool hasFireContact;
    private bool isTrigger;
    private bool boilingWater;

    
// Use this for initialization
    void Start () {
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    hasFireContact = false;
    isTrigger = false;
    boilingWater = false;
    }
	
	// Update is called once per frame
	void Update () {
	 if(hasFireContact == true)
        {
            print("I am on fire Baby!");
            particle.SetActive(true);
            boilingWater = true;
            InvokeRepeating("SpawnCloud", 0.5f, 5);
            hasFireContact = false;
        }
	}

    void SpawnCloud()
    {   
           Instantiate(cloudToSpawn, new Vector3(-8, 19.95f, -39), Quaternion.identity);  
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Fire")
        {
            hasFireContact = true;
            
        }
    }
}
}