using UnityEngine;
using Nimbi.Actors;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
    public class MarbleTrackOnesieTrigger : MonoBehaviour
    {

        private Player player;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        private void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
                player.PlayerCanSwitchOnesie = false;
        }

        private void OnTriggerExit(Collider coll)
        {
            if (coll.tag == "Player")
                player.PlayerCanSwitchOnesie = true;
        }
    }
}