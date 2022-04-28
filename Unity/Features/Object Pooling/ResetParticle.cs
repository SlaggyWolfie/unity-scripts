using UnityEngine;

namespace Slaggy.Unity.Pooling
{
    /// <summary>
    /// Class used to reset the particle for reuse with the object pool/factory
    /// </summary>
    public class ResetParticle : MonoBehaviour, IPooledObject
    {
        public void OnGetFromPool()
        {
            // Eh, it was here before!?
            gameObject.SetActive(false);
            gameObject.SetActive(true);

            ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
            foreach (var particle in particles) particle.Play(true);
        }

        public void OnReturnToPool()
        {
            ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
            foreach (var particle in particles)
                particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}
