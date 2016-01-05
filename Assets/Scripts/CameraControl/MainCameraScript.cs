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
		if (Input.GetButtonDown("LeftBumper")) {
			//degree = Mathf.Round(degree / 90) * 90 + 90;
		} else if (Input.GetButtonDown("RightBumper")) {
			//degree = Mathf.Round(degree / 90) * 90 - 90;
		} else if (Input.GetMouseButton(0)) {
			degree += Input.GetAxis("Mouse X") * cameraSensitivity;
		}
		degree = degree % 360;

		//Set rotation to next degree with a slight lerp
		centerFocus.rotation = Quaternion.Slerp(centerFocus.rotation, Quaternion.Euler(verticalDegree, degree, 0), Time.deltaTime * cameraSpeed);
	}
}