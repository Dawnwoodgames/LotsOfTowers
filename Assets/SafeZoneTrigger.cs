using UnityEngine;
using Nimbi.Actors;
using System.Collections;

public class SafeZoneTrigger : MonoBehaviour {

	private void OnTriggerStay(Collider coll)
    {
        if (!coll.GetComponent<PlayerController>().GetMovement())
            coll.GetComponent<PlayerController>().EnableMovement();
    }
}
