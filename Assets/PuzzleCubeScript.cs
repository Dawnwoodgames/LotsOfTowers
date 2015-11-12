using UnityEngine;
using System.Collections;

public class PuzzleCubeScript : MonoBehaviour {

    private Transform breakEvent;

	void Start () {
        breakEvent = GameObject.Find("BreakEvent").transform;
	}
	
	void Update () {
        breakEvent.GetComponent<Rigidbody>().AddForce(transform.up * 20);
	}
}
