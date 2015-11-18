using UnityEngine;
using System.Collections;
using Assets.Scripts.Framework;
using System.Collections.Generic;
using System.Linq;
using LotsOfTowers.Actors;

namespace LotsOfTowers.Interaction.Triggers
{
	[RequireComponent(typeof(BoxCollider))]

	public class WindTrigger : MonoBehaviour
	{
		public static State state = State.Deactive;
		public float forcePower = 50;

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
				Logger.Log(ex.Message, LogType.Exception);
				throw;
			}
		}

		void FixedUpdate()
		{
			if (state == State.Active)
			{
				windParticles.enableEmission = true;
				if(currentCollisions.Count > 1 && currentCollisions.Any(player => player.tag == "Player"))
				{
					try
					{
						if (currentCollisions.Single(player => player.name == "Player").GetComponent<Player>().Onesie.isElephant)
						{
							currentCollisions.Single(ff => ff.name == "FloatingFloor").GetComponent<Rigidbody>().AddForce(Vector3.up * 50, ForceMode.Acceleration);
						}
						else
						{
							currentCollisions.Single(ff => ff.name == "FloatingFloor").GetComponent<Rigidbody>().AddForce(Vector3.up * 150, ForceMode.Acceleration);
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
						item.GetComponent<Rigidbody>().AddForce(Vector3.up * 50, ForceMode.Acceleration);
					}
				}
			}
			else if (state == State.Deactive)
			{
				windParticles.enableEmission = false;
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