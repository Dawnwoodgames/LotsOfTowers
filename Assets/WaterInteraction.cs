using UnityEngine;
using System.Collections;

public class WaterInteraction : MonoBehaviour {

	void Start () {
	
	}

    private void OnTriggerStay(Collider coll)
    {
        if (!coll.GetComponent<LotsOfTowers.Actors.Player>().IsElephant)
            coll.GetComponent<Rigidbody>().AddForce(Vector3.up, ForceMode.Impulse);
    }
}
