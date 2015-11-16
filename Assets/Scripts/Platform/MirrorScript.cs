using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MirrorScript : MonoBehaviour {

    public GameObject player;
    public GameObject mirrorPlayer;

    private bool mirrorPlayerCurrentlyVisible = true;
    private bool playerCurrentlyVisible = true;

    void Update()
    {
        CheckMirror();
        CheckVisibility();
    }

    public void UpdateMirroredPlayerPosition(GameObject player,Vector3 rayHit) {
        Vector3 newPosition = rayHit - (player.transform.position - rayHit);
        newPosition.y = player.transform.position.y;
        mirrorPlayer.transform.position = newPosition;

        mirrorPlayer.transform.localRotation = new Quaternion(player.transform.localRotation.x,
            player.transform.localRotation.y*-1,
            player.transform.localRotation.z*-1,
            player.transform.localRotation.w)*Quaternion.AngleAxis(90,Vector3.down);
    }

    private void CheckMirror()
    {
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

    private void CheckVisibility()
    {
        GameObject cam = Camera.main.gameObject;

        //First check if the mirrored player can be seen through the mirror

        RaycastHit[] hits = Physics.RaycastAll(mirrorPlayer.transform.position + new Vector3(0, 1, 0), cam.transform.position - mirrorPlayer.transform.position, 20);
        bool mirrorfound = false;
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag == "Mirror" && !mirrorfound)
            {
                mirrorfound = true;
            }

        }

        if (!mirrorfound && mirrorPlayerCurrentlyVisible)
        {
            Renderer[] rs = mirrorPlayer.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rs)
                r.enabled = false;

            mirrorPlayerCurrentlyVisible = false;
        }
        else if (mirrorfound && !mirrorPlayerCurrentlyVisible)
        {
            Renderer[] rs = mirrorPlayer.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rs)
                r.enabled = true;

            mirrorPlayerCurrentlyVisible = true;
        }

        // If there is no mirror between the player and the mirrorplayer, hide the mirrorplayer
        if(!MirrorBetween(mirrorPlayer, player) && mirrorfound)
        {
            Renderer[] rs = mirrorPlayer.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rs)
                r.enabled = false;

            mirrorPlayerCurrentlyVisible = false;
        }

        //Then check if there is a mirror between the camera and the player

        Vector3 rayDirection = cam.transform.position - player.transform.position;
        rayDirection.y = 0;
        hits = Physics.RaycastAll(player.transform.position + new Vector3(0, 1, 0), rayDirection, 20);
        mirrorfound = false;
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag == "Mirror" && !mirrorfound)
            {
                mirrorfound = true;
            }

        }

        if (mirrorfound && playerCurrentlyVisible)
        {
            Renderer[] rs = player.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rs)
                r.enabled = false;

            playerCurrentlyVisible = false;
        }
        else if (!mirrorfound && !playerCurrentlyVisible)
        {
            Renderer[] rs = player.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rs)
                r.enabled = true;

            playerCurrentlyVisible = true;
        }
    }

    private bool MirrorBetween(GameObject p1, GameObject p2)
    {
        RaycastHit[] hits = Physics.RaycastAll(p1.transform.position, p2.transform.position - p1.transform.position, 20);
        bool mirrorfound = false;
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag == "Mirror" && !mirrorfound)
            {
                mirrorfound = true;
            }

        }

        return mirrorfound;
    }
}