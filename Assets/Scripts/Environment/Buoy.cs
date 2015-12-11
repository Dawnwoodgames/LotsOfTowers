﻿using UnityEngine;

namespace LotsOfTowers.Environment {
	[RequireComponent(typeof(MeshRenderer))]
	public sealed class Buoy : MonoBehaviour {
		private float internalClock;
		private float originalY;

		public Color Color {
			get { return GetComponent<MeshRenderer>().material.color; }
		}

		public void Awake() {
			this.internalClock = Random.Range(0f, 1f);
			this.originalY = transform.position.y;
		}

		public void FixedUpdate() {
			internalClock += Time.deltaTime;
			transform.position = new Vector3(transform.position.x, Mathf.Sin(internalClock) + originalY, transform.position.z);
		}
	}
}