using UnityEngine;

namespace Nimbi.VFX {
    public sealed class FlameBreath : MonoBehaviour {
        private ParticleSystem.Particle[] particles;
        private GameObject root;
        private ParticleSystem system;

        public new Light light;

        public void Awake() {
            this.root = GameObject.Find("Nimbi/VFX");
        }

        public void Update() {
            system = GetComponent<ParticleSystem>();
            particles = new ParticleSystem.Particle[system.particleCount];
            system.GetParticles(particles);

            for (int i = 0; i < particles.Length; i++) {
                if (i % 4 == 0) {
                    ((Light)Instantiate(light, particles[i].position, Quaternion.identity))
                        .gameObject.transform.SetParent(root.transform, false);
                }
            }

            system.SetParticles(particles, system.particleCount);
        }
    }
}