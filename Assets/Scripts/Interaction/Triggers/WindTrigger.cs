using UnityEngine;
using System.Collections;
using Nimbi.Framework;
using System.Collections.Generic;
using System.Linq;
using Nimbi.Actors;

namespace Nimbi.Interaction.Triggers
{
	[RequireComponent(typeof(BoxCollider))]

	public class WindTrigger : MonoBehaviour
	{
		public static State state = State.Inactive;
		public float forcePower = 50;
		public float forcePowerWithoutOnesie = 150;

		// Declare and initialize a new List of GameObjects called currentCollisions.
		private List<GameObject> currentCollisions = new List<GameObject>();

		private ParticleSystem windParticles;

		private void Start()
		{
			try
			{
				windParticles = GetComponentInChildren<ParticleSystem>();
			}
			catch (System.Exception ex)
			{
				Nimbi.Framework.Logger.Log(ex);
				throw;
			}
		}

		void FixedUpdate()
		{
			if (state == State.Active)
			{
                ParticleSystem.EmissionModule em = windParticles.emission;
                em.enabled = true;

				currentCollisions = currentCollisions.Distinct().ToList();

				if(currentCollisions.Count > 1 && currentCollisions.Any(player => player.tag == "Player"))
				{
					try
					{
						if (currentCollisions.SingleOrDefault(player => player.tag == "Player").GetComponent<Player>().Onesie.isHeavy)
						{
							currentCollisions.SingleOrDefault(ff => ff.name == "FloatingFloor").GetComponent<Rigidbody>().AddForce(Vector3.up * forcePower, ForceMode.Acceleration);
                        }
						else
						{
							currentCollisions.SingleOrDefault(ff => ff.name == "FloatingFloor").GetComponent<Rigidbody>().AddForce(Vector3.up * forcePowerWithoutOnesie, ForceMode.Acceleration);
						}
					}
					catch (System.Exception)
					{
						throw;
					}
				}
				else
				{
					foreach (GameObject item in currentCollisions)
					{
                        //Add force so the object goes up
                        if (item.tag == "Player" && item.GetComponent<Player>().Onesie.isHeavy)
                            continue;
						item.GetComponent<Rigidbody>().AddForce(Vector3.up * 50, ForceMode.Acceleration);
					}
				}
			}
			else if (state == State.Inactive)
			{
                ParticleSystem.EmissionModule em = windParticles.emission;
                em.enabled = false;
            }
		}

		//Check which object collide with the wind capsule
		void OnTriggerEnter(Collider col)
		{
			// Add the GameObject collided with to the list.
			if (col.GetComponent<Rigidbody>())
			{
				if (!col.GetComponent<Rigidbody>().isKinematic)
				{
					currentCollisions.Add(col.gameObject);
				}
			}
		}

		//Check which object leaves the collider 
		void OnTriggerExit(Collider col)
		{
			// Remove the GameObject collided with from the list.
			if (col.GetComponent<Rigidbody>())
			{
				if (!col.GetComponent<Rigidbody>().isKinematic)
				{
					currentCollisions.Remove(col.gameObject);
				}
			}
		}
	}
}