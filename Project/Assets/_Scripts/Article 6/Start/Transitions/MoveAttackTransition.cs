using System;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// Transition rule that will trigger the transition when the attack input
    /// is provided.
    /// </summary>
    public class MoveAttackTransition : IEventTransitionRule
    {
        IAgentAttackInput m_attackInput;
        bool m_shouldTransition;
        public Type NextState => typeof(AttackState);

        public MoveAttackTransition(IAgentAttackInput attackInput)
        {
            m_attackInput = attackInput;
        }

        public bool ShouldTransition(float deltaTime)
        {
            return m_shouldTransition;
        }

        private void HandleAttackInput()
        {
            m_shouldTransition = true;
        }

        public void Subscribe()
        {
            m_attackInput.OnAttackInput += HandleAttackInput;
        }

        public void Unsubscribe()
        {
            m_attackInput.OnAttackInput -= HandleAttackInput;
        }
    }
}