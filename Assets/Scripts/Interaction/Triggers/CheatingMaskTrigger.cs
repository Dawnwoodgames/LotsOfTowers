using UnityEngine;
using System.Collections;
using Nimbi.Actors;
using Nimbi.Interaction.Triggers;

namespace Nimbi.Actors.Triggers
{
    public class CheatingMaskTrigger : MonoBehaviour
    {

        private bool inTrigger;
        private GameObject player;
        public MaskRotationTrigger cheatingMask;

        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            
        }

        // Update is called once per frame
        void Update()
        {
            if(inTrigger && cheatingMask.isCheating)
            {
                cheatingMask.PushNimbiAway();
            }
        }

        private void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
            {
                Debug.Log("Whatttttt, player is hitting meeeeeeee?");
                inTrigger = true;
            }
        }

    }




}
