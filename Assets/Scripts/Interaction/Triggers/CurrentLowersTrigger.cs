using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
    public class CurrentLowersTrigger : MonoBehaviour
    {
        public Material lowersMat;

        private Renderer rend;

        void Start()
        {
            rend = GameObject.Find("WalkthroughFloor").GetComponent<Renderer>();
        }

        private void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
                ReplaceFloorWithImage();
        }

        private void ReplaceFloorWithImage()
        {
            rend.material = lowersMat;
        }
    }
}