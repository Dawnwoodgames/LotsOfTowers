using UnityEngine;
using System.Collections;
using System;
using LotsOfTowers.CameraControl;
using LotsOfTowers.Interaction;

namespace LotsOfTowers.Interaction.Triggers
{
	public class relocateHamsterBall : MonoBehaviour
	{
		private GameObject player;
		private GameObject ball;
		private GameObject focusView;
		private GameObject teleport;

		void Start()
		{
			player = GameObject.FindGameObjectWithTag("Player");
			ball = GameObject.Find("HamsterBall");
			focusView = GameObject.Find("CenterFocus");
		}

		void OnTriggerEnter(Collider coll)
		{
			if (coll.tag == "HamsterBall")
			{
				coll.transform.localPosition = new Vector3(1.6f, -13f, 30f);
				coll.transform.localRotation = Quaternion.Euler(0, 90, 0);
				coll.GetComponent<Rigidbody>().isKinematic = true;

				StartCoroutine(WaitForLevelSlider());
			}
		}

		IEnumerator WaitForLevelSlider()
		{
			yield return new WaitForSeconds(2);
			ExitHamsterBall();
		}

		private void ExitHamsterBall()
		{
			ball.tag = "Untagged";
			ball.GetComponentInParent<HamsterBall>().playerInside = false;

			player.transform.parent = null;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;

            player.transform.localPosition = new Vector3(ball.transform.position.x, ball.transform.position.y + 1f, ball.transform.position.z - 1f);
            player.GetComponent<CapsuleCollider>().enabled = true;
            player.transform.localScale = new Vector3(1, 1, 1);
			focusView.GetComponent<CameraFollowScript>().SetCameraFocus(player);
			
			player.GetComponent<Rigidbody>().useGravity = true;
		}
	}
}
