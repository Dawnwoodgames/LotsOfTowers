using UnityEngine;
using System.Collections;

public class MainCameraScript : MonoBehaviour {

    GameObject main;

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
        //main.transform.Rotate(0, Input.GetKey("RightJoystickX") * 2, 0);
        if (Input.GetKey("e"))
            main.transform.Rotate(Vector3.down * 2);
        if (Input.GetKey("q"))
            main.transform.Rotate(Vector3.up * 2);
    }
}
