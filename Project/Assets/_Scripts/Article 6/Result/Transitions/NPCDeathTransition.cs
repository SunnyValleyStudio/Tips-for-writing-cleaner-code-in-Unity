using System;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Transition that queries Health component to check if 
    /// the CurrentHealth is less than or equal to 0
    /// </summary>
    public class NPCDeathTransition : ITransitionRule
    {
        public Type NextState => typeof(NavMeshNPCDeathState);
        private Health m_health;

        public NPCDeathTransition(Health health)
        {
            m_health = health;
        }

        public bool ShouldTransition(float deltaTime)
        {
            return m_health.CurrentHealth <= 0;
        }
    }
}