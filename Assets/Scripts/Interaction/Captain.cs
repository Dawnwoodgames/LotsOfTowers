using UnityEngine;
using Nimbi.Actors;
using System.Collections;

namespace Nimbi.Interaction
{
    public class Captain : MonoBehaviour
    {
        public Onesie hamsterOnesie;

        private Player player;
        private GameObject nut;
        private bool nutDelivered = false;
        private Vector3 endMarker;
        private float speed = 1f;
        private float startTime;
        private float journeyLength;
        private bool firstInteraction = true;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            endMarker = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z + 1f);
            nut = GameObject.Find("Nut");
            journeyLength = Vector3.Distance(transform.position, endMarker);
        }

        void Update()
        {
            if (nutDelivered)
            {
                float distCovered = (Time.time - startTime) * speed;
                float fracJourney = distCovered / journeyLength;
                transform.position = Vector3.Lerp(transform.position, endMarker, fracJourney);
            }
        }

        private void OnTriggerStay(Collider coll)
        {
            /*
            First interaction with the Captain
            Gives Hamster Onesie
            */
            if (coll.tag == "Player" && Input.GetButtonDown("Submit") && firstInteraction)
            {
                player.GetComponent<Player>().AddOnesieToFirstFreeSlot(hamsterOnesie);
                firstInteraction = false;
            }

            /*
            Second interaction with the Captain
            If nut is equipped, Captain will move aside
            */
            if (coll.tag == "Player" && Input.GetButtonDown("Submit") && !firstInteraction)
            {
                if (nut != null && nut.GetComponent<Nut>().pickedUp)
                {
                    nutDelivered = true;
                    Destroy(nut);
                    startTime = Time.time;
                }
            }
        }
    }
}