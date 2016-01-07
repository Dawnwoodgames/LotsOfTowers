using UnityEngine;
using System.Collections;

public class MainCameraScript : MonoBehaviour
{
    private float cameraSensitivity;
	private Transform centerFocus;
	public float degree;
	public float verticalDegree;
	public float cameraSpeed = 6;


	// Use this for initialization
	public void Start()
	{
        cameraSensitivity = PlayerPrefs.GetFloat("CameraSensitivity", 2f);
		centerFocus = GameObject.Find("CenterFocus").transform;
		verticalDegree = 30;
	}

	public void Update()
	{
		// Rotate controls
		if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) {
			degree += Input.GetAxis("Mouse X") * cameraSensitivity * 1.2f;
		} else if (Input.GetAxis("RightJoystick") != 0) {
            degree += Input.GetAxis("RightJoystick") * cameraSensitivity;
        }


		degree = degree % 360;

		//Set rotation to next degree with a slight lerp
		centerFocus.rotation = Quaternion.Slerp(centerFocus.rotation, Quaternion.Euler(verticalDegree, degree, 0), Time.deltaTime * cameraSpeed);
	}
}