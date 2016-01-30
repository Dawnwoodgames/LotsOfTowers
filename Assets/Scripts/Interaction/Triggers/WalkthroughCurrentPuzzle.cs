using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
    public class WalkthroughCurrentPuzzle : MonoBehaviour
    {

        public GameObject images;

        private bool boardActive = false;

        private void OnTriggerStay(Collider coll)
        {
            if (coll.tag == "Player")
            {
                if (Input.GetButtonDown("Submit") && !boardActive)
                    ShowWalkthrough();
            }
        }

        private void OnTriggerExit(Collider coll)
        {
            if (coll.tag == "Player" && boardActive)
                CloseWalkthrough();
        }

        private void ShowWalkthrough()
        {
            if (!boardActive)
            {
                boardActive = true;
                images.transform.parent = GameObject.Find("HUD").transform;
                images.SetActive(true);
                images.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        private void CloseWalkthrough()
        {
            boardActive = false;
            images.SetActive(false);
            images.transform.parent = GameObject.Find("WalkthroughCurrent").transform;
        }
    }
}