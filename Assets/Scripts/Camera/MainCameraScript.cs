using UnityEngine;
using System.Collections;
using System;

public class MainCameraScript : MonoBehaviour {

    GameObject cameraObject;

    private bool _inputLocked;

	// Use this for initialization
	void Start ()
    {
        cameraObject = GameObject.Find("Camera");
    }
	
	// Update is called once per frame
	void Update () {
        CameraInput();
        CameraDepth();
	}

    private void CameraDepth()
    {
        gameObject.transform.position = new Vector3(cameraObject.transform.position.x, gameObject.transform.position.y, cameraObject.GetComponent<CameraFollowScript>().GetDistancePlayerFromCamera());
    }

    private void CameraInput()
    {
        cameraObject.transform.Rotate(0, Input.GetAxis("RightJoystickX") * 2, 0);
        if (Input.GetKey("e"))
            cameraObject.transform.Rotate(Vector3.down * 2);
        if (Input.GetKey("q"))
            cameraObject.transform.Rotate(Vector3.up * 2);

        if (Input.GetKey("r"))
        {
            if (!isRotateLocked())
            {
                cameraObject.transform.Rotate(0, -90, 0);
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
