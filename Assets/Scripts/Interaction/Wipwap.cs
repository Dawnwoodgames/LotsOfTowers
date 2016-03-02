using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction
{
    public class Wipwap : MonoBehaviour
    {

        public Player player;
        public GameObject wipwap;
        public Trashbag trash;
        private bool inTrigger;
        private bool activated;
        private Quaternion startRotation;
        private Quaternion goalRotation;

        void Start()
        {
            startRotation = wipwap.transform.localRotation;
            goalRotation = Quaternion.Euler(63.5f, 81, 350);
        }

        void Update()
        {
            // Player is in the trigger and is an elephant, so move that stuff
            if(inTrigger && player.Onesie.type == OnesieType.Elephant && !activated)
            {
                activated = true;
                trash.MoveUp();
            }

            if (activated)
            {
                wipwap.transform.localRotation = Quaternion.RotateTowards(wipwap.transform.localRotation, goalRotation, 4.0f);
            }
        }

        void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
                inTrigger = true;
        }

        void OnTriggerExit(Collider coll)
        {
            if (coll.tag == "Player")
                inTrigger = false;
        }
    }
}