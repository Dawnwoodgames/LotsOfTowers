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

        private void OnTriggerExit(Collider coll)
        {
            if (coll.name == "NpcElephant")
            {
                StartCoroutine(Wait(.5f));
            }
        }

        IEnumerator Wait(float amount)
        {
            yield return new WaitForSeconds(amount);
            GameObject.Find("Player").GetComponent<Rigidbody>().AddForce(Vector3.left * 10, ForceMode.Impulse);
        }
    }
}