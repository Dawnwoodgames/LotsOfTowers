﻿using UnityEngine;
using System.Collections;
using LotsOfTowers.Actors;

public class HamsterWater : MonoBehaviour {

    private bool nearWater;
    private GameObject player;
    private Vector3 defaultPosition;
    public int spitcount = 0;

    void Start()
    {
        defaultPosition = transform.localPosition;
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, 0, gameObject.transform.localScale.z);
    }

    void Update()
    {
        if (nearWater && Input.GetButton("Submit") && player.GetComponent<Player>().Onesie.isHeavy)
            Spit();
        gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale,new Vector3(gameObject.transform.localScale.x, spitcount*0.1f, gameObject.transform.localScale.z),Time.deltaTime*2);
        gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, new Vector3(gameObject.transform.localPosition.x, defaultPosition.y+spitcount*0.1f, gameObject.transform.localPosition.z), Time.deltaTime * 2);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            nearWater = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            nearWater = false;
    }

    void Spit()
    {
        if (player.GetComponent<Player>().holdingWater)
        {
            player.GetComponent<Player>().holdingWater = false;
            spitcount++;
        }
    }
}