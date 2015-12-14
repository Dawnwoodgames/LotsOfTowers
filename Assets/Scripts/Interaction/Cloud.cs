using UnityEngine;
using System.Collections;
using LotsOfTowers.Actors;

public class Cloud : MonoBehaviour {

    private bool inCloud = false;
    private GameObject player;

    void Update()
    {
        if (inCloud && Input.GetButton("Submit") && player.GetComponent<Player>().Onesie.isHeavy)
            Slurp();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            inCloud = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            inCloud = false;
    }

    void Slurp()
    {
        if (!player.GetComponent<Player>().holdingWater)
        {
            player.GetComponent<Player>().holdingWater = true;
            Destroy(gameObject);
        }
    }
}
