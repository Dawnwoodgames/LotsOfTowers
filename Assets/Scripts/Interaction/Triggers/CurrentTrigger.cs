using UnityEngine;
using Nimbi.Actors;
using System.Collections;

public class CurrentTrigger : MonoBehaviour {

    private Rigidbody rb;

    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    private void OnTriggerExit(Collider coll)
    {
        rb.velocity = rb.velocity / 10;
    }

    private void OnTriggerStay(Collider coll)
    {
        coll.GetComponent<PlayerController>().DisableMovement();
        if (coll.tag == "Player")
            if (rb.velocity.magnitude < 5)
                rb.AddForce(transform.forward * 1f, ForceMode.Impulse);
    }
}
