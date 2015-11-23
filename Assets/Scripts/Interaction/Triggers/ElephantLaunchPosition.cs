using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction.Triggers
{
    public class ElephantLaunchPosition : MonoBehaviour
    {
        private void OnTriggerEnter(Collider coll)
        {
            if (coll.name == "NpcElephant")
            {
                coll.GetComponent<ElephantTrigger>().agentActive = false;
                GameObject.Find("PlayerTrigger").GetComponent<LibraTrigger>().elephantReadyToLaunch = true;
            }
        }
    }
}