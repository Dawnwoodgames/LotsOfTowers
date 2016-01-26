using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction
{
    public class BoilerPressurePlate : MonoBehaviour
    {
        public bool lidIsOpen;
        private Player player;
        private bool inTrigger;



        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            inTrigger = false;

        }

        // Update is called once per frame
        void Update()
        {
            //If Nimbi is In his Elephant Onesie!
            if (inTrigger && player.GetComponent<Player>().Onesie.isHeavy)
            {
                lidIsOpen = true;
            }
        }

        public void OnTriggerStay(Collider col)
        {
            if (col.attachedRigidbody)
            {
                inTrigger = true;
            }
        }

        public void OnTriggerExit()
        {
            inTrigger = false;
            lidIsOpen = false;
        }

    }
}

