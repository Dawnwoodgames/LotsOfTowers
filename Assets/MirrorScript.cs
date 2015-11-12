using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MirrorScript : MonoBehaviour {

    public GameObject player;
    public GameObject mirrorPlayer;

    void Update()
    {
        CheckMirror();
    }

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

    private void CheckMirror()
    {
        Debug.DrawRay(player.transform.position + new Vector3(0, 1, 0), Quaternion.AngleAxis(45, Vector3.down) * Vector3.left * 20, Color.red);
        Debug.DrawRay(player.transform.position + new Vector3(0, 1, 0), Quaternion.AngleAxis(45, Vector3.down) * Vector3.forward * 20, Color.blue);
        Debug.DrawRay(player.transform.position + new Vector3(0, 1, 0), Quaternion.AngleAxis(45, Vector3.down) * Vector3.back * 20, Color.green);
        Debug.DrawRay(player.transform.position + new Vector3(0, 1, 0), Quaternion.AngleAxis(45, Vector3.down) * Vector3.right * 20, Color.magenta);

        List<RaycastHit> hitList = new List<RaycastHit>(Physics.RaycastAll(player.transform.position + new Vector3(0, 1, 0), Quaternion.AngleAxis(45, Vector3.down) * Vector3.left, 20));
        hitList.AddRange(Physics.RaycastAll(player.transform.position + new Vector3(0, 1, 0), Quaternion.AngleAxis(45, Vector3.down) * Vector3.forward, 20));
        hitList.AddRange(Physics.RaycastAll(player.transform.position + new Vector3(0, 1, 0), Quaternion.AngleAxis(45, Vector3.down) * Vector3.back, 20));
        hitList.AddRange(Physics.RaycastAll(player.transform.position + new Vector3(0, 1, 0), Quaternion.AngleAxis(45, Vector3.down) * Vector3.right, 20));

        RaycastHit[] hits = hitList.ToArray();
        bool mirrorfound = false;
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag == "Mirror" && !mirrorfound)
            {
                UpdateMirroredPlayerPosition(player, hit.point);
                mirrorfound = true;
            }

        }
    }


}