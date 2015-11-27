using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction.Triggers
{
    public class ElephantTrigger : MonoBehaviour
    {
        public GameObject libraTrigger;
        public GameObject elephantTrigger;
        public GameObject elephantLaunch;
        private LibraTrigger triggerScript;
        private NavMeshAgent agent;

        public bool agentActive = false;
        private bool elephantStrafes = false;
        private bool initialPush;

        void Start()
        {
            triggerScript = libraTrigger.GetComponent<LibraTrigger>();
            agent = gameObject.GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            // Move elephant forward when player collides with libra
            if (triggerScript.playerOnLibra)
            {
                if (!initialPush)
                {
                    transform.Translate(Vector3.forward * 8 * Time.deltaTime);
                    initialPush = true;
                }

                MoveElephant();
            }

            // Activate pathfinding when elephant steps off the libra
            if (agentActive)
                agent.SetDestination(elephantLaunch.transform.position);

            // Move elephant forward when elephant is on launch position AND player is on libra
            if (triggerScript.elephantReadyToLaunch)
            {
                if (triggerScript.playerOnLibra)
                    triggerScript.libra.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }

        private void MoveElephant() {
            transform.Translate(Vector3.forward * 2 * Time.deltaTime);
        }

        // When elephant collides with trigger 
        private void OnTriggerEnter(Collider coll)
        {
            if (coll.name == "RotateElephantTrigger")
            {
                transform.rotation = new Quaternion(0, 0, transform.rotation.y - 0.3f, 0);
                triggerScript.libra.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
                StartCoroutine(Wait(1));
            }
        }
        IEnumerator Wait(int amount)
        {
            yield return new WaitForSeconds(amount);
            triggerScript.playerOnLibra = false;
            agent.enabled = true;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            agentActive = true;
        }
    }
}