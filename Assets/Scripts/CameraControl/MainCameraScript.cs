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
        // Rotate controls
        if (Input.GetButtonDown("LeftBumper"))
            centerFocus.Rotate(0, +90, 0, Space.World);
        if (Input.GetButtonDown("RightBumper"))
            centerFocus.Rotate(0, -90, 0, Space.World);

        // Zoom controls
        /*if (Input.GetButtonDown("DPADup") || Input.GetAxis("DPADup") == 1)
        {
            centerFocus.position = new Vector3(25, 45, 0);
            gameObject.GetComponent<Camera>().orthographicSize = 10;
        }
        if (Input.GetButtonDown("DPADdown") || Input.GetAxis("DPADdown") == -1)
        {
            centerFocus.position = new Vector3(20, 45, 0);
            gameObject.GetComponent<Camera>().orthographicSize = 7;
        }*/
    }
}
