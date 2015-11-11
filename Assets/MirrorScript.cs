using UnityEngine;
using System.Collections;

public class MirrorScript : MonoBehaviour {

    public GameObject mirrorPlayer;

    public void UpdateMirroredPlayerPosition(GameObject player,Vector3 rayHit) {
        Vector3 newPosition = rayHit - (player.transform.position - rayHit);
        newPosition.y = player.transform.position.y;
        mirrorPlayer.transform.position = newPosition;

        Debug.DrawLine(player.transform.position+new Vector3(0,1,0), rayHit, Color.red);
        mirrorPlayer.transform.localRotation = new Quaternion(player.transform.localRotation.x,
            player.transform.localRotation.y*-1,
            player.transform.localRotation.z*-1,
            player.transform.localRotation.w)*Quaternion.AngleAxis(90,Vector3.up);
    }


}