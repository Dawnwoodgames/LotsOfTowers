using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction
{
	public class CompleteFan : MonoBehaviour
	{
		private Animator animator;
		private bool inTrigger = false;
		private bool isHeavy = false;
		private Player player;

		void Start()
		{
			animator = GetComponentInParent<Animator>();
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		}

		void OnTriggerStay(Collider coll)
		{
			if (coll.tag == "Player")
			{
				inTrigger = true;
			}
		}

		void OnTriggerExit(Collider coll)
		{
			if (coll.tag == "Player")
			{
				inTrigger = false;
			}
		}

		void Update()
		{
			isHeavy = player.Onesie.isHeavy;

			if (inTrigger && isHeavy)
			{
				animator.SetBool("GoingDown", true);
				animator.SetBool("GoingUp", false);
			}
			else
			{
				animator.SetBool("GoingDown", false);
				animator.SetBool("GoingUp", true);
			}
		}
	}
}