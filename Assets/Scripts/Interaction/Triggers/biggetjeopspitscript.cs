using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction.Triggers
{
	public class biggetjeopspitscript : MonoBehaviour
	{
		private ParticleSystem vuurtje;
		private Player player;
		private GameObject pigModel;
		private GameObject pigColor;
		private bool rotating = false;
		private Color newColor;

		void Start()
		{
			vuurtje = GetComponentInChildren<ParticleSystem>();
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
			pigModel = GameObject.FindGameObjectWithTag("PigModel");
			pigColor = GameObject.FindGameObjectWithTag("PigColor");
			vuurtje.Stop();
			ColorUtility.TryParseHtmlString("#492200FF", out newColor);
		}
		void FixedUpdate()
		{
			if (rotating)
			{
				pigModel.transform.Rotate(Vector3.forward * 2);
				pigColor.GetComponent<SkinnedMeshRenderer>().material.color = Color.Lerp(pigColor.GetComponent<SkinnedMeshRenderer>().material.color, newColor, Time.deltaTime);
			}
		}

		private void OnTriggerStay(Collider coll)
		{
			if (coll.tag == "Fire")
			{
				vuurtje.Play();
				rotating = true;

				StartCoroutine(WaitForDropping());
			}
		}

		private IEnumerator WaitForDropping()
		{
			yield return new WaitForSeconds(2);

			//Wait couple of seconds and rotate biggetje?
			GetComponentInChildren<Rigidbody>().useGravity = true;
		}
	}
}