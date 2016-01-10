using UnityEngine;
using Nimbi.Actors;

namespace Nimbi.Interaction
{
	[RequireComponent(typeof(Rigidbody))]
	public class MovableObject : MonoBehaviour
	{
		private Player player;
		private Rigidbody rigidBody;
		
		public void Awake()
		{
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
			rigidBody = GetComponent<Rigidbody>();
			rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
		}
		
		public void Update()
		{
			rigidBody.isKinematic = !player.Onesie.canMoveObjects;
		}
	}
}