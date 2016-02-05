using UnityEngine;
using Nimbi.Actors;
using UnityEngine.UI;

namespace Nimbi.CameraControl
{
    [RequireComponent(typeof(Animator))]
	public class MainCameraScript : MonoBehaviour
	{
		private Transform centerFocus;
		public float degree;
		public float verticalDegree = 30;
		public float cameraSpeed = 6;
        private bool animationPlayed;

		[HideInInspector]
		public bool playingAnimation;
		private bool doneWithAnimating;

		#region Properties
		public float Sensitivity
		{
			get { return PlayerPrefs.GetFloat("CameraSensitivity", 2f); }
		}
		#endregion

		void Awake()
		{
			playingAnimation = false;
		}

		void Start()
		{
			centerFocus = GameObject.Find("CenterFocus").transform;
        }

		void Update()
		{

            
			if (playingAnimation)
			{
				if(Input.GetButtonDown("Submit") && GetComponent<Animator>().enabled == true)
				{
					GetComponent<Animator>().Play(0,-1,0.99f);
					GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = true;
					playingAnimation = false;
                    transform.localPosition = new Vector3(0, 0, -15);
                }
				else
				{
					GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
				}

				if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
				{
					GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = true;
					playingAnimation = false;
					transform.localPosition = Vector3.back * 15;
					transform.localRotation = Quaternion.identity;
				}
			}
			else if(GetComponent<Animator>() != null)
			{
				GetComponent<Animator>().enabled = false;
				doneWithAnimating = true;
			}
			else if(doneWithAnimating)
			{
				transform.localPosition = Vector3.back * 15;
				transform.localRotation = Quaternion.identity;
				doneWithAnimating = false;
            }

            if (!animationPlayed && GameObject.Find("Loading Screen") != null && !GameObject.Find("Loading Screen").GetComponent<Image>().enabled)
            {
                animationPlayed = true;
                playingAnimation = true;
                GetComponent<Animator>().enabled = true;
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