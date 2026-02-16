using System;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// Transition that utilizes IAgentJumpInput to listen to Input event in order to trigger the transition.
    /// It adds a bit of a delay so that we can't jump immediatelly after landing. Thic condition is optional.
    /// </summary>
    public class JumpTransition : ITransitionRule
    {
        public Type NextState => typeof(JumpState);
        public float m_jumpTimeout = 0.20f;

        private IAgentJumpInput m_jumpInput;

        public JumpTransition(IAgentJumpInput jumpInput)
        {
            m_jumpInput = jumpInput;
        }

        public bool ShouldTransition(float deltaTime)
        {
            if (m_jumpTimeout <= 0 && m_jumpInput.JumpInput)
                return true;
            m_jumpTimeout -= deltaTime;
            return false;
        }
    }
}
