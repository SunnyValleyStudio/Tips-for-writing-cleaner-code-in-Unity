using UnityEngine;

namespace Tips.Part_5_Start
{
    public class SpawnParticleEffect : MonoBehaviour
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
    }
}