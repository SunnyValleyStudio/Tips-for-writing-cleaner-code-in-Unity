using System;

namespace Tips.Part_6_End
{
    public class MoveDrawWeaponTransition : IEventTransitionRule
    {
        public Type NextState => typeof(DrawWeaponState);
        private IAgentToggleWeaponInput m_toggleWeaponInput;
        private bool m_isTransitioning = false;
        public MoveDrawWeaponTransition(IAgentToggleWeaponInput toggleWeaponInput)
        {
            m_toggleWeaponInput = toggleWeaponInput;
        }

        public bool ShouldTransition(float deltaTime)
        {
            return m_isTransitioning;
        }

        public void Subscribe()
        {
            m_toggleWeaponInput.OnToggleWeaponInput += HandleTransition;
        }

        private void HandleTransition()
        {
            m_isTransitioning = true;
        }

        public void Unsubscribe()
        {
            m_toggleWeaponInput.OnToggleWeaponInput -= HandleTransition;
        }
    }
}