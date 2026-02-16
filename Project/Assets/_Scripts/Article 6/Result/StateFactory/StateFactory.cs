using System;

namespace Tips.Part_6_End
{
    /// <summary>
    /// The same code that previously resided in the Agent class inside the StateFactory(..) method
    /// </summary>
    public class StateFactory
    {
        /// <summary>
        /// We can share access to the StateFactoryData and assign to it specific data
        /// for each specific StateFactory implementation
        /// </summary>
        protected StateFactoryData m_stateFactoryData;
        public StateFactory(StateFactoryData stateFactoryData)
        {
            m_stateFactoryData = stateFactoryData;
        }

        public virtual State CreateState(Type stateType)
        {
            State newState = null;
            if (stateType == typeof(MovementState))
            {
                newState = new MovementState(m_stateFactoryData.AgentMover, m_stateFactoryData.GroundDetector, m_stateFactoryData.AgentAnimations, m_stateFactoryData.MovementInput, m_stateFactoryData.AgentStats);
                newState.AddTransition(new GroundedFallTransition(m_stateFactoryData.GroundDetector));
            }
            else if (stateType == typeof(FallState))
            {
                newState = new FallState(m_stateFactoryData.AgentMover, m_stateFactoryData.AgentAnimations, m_stateFactoryData.MovementInput, m_stateFactoryData.AgentStats);
                newState.AddTransition(new FallLandTransition(m_stateFactoryData.GroundDetector));
            }
            else if (stateType == typeof(LandState))
            {
                newState = new LandState(m_stateFactoryData.AgentAnimations, m_stateFactoryData.AgentStats);
                newState.AddTransition(new LandMovementTransition());
            }
            else
            {
                throw new Exception($"Type not handled {stateType}");
            }
            return newState;
        }
    }

    /// <summary>
    /// To pass the dependencies that our Sttes uses we need to create a new data class
    /// and pass it inside the StateFactory constructor
    /// </summary>
    public class StateFactoryData
    {
        public IAgentMover AgentMover { get; set; }
        public IAgentMovementInput MovementInput { get; set; }
        public GroundedDetector GroundDetector { get; set; }
        public AgentAnimations AgentAnimations { get; set; }
        public AgentStats AgentStats { get; set; }

    }
}