﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Binocular : MonoBehaviour {

    public Image levelPreview;
    bool isDisplayed = false;
    RectTransform imageTransform;
	// Use this for initialization
	void Start () {
        levelPreview.gameObject.SetActive(false);
        imageTransform = levelPreview.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isDisplayed && imageTransform.rect.width < Screen.width)
            imageTransform.sizeDelta = new Vector2(imageTransform.rect.width + Screen.width/2f * Time.deltaTime, imageTransform.rect.height + Screen.width/2f * Time.deltaTime);

    }

    void OnCollisionEnter()
    {
        isDisplayed = true;
        levelPreview.gameObject.SetActive(true);
    }
}
