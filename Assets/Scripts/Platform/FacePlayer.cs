﻿using UnityEngine;
using System.Collections;

public class FacePlayer : MonoBehaviour {

    public Transform player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }
}
