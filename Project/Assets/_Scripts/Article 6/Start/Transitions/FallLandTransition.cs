using System;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// Checks if we are grounded before transitioning to the LandState
    /// </summary>
    public class FallLandTransition : ITransitionRule
    {
        public Type NextState => typeof(LandState);
        private GroundedDetector m_groundedDetector;

        public FallLandTransition(GroundedDetector groundedDetector)
        {
            m_groundedDetector = groundedDetector;
        }

        public bool ShouldTransition(float deltaTime)
        {
            return m_groundedDetector.Grounded;
        }
    }
}
