using System;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// Transitions to the FallState the moment velocity Y is less then 0.
    /// It requires a delay because of how JumpState works - that it sets the velocity
    /// during the first update - but the Transition check runs before it. That is why we need the delay.
    /// </summary>
    public class JumpFallTransition : ITransitionRule
    {
        public Type NextState => typeof(FallState);

        private IAgentMover m_mover;
        //Delay to let the JumpSate call Move() on the m_mover so that we don't immediately
        //exit from Jump to Fall state
        private float m_checkDelay = 0.2f;

        public JumpFallTransition(IAgentMover mover)
        {
            m_mover = mover;
        }

        public bool ShouldTransition(float deltaTime)
        {
            if (m_checkDelay <= 0)
                return m_mover.CurrentVelocity.y <= 0;
            m_checkDelay -= deltaTime;
            return false;
        }
    }
}