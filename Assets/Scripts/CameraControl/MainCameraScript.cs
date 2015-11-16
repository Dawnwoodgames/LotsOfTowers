using UnityEngine;
using System.Collections;

public class MainCameraScript : MonoBehaviour {

    private Transform centerFocus;

    public float degree;
    private float angle;

    private Quaternion rotateLeft = Quaternion.Euler(0, 90, 0);

    // Use this for initialization
    void Start () {
        centerFocus = GameObject.Find("CenterFocus").transform;
        degree = 45;
	}

	// Update is called once per frame
	void Update () {
        CameraInput();
	}

    private void CameraInput()
    {
        // Rotate controls
        if (Input.GetButtonDown("LeftBumper"))
            degree += 90;
        if (Input.GetButtonDown("RightBumper"))
            degree -= 90;

        angle = Mathf.LerpAngle(centerFocus.rotation.y, degree, Time.deltaTime);
        centerFocus.rotation = Quaternion.Slerp(centerFocus.rotation, Quaternion.Euler(30, degree, 0), Time.deltaTime * 20);

        // Zoom controls
        if (Input.GetButtonDown("DPADup") || Input.GetAxis("DPADup") == 1)
        {
            centerFocus.position = new Vector3(25, 45, 0);
            gameObject.GetComponent<Camera>().orthographicSize = 10;
        }
        if (Input.GetButtonDown("DPADdown") || Input.GetAxis("DPADdown") == -1)
        {
            centerFocus.position = new Vector3(20, 45, 0);
            gameObject.GetComponent<Camera>().orthographicSize = 7;
        }
    }
}
