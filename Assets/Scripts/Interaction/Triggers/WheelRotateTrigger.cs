using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction.Triggers
{
    public class WheelRotateTrigger : MonoBehaviour
    {
        public bool isBreakable;

        private bool playerRunning = false;

        private void OnTriggerStay(Collider coll)
        {
            playerRunning = true;
        }

        private void OnTriggerExit()
        {
            playerRunning = false;
        }

        public bool GetPlayerRunning() { return playerRunning; }
    }
}