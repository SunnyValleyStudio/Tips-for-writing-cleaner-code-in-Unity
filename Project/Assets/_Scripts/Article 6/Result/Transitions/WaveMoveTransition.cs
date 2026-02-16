using System;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Transitions from Movement to Wave state based on the transition length.
    /// We should merge the LandMovement, InteractMovement and this transitions into a more abstract "DelayedTransition"
    /// to avoid code duplication.
    /// </summary>
    public class WaveMoveTransition : ITransitionRule
    {
        private float m_waveAnimationDelay = 1.4f;
        public Type NextState => typeof(MovementState);

        public bool ShouldTransition(float deltaTime)
        {
            if (m_waveAnimationDelay <= 0)
                return true;
            m_waveAnimationDelay -= deltaTime;
            return false;
        }
    }
}

