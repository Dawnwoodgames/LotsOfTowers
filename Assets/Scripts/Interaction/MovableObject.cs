using UnityEngine;
using LotsOfTowers.Actors;

namespace LotsOfTowers.Interaction
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