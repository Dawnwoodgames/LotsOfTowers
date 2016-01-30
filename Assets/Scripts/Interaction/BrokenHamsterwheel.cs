using UnityEngine;
using System.Collections;
using Nimbi.Actors;
using Nimbi.Interaction.Triggers;

namespace Nimbi.Interaction
{
	public class BrokenHamsterwheel : MonoBehaviour
	{
		public RotateTrigger rotateTrigger;
		public WaterHole waterHole;
		public float maxDamage = 6.5f;
        public GameObject bumpWall;

		private Player player;
		private float damage = 0;
		private bool broken = false;
		private bool inTrigger = false;

		void Start()
		{
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		}

		void FixedUpdate()
		{
			if (rotateTrigger.GetPlayerRunning() && !broken)
			{
				damage += 1 * Time.deltaTime;
				if (damage > (maxDamage / 4))
				{
					transform.Rotate(new Vector3(0, 0, .05f));
					if (damage > (maxDamage / 2))
					{
						transform.Rotate(new Vector3(0, 0, .15f));
						if (damage > maxDamage)
						{
							waterHole.waterRising = false;
							transform.Rotate(new Vector3(0, 0, .3f));
						}
					}
				}
			}
			else if (!broken && damage > maxDamage)
			{
				if (player.Onesie.isHeavy && inTrigger)
				{
					broken = true;
				}
			}
			else if (broken)
			{
				player.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * 20f, ForceMode.Impulse);

				gameObject.AddComponent<Rigidbody>();
				gameObject.GetComponent<Rigidbody>().useGravity = true;
				gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
				gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

				gameObject.GetComponent<MeshCollider>().convex = true;

				Destroy(rotateTrigger.gameObject);
				Destroy(transform.parent.GetChild(1).gameObject);
                Destroy(bumpWall);
				Destroy(this);
			}
		}

		private void OnTriggerStay(Collider coll)
		{
			if (coll.tag == "Player")
			{
				inTrigger = true;
			}
		}

		private void OnTriggerExit(Collider coll)
		{
			if (coll.tag == "Player")
			{
				inTrigger = false;
			}
		}
	}
}
