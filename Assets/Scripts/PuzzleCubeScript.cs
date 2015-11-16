using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzleCubeScript : MonoBehaviour {

    private Transform floor;
    private Transform sphere;
    private List<Transform> fractures = new List<Transform>();

	void Start () {
        sphere = GameObject.Find("Sphere").transform;
        floor = GameObject.Find("FloatingFloor").transform;
        foreach (Transform child in floor)
        {
            fractures.Add(child);
        }
	}
	
	void Update() {
        if (Input.GetKeyDown(KeyCode.C))
        {
            sphere.GetComponent<Rigidbody>().AddForce(transform.up * 1200, ForceMode.Force);
            foreach (Transform child in fractures)
            {
                child.gameObject.AddComponent<Rigidbody>();
                child.GetComponent<Rigidbody>().useGravity = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            foreach (Transform child in fractures)
            {
                child.GetComponent<Rigidbody>().mass = 0.1f;
                child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            }

            Destroy(sphere.gameObject);
        }
    }
}
