namespace Tips.Part_6_Start
{
    /// <summary>
    /// Agent implementation that allows the NPC to wave at the player beside having the default states defined
    /// inside the abstract Agent class
    /// </summary>
    public class NPCAgent : Agent
    {
        private IAgentWaveInput m_waveInput;

        protected override void Awake()
        {
            base.Awake();
            m_waveInput = GetComponent<IAgentWaveInput>();
            _stateFactory = new NPCAgentStateFactory(new NPCStateFactoryData()
            {
                AgentStats = m_agentStats,
                MovementInput = m_input,
                GroundDetector = m_groundDetector,
                AgentAnimations = m_agentAnimations,
                AgentMover = m_mover,
                WaveInput = m_waveInput
            });
        }

    }
}