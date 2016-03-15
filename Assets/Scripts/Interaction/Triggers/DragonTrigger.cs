using UnityEngine;
using System.Collections;
using Nimbi.Actors;
using Nimbi.UI;

namespace Nimbi.Interaction
{
    public class DragonTrigger : MonoBehaviour
    {
        public Transform[] walkspots;


        private GameObject player;
        private bool isWalking;
        private int nextPosition;
        private bool scared = true;


        //Fields for Scary Statue
        public MaskRotationTrigger scaryStatue;

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


            }
        }
}
