using UnityEngine;
using System.Collections;

public class WalkthroughCurrentPuzzle : MonoBehaviour {

    public GameObject images;

    private bool boardActive = false;

    private void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "Player")
        {
            if (Input.GetButtonDown("Submit") && !boardActive)
                ShowWalkthrough();
            else
            {
                if (Input.GetButtonDown("Submit"))
                {
                    if (images.transform.GetChild(0).gameObject.activeSelf)
                    {
                        images.transform.GetChild(0).gameObject.SetActive(false);
                        images.transform.GetChild(1).gameObject.SetActive(true);
                    }
                    else
                    {
                        images.transform.GetChild(0).gameObject.SetActive(true);
                        images.transform.GetChild(1).gameObject.SetActive(false);
                    }
                }
            }
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
            images.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    private void CloseWalkthrough()
    {
        boardActive = false;
        images.SetActive(false);
        images.transform.parent = GameObject.Find("walkthrough").transform;
    }
}
