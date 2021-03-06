﻿using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{
    public class CypherPuzzleChecker : MonoBehaviour
    {
        public GameObject oven, furnaceDoor;
        public bool cheat = false;

        private bool slotOneComplete, slotTwoComplete, slotThreeComplete = false;

        void Update()
        {
            if ((slotOneComplete && slotTwoComplete && slotThreeComplete) || cheat)
            {
                oven.GetComponent<ParticleSystem>().Play();
                furnaceDoor.GetComponent<Interaction.Triggers.FurnaceDoor>().cypherComplete = true;
            }
        }

        void OnTriggerEnter(Collider coll)
        {
            if (coll.name == "slot_1_complete")
                slotOneComplete = true;

            if (coll.name == "slot_2_complete")
                slotTwoComplete = true;

            if (coll.name == "slot_3_complete")
                slotThreeComplete = true;
        }

        void OnTriggerExit(Collider coll)
        {
            if (coll.name == "slot_1_complete")
                slotOneComplete = false;

            if (coll.name == "slot_2_complete")
                slotTwoComplete = false;

            if (coll.name == "slot_3_complete")
                slotThreeComplete = false;
        }
    }
}