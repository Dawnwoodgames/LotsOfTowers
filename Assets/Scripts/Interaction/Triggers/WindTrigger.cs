using UnityEngine;
using System.Collections;
using Assets.Scripts.Framework;
using System.Collections.Generic;

namespace LotsOfTowers.Interaction.Triggers
{
	public class WindTrigger : MonoBehaviour
	{
		public static State state;
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
				foreach (GameObject item in currentCollisions)
				{
					//Add force so the object goes up
					item.GetComponent<Rigidbody>().AddForce(Vector3.up * forcePower);
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
				currentCollisions.Add(col.gameObject);
			}
		}

		//Check which object leaves the collider 
		void OnTriggerExit(Collider col)
		{
			// Remove the GameObject collided with from the list.
			if (col.GetComponent<Rigidbody>())
			{
				currentCollisions.Remove(col.gameObject);
			}
		}
	}
}