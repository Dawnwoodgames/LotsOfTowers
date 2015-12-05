using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction.Triggers
{
    public class ElephantLaunchPosition : MonoBehaviour
    {
        public GameObject player;
        private void OnTriggerEnter(Collider coll)
        {
            if (coll.name == "NpcElephant")
            {
                coll.GetComponent<ElephantTrigger>().agentActive = false;
                GameObject.Find("PlayerTrigger").GetComponent<LibraTrigger>().elephantReadyToLaunch = true;
            }
        }

        private void OnTriggerExit(Collider coll)
        {
            if (coll.name == "NpcElephant")
            {
                GameObject.Find("PlayerTrigger").GetComponent<LibraTrigger>().elephantReadyToLaunch = false;
                StartCoroutine(Wait(.5f));
            }
        }

        IEnumerator Wait(float amount)
        {
            yield return new WaitForSeconds(amount);
            player.GetComponent<Rigidbody>().AddForce(Vector3.left * 5, ForceMode.Impulse);
        }
    }
}