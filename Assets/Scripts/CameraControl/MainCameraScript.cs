using UnityEngine;
using System.Collections;

public class MainCameraScript : MonoBehaviour
{
	private Transform centerFocus;
	public float degree;
	public float verticalDegree;
	public float cameraSpeed = 6;


	// Use this for initialization
	void Start()
	{
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
		} else
		{
			// *2 should be :
			// *mouseSensitivity
			degree += Input.GetAxis("Mouse X") * 2;
		}
		degree = degree % 360;

		//Set rotation to next degree with a slight lerp
		centerFocus.rotation = Quaternion.Slerp(centerFocus.rotation, Quaternion.Euler(verticalDegree, degree, 0), Time.deltaTime * cameraSpeed);
	}
}