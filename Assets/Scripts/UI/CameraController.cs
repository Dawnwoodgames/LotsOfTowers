using System;
using UnityEngine;

namespace LotsOfTowers.UI {
	[RequireComponent(typeof(Camera))]
	public sealed class CameraController : MonoBehaviour {
		private Transform mount;

		public float speed = 0.1f;

		public void Awake() {
			SetActiveMenu(GameObject.Find("Menu/Main"));
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