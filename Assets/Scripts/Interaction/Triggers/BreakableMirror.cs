﻿using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction.Triggers
{
    public class BreakableMirror : MonoBehaviour
    {
        public GameObject[] ropes;
        private void OnTriggerStay(Collider coll)
        {
            if (coll.tag == "Player" && coll.GetComponent<Player>().Onesie.type == OnesieType.Hamster)
                if (Input.GetButtonDown("Submit"))
                    BreakObject();
        }

        private void BreakObject()
        {
            if (!GetComponent<Rigidbody>())
            {
                gameObject.AddComponent<Rigidbody>();
            }
            
            GetComponent<Rigidbody>().AddForce(Vector3.right*0.2f,ForceMode.Impulse);
            foreach(GameObject rope in ropes)
            {
                Destroy(rope);
            }
        }
    }
}