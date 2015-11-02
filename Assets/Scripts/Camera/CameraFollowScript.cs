using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Camera
{
    public class CameraFollowScript : MonoBehaviour
    {

        Vector3 playerPosition;

        void Update()
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            this.transform.position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);
        }
    }
}