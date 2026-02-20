using UnityEngine;

namespace Tips.Part_5_End
{
    /// <summary>
    /// Spawns Wood Chip particles when the Player hits the Tree object
    /// </summary>
    public class SpawnParticleEffect : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private ParticleSystem m_particlesPrefab;

        private ParticleSystem m_particleSystem;

        private void Awake()
        {
            InitializeParticleSystem();
        }

        private void InitializeParticleSystem()
        {
            m_particleSystem = Instantiate(m_particlesPrefab, Vector3.zero, Quaternion.identity, transform);
            m_particleSystem.Stop();
        }

        public void SpawnParticles(Vector3 position, Vector3 normal)
        {
            if (m_particleSystem.isPlaying)
            {
                m_particleSystem.Stop();
            }

            m_particleSystem.transform.position = position;
            m_particleSystem.transform.rotation = Quaternion.LookRotation(normal);

            m_particleSystem.Play();
        }

        /// <summary>
        /// We calculate here the normal (direction from the Tree object to the
        /// player) and spawn the particles in a way that it shoots particles
        /// in the direction of the player. We do that just to show how we can
        /// leverage the extra Sender parameter passed inside the DamageData 
        /// object
        /// </summary>
        /// <param name="damageData"></param>
        public void TakeDamage(DamageData damageData)
        {
            Vector3 position = transform.position;
            position.y = damageData.Sender.transform.position.y;
            Vector3 normal = (damageData.Sender.transform.position - transform.position).normalized;
            SpawnParticles(position, normal);
        }
    }
}