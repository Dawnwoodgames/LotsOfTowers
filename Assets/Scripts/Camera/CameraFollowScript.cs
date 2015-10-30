using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour {

    Vector3 player;

    private float distancePlayerFromCamera;
	
	void Update () {
        player = GameObject.FindGameObjectWithTag("Player").transform.position;
        gameObject.transform.position = new Vector3(0, player.y - 2f);

        distancePlayerFromCamera = Vector3.Distance(player, gameObject.transform.position);
	}

    public float GetDistancePlayerFromCamera()
    {
        return -distancePlayerFromCamera - 5f;
    }
}
