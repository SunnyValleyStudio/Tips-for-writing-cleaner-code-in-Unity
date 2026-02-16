using UnityEngine;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// IDamagable implementation that plays an audio clip 
    /// when the object takes damage.
    /// </summary>
    public class PlayAudioGetHitEffect : MonoBehaviour, IDamagable
    {
        [SerializeField]
        private AudioSource m_audioSource;

        public void TakeDamage(DamageData damageData)
        {
            m_audioSource.Play();
        }
    }
}