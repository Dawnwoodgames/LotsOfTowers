using UnityEngine;
using System.Collections;
using LotsOfTowers.Actors;

[RequireComponent(typeof(Animator))]
public class CompleteFan : MonoBehaviour {

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Player" && !other.gameObject.GetComponent<Player>().Onesie.isHeavy)
        {
            animator.SetBool("GoingDown", false);
            animator.SetBool("GoingUp", true);
        }
        else if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<Player>().Onesie.isHeavy)
        {
            animator.SetBool("GoingDown", true);
            animator.SetBool("GoingUp", false);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("GoingDown", false);
            animator.SetBool("GoingUp", true);
        }
    }
}
