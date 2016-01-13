using UnityEngine;

namespace Nimbi.VFX {
    public sealed class FlameBreath : MonoBehaviour {
        private ParticleSystem.Particle[] particles;
        private ParticleSystem system;

        public new Light light;

        public void Awake() {
            this.system = GetComponent<ParticleSystem>();
        }

        public void Update() {
            particles = new ParticleSystem.Particle[system.particleCount];
            system.GetParticles(particles);

            for (int i = 0; i < particles.Length; i++) {
                Instantiate(light, particles[i].position, Quaternion.identity);
            }

            system.SetParticles(particles, system.particleCount);
        }
    }
}