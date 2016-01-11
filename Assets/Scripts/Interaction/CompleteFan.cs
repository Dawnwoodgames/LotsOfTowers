using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction
{
    public class CompleteFan : MonoBehaviour
    {
        private Animator animator;
        void Start()
        {
            animator = GetComponentInParent<Animator>();
        }

        void OnTriggerStay(Collider player)
        {
            player.GetComponent<Rigidbody>().sleepThreshold = 0;

            if (!player.GetComponent<Player>().Onesie.isHeavy)
            {
                animator.SetBool("GoingDown", false);
                animator.SetBool("GoingUp", true);
            }
            else if (player.GetComponent<Player>().Onesie.isHeavy)
            {
                animator.SetBool("GoingDown", true);
                animator.SetBool("GoingUp", false);
            }
        }

        void OnTriggerExit(Collider player)
        {
            player.GetComponent<Rigidbody>().sleepThreshold = 0.14f;
        }
    }
}