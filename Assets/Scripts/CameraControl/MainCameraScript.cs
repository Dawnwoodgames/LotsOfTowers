using UnityEngine;
using System.Collections;

public class MainCameraScript : MonoBehaviour
{
	private Transform centerFocus;
	public float degree;
    public float verticalDegree;

	// Use this for initialization
	void Start()
	{
		centerFocus = GameObject.Find("CenterFocus").transform;
        verticalDegree = 30;
	}

	// Update is called once per frame
	void Update()
	{
		CameraInput();
	}

	private void CameraInput()
	{
		// Rotate controls
		if (Input.GetButtonDown("LeftBumper"))
		{
			degree += 90;
		}
		if (Input.GetButtonDown("RightBumper"))
		{
			degree -= 90;
		}

        degree = degree % 360;

		//Set rotation to next degree with a slight lerp
		centerFocus.rotation = Quaternion.Slerp(centerFocus.rotation, Quaternion.Euler(verticalDegree, degree, 0), Time.deltaTime * 20);

		// Zoom controls
		if (Input.GetButtonDown("DPADup") || Input.GetAxis("DPADup") == 1)
		{
			//Zoom in
			centerFocus.position = new Vector3(25, 45, 0);
			gameObject.GetComponent<Camera>().orthographicSize = 10;
		}
		if (Input.GetButtonDown("DPADdown") || Input.GetAxis("DPADdown") == -1)
		{
			//Zoom out
			centerFocus.position = new Vector3(20, 45, 0);
			gameObject.GetComponent<Camera>().orthographicSize = 7;
		}
        if (Input.GetKeyDown(KeyCode.K))
            verticalDegree = 10f;
        else if (Input.GetKeyUp(KeyCode.K))
            verticalDegree = 30f;
	}
}
