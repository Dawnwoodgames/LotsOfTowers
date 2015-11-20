using System;
using UnityEngine;

namespace LotsOfTowers.UI {
	[RequireComponent(typeof(Camera))]
	public sealed class MenuCameraController : MonoBehaviour {
		private Transform mount;

		public float speed = 0.1f;
		public float zoom = 1;

		public void Awake() {
			SetActiveMenu(FindObjectOfType<Canvas>().gameObject);
		}

		public void SetActiveMenu(GameObject menu) {
			try {
				mount = GameObject.Find("Menu/" + menu.name + "/Mounting Point").transform;
			} catch (Exception) { }
		}

		public void Update() {
			if (mount != null) {
				transform.position = Vector3.Lerp(transform.position, mount.position, speed);
				transform.rotation = Quaternion.Slerp(transform.rotation, mount.rotation, speed);
			}
		}
	}
}