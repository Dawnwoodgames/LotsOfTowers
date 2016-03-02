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
        private bool cameraEnabled = true; // Boolean to call when Nimbi can Interact with the Camera.

        //Create function to shake the camera when needed//
        //public bool shakeCamera;
        //public float shakeY;
        //public float shakeSpeed;

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
					GetComponent<Animator>().Play("Idle",-1,0f);
					GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = true;
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
			else if(GetComponent<Animator>() != null && GetComponent<Animator>().enabled)
			{
				GetComponent<Animator>().enabled = false;
				doneWithAnimating = true;
			}
			else if(doneWithAnimating)
			{
				transform.localPosition = Vector3.back * 15;
                transform.localRotation = Quaternion.Euler(Vector3.zero);
				doneWithAnimating = false;
            }

            if (!animationPlayed && GameObject.Find("Loading Screen") != null && !GameObject.Find("Loading Screen").GetComponent<Image>().enabled)
            {
                animationPlayed = true;
                playingAnimation = true;
                GetComponent<Animator>().enabled = true;
            }

			// Rotate controls
			if ((Input.GetMouseButton(0) || Input.GetMouseButton(1)) && !playingAnimation && cameraEnabled) //Disable Camera for Introduction Level
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

            //Set Camera void
            ////if (shakeCamera)
            //{
            //    Vector2 _newPosition = new Vector2(0, shakeY);
            //    if (shakeY < 0)
            //    {
            //        shakeY *= shakeSpeed;
            //    }
            //    shakeY = -shakeY;
            //    transform.Translate(_newPosition, Space.Self);
            //}
		}
	}
}