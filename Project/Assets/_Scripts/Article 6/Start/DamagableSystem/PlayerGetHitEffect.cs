using UnityEngine;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// This script is here to trigger the PlayerTakeDamageEffect 
    /// when the player gets hit.
    /// </summary>
    public class PlayerGetHitEffect : MonoBehaviour, IDamagable
    {
        [SerializeField]
        PlayerTakeDamageEffect m_takeDamageEffect;

        public void TakeDamage(DamageData damageData)
        {
            m_takeDamageEffect.TriggerHitEffect();
        }
    }
}