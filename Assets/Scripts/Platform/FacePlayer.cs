using UnityEngine;
using System.Collections;

public class FacePlayer : MonoBehaviour
{
	public Transform player;

	// Update is called once per frame
	void Update()
	{
		transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
	}
}
