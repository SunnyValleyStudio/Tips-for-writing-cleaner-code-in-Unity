using UnityEngine;

namespace Tips.Part_5_End
{
    /// <summary>
    /// This script is here to trigger the PlayerTakeDamageEffect 
    /// when the player gets hit.
    /// </summary>
    public class PlayerGetHitEffect : MonoBehaviour, IDamageable
    {
        [SerializeField]
        PlayerTakeDamageEffect m_takeDamageEffect;

        public void TakeDamage(DamageData damageData)
        {
            m_takeDamageEffect.TriggerHitEffect();
        }
    }
}