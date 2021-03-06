﻿using UnityEngine;
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
					transform.Rotate(new Vector3(0, 0, .15f));
					if (damage > (maxDamage / 2))
					{
						transform.Rotate(new Vector3(0, 0, .25f));
						if (damage > maxDamage)
						{
							transform.Rotate(new Vector3(0, 0, .4f));
						}
					}
				}
			}
			else if (!broken && damage > maxDamage)
			{
				if (player.Onesie.isHeavy)
				{
					broken = true;
				}
			}
			else if (broken)
			{
                gameObject.AddComponent<Rigidbody>();
                gameObject.GetComponent<Rigidbody>().mass = 0;
                gameObject.GetComponent<Rigidbody>().drag = 0;
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

                player.GetComponent<Rigidbody>().velocity = Vector3.zero;

                gameObject.GetComponent<MeshCollider>().convex = true;

                Destroy(rotateTrigger.gameObject);
                Destroy(transform.parent.GetChild(1).gameObject);
                Destroy(bumpWall);
                Destroy(this);
            }
		}
	}
}
