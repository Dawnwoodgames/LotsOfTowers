﻿using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction
{
    public class Cloud : MonoBehaviour
    {

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
            if (!player.GetComponent<Player>().HoldingWater)
            {
                player.GetComponent<Player>().HoldingWater = true;
                Destroy(gameObject);
            }
        }
    }
}