using UnityEngine;
using Nimbi.Actors;
using System.Collections;

public class SafeZoneTrigger : MonoBehaviour {

	private void OnTriggerStay(Collider coll)
    {
        if (!coll.GetComponent<PlayerController>().GetMovement())
            coll.GetComponent<PlayerController>().EnableMovement();
    }

    private void OnTriggerEnter(Collider coll) { StartCoroutine(ResetVelocity(coll)); }

    private IEnumerator ResetVelocity(Collider coll)
    {
        yield return new WaitForSeconds(1);
        coll.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
