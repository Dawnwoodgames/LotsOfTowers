using UnityEngine;
using System.Collections;

namespace Nimbi.Environment
{
    public class MarbleTrackTransparancy : MonoBehaviour
    {
        private GameObject marbleTrack;
        private Renderer rend;
        private Material originMaterial;

        void Start()
        {
            marbleTrack = transform.GetChild(0).gameObject;
            rend = marbleTrack.GetComponent<Renderer>();
            originMaterial = rend.materials[0];
        }

        private void OnCollisionEnter(Collision coll)
        {
            if (coll.gameObject.tag == "Player")
                ActivateSeethrough();
        }

        private void OnCollisionExit(Collision coll)
        {
            if (coll.gameObject.tag == "Player")
                DeactivateSeethrough();
        }

        private void ActivateSeethrough()
        {
            rend.material = rend.materials[1];
        }

        private void DeactivateSeethrough()
        {
            rend.material = originMaterial;
        }
    }
}