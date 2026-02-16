using System;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Transition that listens to the Health component 
    /// and triggers the GetHitState whenever OnHit event is called
    /// </summary>
    public class GetHitTransition : IEventTransitionRule
    {
        public Type NextState => typeof(GetHitState);
        private Health m_health;
        private bool m_shouldTransition = false;
        public GetHitTransition(Health health)
        {
            m_health = health;
        }

        public bool ShouldTransition(float deltaTime)
        {
            return m_shouldTransition;
        }

        public void Subscribe()
        {
            m_health.OnHit += TriggerTransition;
        }

        private void TriggerTransition()
        {
            m_shouldTransition = true;
        }

        public void Unsubscribe()
        {
            m_health.OnHit -= TriggerTransition;
        }
    }
}