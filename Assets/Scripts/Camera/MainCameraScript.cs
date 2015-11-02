using UnityEngine;
using System.Collections;
using System;

public class MainCameraScript : MonoBehaviour {

    GameObject centerObject;

    private bool _inputLocked;
    
    void Start()
    {
        centerObject = GameObject.Find("CenterFocus");
    }
	
	void Update () {
        CameraInput();
    }

    private void CameraInput()
    {
        centerObject.transform.Rotate(0, Input.GetAxis("CameraRotate") * 2, 0);

        if (Input.GetKey("r"))
        {
            if (!isRotateLocked())
            {
                centerObject.transform.Rotate(0, -90, 0);
                LockRotate();
            }
        }
    }

    private void LockRotate()
    {
        _inputLocked = true;
        Invoke("UnlockRotate", .2f);
    }

    private void UnlockRotate()
    {
        _inputLocked = false;
    }

    private bool isRotateLocked()
    {
        return _inputLocked;
    }
}
