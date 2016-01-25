using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.CameraControl
{
	public class MainCameraScript : MonoBehaviour
	{
		private Transform centerFocus;
		public float degree;
		public float verticalDegree;
		public float cameraSpeed = 6;

		[HideInInspector]
		public bool playingAnimation;

		#region Properties
		public float Sensitivity
		{
			get { return PlayerPrefs.GetFloat("CameraSensitivity", 2f); }
		}
		#endregion

		void Awake()
		{
			playingAnimation = true;
		}

		// Use this for initialization
		void Start()
		{
			centerFocus = GameObject.Find("CenterFocus").transform;
			verticalDegree = 30;
        }

		void Update()
		{
			if (playingAnimation)
			{
				if(Input.GetButtonDown("Submit"))
				{
					GetComponent<Animator>().speed = 1000;
					GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = true;
					playingAnimation = false;
				}
				else
				{
					GetComponent<Animator>().enabled = true;
					GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
				}

				if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
				{
					GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = true;
					playingAnimation = false;
				}
			}
			else if(GetComponent<Animator>() != null)
			{
				GetComponent<Animator>().enabled = false;
			}

			// Rotate controls
			if ((Input.GetMouseButton(0) || Input.GetMouseButton(1)) && !playingAnimation)
			{
				degree += Input.GetAxis("Mouse X") * Sensitivity * 1.2f;
			}
			else if ((Input.GetAxis("RightJoystick") != 0) && !playingAnimation)
			{
				degree += Input.GetAxis("RightJoystick") * Sensitivity;
			}

			degree = degree % 360;

			//Set rotation to next degree with a slight lerp
			centerFocus.rotation = Quaternion.Slerp(centerFocus.rotation, Quaternion.Euler(verticalDegree, degree, 0), Time.deltaTime * cameraSpeed);
		}
	}
}