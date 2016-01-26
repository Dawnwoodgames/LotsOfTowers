using UnityEngine;

namespace Nimbi.Environment {
	public class Buoy : MonoBehaviour {
        private float awakeY;

        public bool red = false;
        public float speed = 0.5f;

        public void Awake() {
            this.awakeY = transform.localPosition.y;
        }

        public void FixedUpdate() {
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.PingPong(Time.timeSinceLevelLoad * speed, awakeY), transform.localPosition.z);
        }
	}
}