using System;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// State Factory for the NPC
    /// </summary>
    public class NPCAgentStateFactory : StateFactory
    {

        public NPCAgentStateFactory(NPCStateFactoryData stateFactoryData) : base(stateFactoryData)
        {
        }

        public override State CreateState(Type stateType)
        {
            NPCStateFactoryData m_npcStateFactoryData = (NPCStateFactoryData)m_stateFactoryData;
            State newState = null;
            if (stateType == typeof(WaveState))
            {
                newState = new WaveState(m_npcStateFactoryData.AgentAnimations);
                newState.AddTransition(new DelayedTransition(1.4f, typeof(MovementState)));
            }
            else
            {
                newState = base.CreateState(stateType);
                if (stateType == typeof(MovementState))
                {
                    newState.AddTransition(new MoveWaveTransition(m_npcStateFactoryData.WaveInput));
                }
            }
            return newState;
        }
    }

    public class NPCStateFactoryData : StateFactoryData
    {
        public IAgentWaveInput WaveInput { get; set; }
    }
}