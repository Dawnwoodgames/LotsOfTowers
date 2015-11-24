using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using LotsOfTowers.Actors;

public class Binocular : MonoBehaviour {

    public Image levelPreview;
    bool isDisplayed = false;
    RectTransform imageTransform;
    Vector2 startSize;
	// Use this for initialization
	void Start () {
        levelPreview.gameObject.SetActive(false);
        imageTransform = levelPreview.GetComponent<RectTransform>();
        startSize = imageTransform.sizeDelta;
	}
	
	// Update is called once per frame
	void Update () {
        if (isDisplayed && imageTransform.rect.width < Screen.width)
            imageTransform.sizeDelta = new Vector2(imageTransform.rect.width + Screen.width/2f * Time.deltaTime, imageTransform.rect.height + Screen.width/2f * Time.deltaTime);

    }

    void OnTriggerStay(Collider coll)
    {
		if(CrossPlatformInputManager.GetButton("Submit") && coll.tag == "Player")
		{
			isDisplayed = true;
			levelPreview.gameObject.SetActive(true);
			coll.GetComponent<PlayerController>().enabled = false;
		}
    }
    void OnTriggerExit(Collider coll)
    {
        if(coll.tag == "Player")
        {
            isDisplayed = false;
            imageTransform.sizeDelta = startSize;
            levelPreview.gameObject.SetActive(false);
        }
    }
}
