using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Nimbi.Actors;

public class MirrorScript : MonoBehaviour {

    public GameObject player;
    public GameObject mirrorPlayer;
    private Vector3 mirrorNormal;

    private bool mirrorPlayerCurrentlyVisible = true;
    private bool playerCurrentlyVisible = true;

    void Start()
    {
        
    }

    void Update()
    {
        CheckMirror();
        CheckVisibility();
    }

    public void UpdateMirroredPlayerPosition(GameObject player,Vector3 rayHit) {
        Vector3 newPosition = transform.position + Vector3.Reflect(player.transform.position - transform.position, mirrorNormal);
        newPosition.y = player.transform.position.y;
        mirrorPlayer.transform.position = newPosition;

        mirrorPlayer.transform.localRotation = new Quaternion(player.transform.localRotation.x,
            player.transform.localRotation.y*-1,
            player.transform.localRotation.z*-1,
            player.transform.localRotation.w)*Quaternion.AngleAxis(90,Vector3.down);
    }

    private void CheckMirror()
    {

        Vector3 hitTarget = transform.position - player.transform.position;
        hitTarget.y = 1;
        RaycastHit[] hits = Physics.RaycastAll(player.transform.position+Vector3.up, hitTarget, 20);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag == "Mirror")
            {
                mirrorNormal = hit.normal;
                UpdateMirroredPlayerPosition(player, hit.normal);
            }
        }
    }

    private void CheckVisibility()
    {
        //First check if the mirrored player can be seen through the mirror
        RaycastHit[] hits;
        bool mirrorfound = true;
        for (int hor = 0; hor <= 1; hor++)
            for (int vert = 0; vert <= 1; vert++)
            {
                if (!mirrorfound)
                    continue;
                bool throughmirror = false;
                hits = Physics.RaycastAll(mirrorPlayer.transform.position, Camera.main.ViewportToWorldPoint(new Vector3(hor, vert, Camera.main.nearClipPlane)) - mirrorPlayer.transform.position, 20);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.tag == "Mirror")
                    {
                        throughmirror = true;
                    }

                }
                mirrorfound = throughmirror;
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

        mirrorfound = true;
        for (int hor = 0; hor <= 1; hor++)
            for (int vert = 0; vert <= 1; vert++)
            {
                if (!mirrorfound)
                    continue;
                bool throughmirror = false;
                hits = Physics.RaycastAll(player.transform.position, Camera.main.ViewportToWorldPoint(new Vector3(hor, vert, Camera.main.nearClipPlane)) - player.transform.position, 20);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.tag == "Mirror")
                    {
                        throughmirror = true;
                    }

                }
                mirrorfound = throughmirror;
            }

        if (mirrorfound)
        {
            Renderer[] rs = player.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rs)
                r.enabled = false;

            playerCurrentlyVisible = false;
        }
        else if (!mirrorfound && !playerCurrentlyVisible)
        {
            player.GetComponent<Player>().ResetRenderers();

            playerCurrentlyVisible = true;
        }
    }
    /// <summary>
    /// Check if there is a mirror between two game objects
    /// </summary>
    /// <param name="p1">Game object 1</param>
    /// <param name="p2">Game object 2</param>
    /// <returns></returns>
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