﻿using UnityEngine;
using Nimbi.Actors;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
    public class MarbleTrackBlockEntranceTrigger : MonoBehaviour
    {
        private Rigidbody rb;

        void Start()
        {
            rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player" && coll.GetComponent<Player>().Onesie.type != OnesieType.Hamster)
                rb.AddForce(transform.forward * 10, ForceMode.Impulse);
        }
    }
}