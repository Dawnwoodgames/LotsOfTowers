﻿using UnityEngine;
using LotsOfTowers.Actors;

namespace LotsOfTowers.Platform {
	[RequireComponent(typeof(Rigidbody))]
	public sealed class MovableObject : MonoBehaviour {
		private Player player;
		private Rigidbody rigidBody;

		public void Awake() {
			this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
			this.rigidBody = GetComponent<Rigidbody>();
		}

		public void Start() {
			rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
		}

		public void Update() {
			rigidBody.isKinematic = !player.CanMoveObjects;
		}
	}
}