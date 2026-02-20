using System;
using UnityEngine;

namespace Tips.Part_5_End
{
    /// <summary>
    /// Health script that can be affected when an object takes damage.
    /// CurrentHealth holds the health value.
    /// OnHit event is invoked when the object takes damage to trigger
    /// state transition to GetHitState or DeathState.
    /// </summary>
    public class Health : MonoBehaviour, IDamageable
    {
        [field: SerializeField]
        public int CurrentHealth { get; private set; }

        [SerializeField]
        private int m_maxHealth = 2;
        [SerializeField]
        private bool m_isInvincible = false;

        public event Action OnHit;

        private void Awake()
        {
            CurrentHealth = m_maxHealth;
        }

        /// <summary>
        /// Additional method just to show that despite inheriting from
        /// IDamagable interface the Health script can still have its own
        /// methods.
        /// </summary>
        /// <param name="isInvincible"></param>
        public void SetInvincible(bool isInvincible)
            => m_isInvincible = isInvincible;

        /// <summary>
        /// Example of how we can leverage IDamagable interface to modify
        /// the health of an object.
        /// </summary>
        /// <param name="damageData"></param>
        public void TakeDamage(DamageData damageData)
        {
            if (m_isInvincible)
                return;
            CurrentHealth -= damageData.DamageAmount;
            OnHit?.Invoke();
        }
    }
}