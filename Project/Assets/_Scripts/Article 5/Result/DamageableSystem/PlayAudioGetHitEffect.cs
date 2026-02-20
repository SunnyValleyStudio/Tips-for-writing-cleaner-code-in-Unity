using UnityEngine;

namespace Tips.Part_5_End
{
    /// <summary>
    /// IDamagable implementation that plays an audio clip 
    /// when the object takes damage.
    /// </summary>
    public class PlayAudioGetHitEffect : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private AudioSource m_audioSource;

        public void TakeDamage(DamageData damageData)
        {
            m_audioSource.Play();
        }
    }
}