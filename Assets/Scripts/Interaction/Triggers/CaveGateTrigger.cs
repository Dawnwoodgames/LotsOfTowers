using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction
{

    public class CaveGateTrigger : MonoBehaviour
    {
        private Player player;
        private bool inTrigger;
        private ScaryStatue scaryStatue;


        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            inTrigger = false;
            scaryStatue = GameObject.Find("Scary-Statue").GetComponent<ScaryStatue>();
        }

        // Update is called once per frame
        void Update()
        {
            if (inTrigger && scaryStatue.isScary)
            {
                player.transform.position = new Vector3(10f, 0.88f, 15f);
            }
        }

        private void OnTriggerStay(Collider coll)
        {
            if (coll.attachedRigidbody)
            {
                inTrigger = true;
            }
        }
    }
}