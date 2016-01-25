using UnityEngine;
using System.Collections;
using Nimbi.Actors;

public class WaterDrops : MonoBehaviour {

    public GameObject[] drops;
    public Player player;

    // Update is called once per frame
    void Update () {
        if (player.HoldingWater)
        {
            foreach(GameObject go in drops)
            {
                go.GetComponent<Renderer>().enabled = true;
            }
        }
        else
        {
            foreach (GameObject go in drops)
            {
                go.GetComponent<Renderer>().enabled = false;
            }
        }
	}
}
