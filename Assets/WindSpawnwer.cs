using UnityEngine;
using System.Collections;

public class WindSpawnwer : MonoBehaviour {

    public GameObject WindParticle;
    public Vector3 direction;
    public float spawnRate; //Time between spawns

    float nextSpawn;
	// Use this for initialization
	void Start () {
        nextSpawn = Time.time + spawnRate;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            GameObject wind = Instantiate(WindParticle) as GameObject;
            wind.GetComponent<WindParticle>().direction = direction;
        }
	}
}
