using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction.Triggers
{
    class SeesawLibraBoardTrigger : MonoBehaviour
    {
        private bool playerOnTrigger = false;
        private bool elephantOnTrigger = false;

        void OnTriggerEnter(Collider col)
        {
            if (col.tag == "Player")
            {
                playerOnTrigger = true;
            }
            if (col.name == "Elephant")
            {
                elephantOnTrigger = true;
            }
        }

        void OnTriggerExit(Collider col)
        {
            if (col.tag == "Player")
            {
                playerOnTrigger = false;
            }
            if (col.name == "Elephant")
            {
                elephantOnTrigger = false;
            }
        }

        public bool isElephantOnTrigger()
        {
            return elephantOnTrigger;
        }

        public bool isPlayerOnTrigger()
        {
            return playerOnTrigger;
        }

    }
}