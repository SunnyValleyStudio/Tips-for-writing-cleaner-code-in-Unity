using System;
using UnityEngine;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Adapter-style observer that observes when CurrentHealth of the Health component
    /// reaches 0 without modifying the Health class itself
    /// </summary>
    public class HealthDeathObserver : MonoBehaviour
    {
        [SerializeField] private Health m_health;

        public event Action OnDied;        
        void OnEnable()  { m_health.OnHit += HandleHit; }   
        void OnDisable() { m_health.OnHit -= HandleHit; }

        void HandleHit()
        {
            if (m_health.CurrentHealth <= 0) 
                OnDied?.Invoke();
        }

    }
}
