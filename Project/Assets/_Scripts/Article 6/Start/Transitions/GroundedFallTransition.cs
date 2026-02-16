using System;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// Based on the grounded flag transitions us to FallState
    /// </summary>
    public class GroundedFallTransition : ITransitionRule
    {
        public Type NextState => typeof(FallState);
        private GroundedDetector m_groundedDetector;

        public GroundedFallTransition(GroundedDetector groundedDetector)
        {
            m_groundedDetector = groundedDetector;
        }

        public bool ShouldTransition(float deltaTime)
        {
            return m_groundedDetector.Grounded == false && m_groundedDetector.StairsGrounded == false;
        }
    }
}
