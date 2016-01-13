using UnityEngine;

namespace Nimbi.VFX {
    public sealed class FlameBreath : MonoBehaviour {
        private ParticleSystem.Particle[] particles;
        private ParticleSystem system;

        public new Light light;

        public void Update() {
            system = GetComponent<ParticleSystem>();
            particles = new ParticleSystem.Particle[system.particleCount];
            system.GetParticles(particles);

            for (int i = 0; i < particles.Length; i++) {
                Debug.Log(particles[i].position);
                Instantiate(light, system.transform.position + particles[i].position, Quaternion.identity);
            }

            system.SetParticles(particles, system.particleCount);
        }
    }
}