using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction.Triggers
{
    public class DragonTrigger : MonoBehaviour {

        public GameObject dragon;
        private GameObject player;
        public float horizonSpeed;
        public float verticalSpeed;
        public Transform[] walkspots;
        public float aplitude;

        private bool isWalking;
        private int nextPosition;

        //Fields for Scary Statue
        public ScaryStatue scaryStatue;
        private bool inTrigger = false;
        
        void Start() {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void FixedUpdate() {
            BraveDragon();
        }

        private void BraveDragon()
        {
            if (!scaryStatue.isScary)
            {
                isWalking = true;
                nextPosition = 0;

                if (isWalking)
                {
                    dragon.transform.position = Vector3.MoveTowards(dragon.transform.position, new Vector3(walkspots[nextPosition].position.x, dragon.transform.position.y, walkspots[nextPosition].position.z), 3 * Time.smoothDeltaTime);
                    if (Mathf.Abs(dragon.transform.position.x - walkspots[nextPosition].position.x) < 0.1f && Mathf.Abs(dragon.transform.position.z - walkspots[nextPosition].position.z) < 0.1f)
                    {

                        nextPosition++;

                        if (walkspots.Length <= nextPosition)
                        {
                            isWalking = false;
                            dragon.GetComponent<Animator>().SetBool("isWalking", false);
                            dragon.transform.LookAt(new Vector3(player.transform.position.x, dragon.transform.position.y, dragon.transform.position.z));
                        }
                        else
                            dragon.transform.LookAt(new Vector3(walkspots[nextPosition].position.x, dragon.transform.position.y, walkspots[nextPosition].position.z));
                    }
               
                }
            }
        }

        private void OnTriggerStay(Collider coll)
        {
           if(coll.attachedRigidbody)
            {
                inTrigger = true;
            }
        }
    }
}
