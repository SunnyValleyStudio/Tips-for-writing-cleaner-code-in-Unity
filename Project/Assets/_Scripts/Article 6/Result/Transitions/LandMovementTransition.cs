using System;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Waits the duration of Land Animation before transitioning.
    /// One downside is that if we modify Land Animation we need to manually change it.
    /// An alternative would be to ask Animator through AgentAnimations script about this length
    /// or in some other way obtain it directly from the animation.
    /// </summary>
    public class LandMovementTransition : ITransitionRule
    {
        //Land Animation length in seconds
        private float m_landAnimationDelay = 0.533f;

        public Type NextState => typeof(MovementState);

        public bool ShouldTransition(float deltaTime)
        {
            if (m_landAnimationDelay <= 0)
            {
                return true;
            }
            m_landAnimationDelay -= deltaTime;
            return false;
        }
    }
}

