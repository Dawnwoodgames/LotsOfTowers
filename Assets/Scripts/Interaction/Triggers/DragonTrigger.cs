using UnityEngine;
using System.Collections;
using Nimbi.Actors;
using Nimbi.UI;

namespace Nimbi.Interaction.Triggers
{
    public class DragonTrigger : MonoBehaviour
    {


        public float horizonSpeed;
        public float verticalSpeed;
        public Transform[] walkspots;
        public Onesie dragonOnesie;

        private GameObject player;
        private bool isWalking;
        private int nextPosition;
        private bool scared = true;
        private bool firstInteraction = false;
        private bool onesieGiven = false;

        //Fields for Scary Statue
        public ScaryStatue scaryStatue;
        private bool inTrigger = false;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void FixedUpdate()
        {
            BraveDragon();
        }

        private void BraveDragon()
        {
            if (!scaryStatue.isScary && scared)
            {
                isWalking = true;
                nextPosition = 0;
                scared = false;
                firstInteraction = true;
            }



            //Lets Give our Dragon something to Move!
            if (isWalking)
            {
                GetComponent<Animator>().SetBool("isWalking", true);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(walkspots[nextPosition].position.x, transform.position.y, walkspots[nextPosition].position.z), 3 * Time.smoothDeltaTime);
                if (Mathf.Abs(transform.position.x - walkspots[nextPosition].position.x) < 0.1f && Mathf.Abs(transform.position.z - walkspots[nextPosition].position.z) < 0.1f)
                {

                    nextPosition++;

                    if (walkspots.Length <= nextPosition)
                    {
                        isWalking = false;
                        GetComponent<Animator>().SetBool("isWalking", false);
                        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, transform.position.z));
                    }
                    else
                        transform.LookAt(new Vector3(walkspots[nextPosition].position.x, transform.position.y, walkspots[nextPosition].position.z));
                    
                }
            }

            if (Input.GetButtonDown("Submit") && firstInteraction)
            {
                if (!onesieGiven)
                {
                    showOnesiePopupAndGiveOnesie();
                }

                if (onesieGiven)
                {

                }
            }
        }



        private void showOnesiePopupAndGiveOnesie()
        {
            player.GetComponent<Player>().AddOnesie(dragonOnesie);
            GameObject.Find("CenterFocus").GetComponent<OnesieInfoPopup>().ShowPopup(OnesieType.Dragon, 0);
            onesieGiven = true;
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
