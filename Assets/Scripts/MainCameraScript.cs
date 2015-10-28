using UnityEngine;
using System.Collections;

public class MainCameraScript : MonoBehaviour {

    GameObject main;

    private bool _inputLocked;

	// Use this for initialization
	void Start ()
    {
        main = GameObject.Find("Camera");
    }
	
	// Update is called once per frame
	void Update () {
        CameraInput();
	}

    private void CameraInput()
    {
        //main.transform.Rotate(0, Input.GetAxis("RightJoystickX") * 2, 0);
        if (Input.GetKey("e"))
            main.transform.Rotate(Vector3.down * 2);
        if (Input.GetKey("q"))
            main.transform.Rotate(Vector3.up * 2);

        if (Input.GetKey("r"))
        {
            if (!isRotateLocked())
            {
                main.transform.Rotate(0, -90, 0);
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
