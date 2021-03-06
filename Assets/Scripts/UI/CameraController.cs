﻿using System;
using UnityEngine;

namespace Nimbi.UI {
	[RequireComponent(typeof(Camera))]
	public class CameraController : MonoBehaviour {

		public Transform mount;
		public float speed;

		public void Update() {
			if (mount != null && speed > 0) {
				transform.position = Vector3.Lerp(transform.position, mount.position, speed * Time.smoothDeltaTime);
				transform.rotation = Quaternion.Slerp(transform.rotation, mount.rotation, speed * Time.smoothDeltaTime);
			}
		}
	}
}