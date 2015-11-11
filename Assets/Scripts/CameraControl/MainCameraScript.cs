using UnityEngine;
using System.Collections;

public class MainCameraScript : MonoBehaviour {

    private Transform centerFocus;

    private Vector3 startRot, endRot;

	// Use this for initialization
	void Start () {
        centerFocus = GameObject.Find("CenterFocus").transform;
	}
	
	// Update is called once per frame
	void Update () {
        CameraInput();
	}

    private void CameraInput()
    {
        if (Input.GetButtonDown("LeftBumper"))
        {
            centerFocus.Rotate(0, +90, 0, Space.World);
        }
        else if (Input.GetButtonDown("RightBumper"))
        {
            centerFocus.Rotate(0, -90, 0, Space.World);
        }
    }
}
