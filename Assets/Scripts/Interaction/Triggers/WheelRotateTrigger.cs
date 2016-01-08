using UnityEngine;
using LotsOfTowers.Actors;
using System.Collections;

namespace LotsOfTowers.Interaction.Triggers
{
    public class WheelRotateTrigger : MonoBehaviour
    {

        private bool playerRunning = false;

        private void OnTriggerStay(Collider coll)
        {
            if (coll.tag == "Player" && coll.GetComponent<Player>().Onesie.type == OnesieType.Hamster)
                playerRunning = true;
        }

        private void OnTriggerExit()
        {
            playerRunning = false;
        }

        public bool GetPlayerRunning() { return playerRunning; }
    }
}