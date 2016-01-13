using UnityEngine;

namespace Nimbi.Framework {
    public sealed class AutoKill : MonoBehaviour {
        private float innerTimer;

        public float duration = 0.001f;

        public void Update() {
            innerTimer += Time.deltaTime;

            if (duration <= innerTimer) {
                Destroy(gameObject);
            }
        }
    }
}