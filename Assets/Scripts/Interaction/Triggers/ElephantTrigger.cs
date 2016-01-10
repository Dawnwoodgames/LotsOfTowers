using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
    public class ElephantTrigger : MonoBehaviour
    {
        public GameObject libraTrigger;
        public GameObject elephantTrigger;
        public GameObject elephantLaunch;
        public GameObject player;
        private LibraTrigger triggerScript;
        private NavMeshAgent agent;

        public bool agentActive = false;
        private bool initialPush;
        private bool onElephantTrigger = false;
        private bool moving = false;
        private bool moveNow = false;
        private bool isJumping = false;

        void Start()
        {
            triggerScript = libraTrigger.GetComponent<LibraTrigger>();
            agent = gameObject.GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            // Move elephant forward when player collides with libra
            if (triggerScript.playerOnLibra && !onElephantTrigger || moveNow)
            {
                if (!initialPush)
                {
                    transform.Translate(Vector3.forward * 2 * Time.deltaTime);
                    initialPush = true;
                }
                MoveElephant();
            }

            // Activate pathfinding when elephant steps off the libra
            if (agentActive && !moving)
            {
                agent.SetDestination(elephantLaunch.transform.position);
                moving = true;
            }

            // Move elephant forward when elephant is on launch position AND player is on libra
            if (triggerScript.elephantReadyToLaunch && triggerScript.playerReadyToLaunch)
            {
                isJumping = true;
                transform.LookAt(player.transform);
                agent.enabled = false;
                MoveElephant();
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().mass = 20;
                player.GetComponent<Rigidbody>().mass = 1;
                moving = false;
            }
            if (!triggerScript.elephantReadyToLaunch && !triggerScript.playerReadyToLaunch && isJumping)
            {
                StartCoroutine(ReturnToPosition());
                isJumping = false;
                
            }
        }

        private void MoveElephant() {
            transform.Translate(Vector3.forward * 2 * Time.deltaTime);
        }

        // When elephant collides with trigger 
        private void OnTriggerEnter(Collider coll)
        {
            if (coll.name == "ElephantTrigger")
            {
                onElephantTrigger = true;
                player.GetComponent<Rigidbody>().mass = GetComponent<Rigidbody>().mass-2;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            }
        }

        public void StartMoving()
        {
            StartCoroutine(Wait(3));
        }

        IEnumerator Wait(int amount)
        {
            yield return new WaitForSeconds(amount);
            agent.enabled = true;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            agentActive = true;
        }

        IEnumerator ReturnToPosition()
        {
            yield return new WaitForSeconds(0.2f);
            agent.enabled = true;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().mass = 5;
            agent.SetDestination(elephantLaunch.transform.position);
            moving = true;
        }
    }
}