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

        private bool agentActive = false;
        private bool elephantStrafes = false;

        void Start()
        {
            triggerScript = libraTrigger.GetComponent<LibraTrigger>();
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (triggerScript.firstTrigger)
                MoveElephant();

            if (agentActive)
                agent.SetDestination(elephantLaunch.transform.position);

            if (triggerScript.secondTrigger)
                MoveElephant();
        }

        private void MoveElephant() {
            transform.Translate(Vector3.forward * 2 * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider coll)
        {
            if (coll.name == "elephanttrigger")
            {
                agentActive = true;
                this.transform.Rotate(new Vector3(.45f, 0f, 0f));
            }
        }
    }
}