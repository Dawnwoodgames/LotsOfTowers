using UnityEngine;
using Nimbi.Actors;
using System.Collections;

public class CurrentTrigger : MonoBehaviour {

    private Rigidbody rb;

    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "Player")
        {
            /*rb.isKinematic = true;
            rb.isKinematic = false;*/

            if (rb.velocity.magnitude < 5)
                rb.AddForce(transform.forward * 1f, ForceMode.Impulse);
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        rb.velocity = Vector3.one;
    }
}
