using UnityEngine;
using System.Collections;

public class WaterInteraction : MonoBehaviour {

	void Start () {
	
	}

    private void OnTriggerStay(Collider coll)
    {
        if (!coll.GetComponent<Nimbi.Actors.Player>().Onesie.isHeavy)
            coll.GetComponent<Rigidbody>().AddForce(Vector3.up, ForceMode.Impulse);
    }
}
