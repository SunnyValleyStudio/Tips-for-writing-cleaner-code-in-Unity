using System;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Another transition that waits a specified delay before transition occurs.
    /// We should merge the LandMovement and this transitions into a more abstract "DelayedTransition"
    /// to avoid code duplication.
    /// </summary>
    public class InteractMoveTransition : ITransitionRule
    {
        private float m_interactAnimationDelay = 1.2f;
        public Type NextState => typeof(MovementState);

        public bool ShouldTransition(float deltaTime)
        {
            if (m_interactAnimationDelay <= 0)
                return true;
            m_interactAnimationDelay -= deltaTime;
            return false;
        }
    }
}
