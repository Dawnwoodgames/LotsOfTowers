using UnityEngine;

namespace Nimbi.Environment {
	public class Buoy : MonoBehaviour {
        private float awakeY;
        private float deviation;

        public bool red = false;
        public float speed = 0.5f;

        public void Awake() {
            this.awakeY = transform.localPosition.y;
            this.deviation = Random.Range(0f, 0.5f);
        }

        public void FixedUpdate() {
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.PingPong(Time.timeSinceLevelLoad * speed + deviation, awakeY), transform.localPosition.z);
        }
	}
}