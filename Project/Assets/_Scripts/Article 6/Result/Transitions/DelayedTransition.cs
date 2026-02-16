using System;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Transition that waits for a delay before transitioning to 
    /// the next state.
    /// </summary>
    public class DelayedTransition : ITransitionRule
    {
        // Delay before transitioning to the next state.
        private float m_animationDelay;
        //The next state to transition to.
        public Type NextState { private set; get; }

        public DelayedTransition(float delay, Type nextStateType)
        {
            m_animationDelay = delay;
            NextState = nextStateType;
        }
        public bool ShouldTransition(float deltaTime)
        {
            if (m_animationDelay <= 0)
                return true;
            m_animationDelay -= deltaTime;
            return false;
        }
    }
}