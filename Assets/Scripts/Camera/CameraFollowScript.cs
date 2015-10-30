using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour {

    Vector3 player;

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform.position;
        this.transform.position = new Vector3(player.x, player.y, player.z);
    }
}
