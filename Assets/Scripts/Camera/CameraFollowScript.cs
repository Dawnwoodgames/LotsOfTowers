using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour {

    Vector3 player;
	
	void Update () {
        player = GameObject.FindGameObjectWithTag("Player").transform.position;
        gameObject.transform.position = new Vector3(0, player.y - 2f);
	}
}
