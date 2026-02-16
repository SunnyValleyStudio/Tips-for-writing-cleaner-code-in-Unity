using UnityEngine;

namespace Tips.Part_6_End
{
    /// <summary>
    /// State that triggers Hit animation. It also stops player movement
    /// </summary>
    public class GetHitState : State
    {
        private AgentAnimations m_agentAnimations;
        private IAgentMover m_mover;
        private AgentStats m_agentStats;

        public GetHitState(AgentAnimations agentAnimations, IAgentMover mover, AgentStats agentStats)
        {
            m_agentAnimations = agentAnimations;
            m_mover = mover;
            m_agentStats = agentStats;
        }

        public override void Enter()
        {
            m_mover.Move(Vector3.zero, 0);
            m_agentStats.AnimationMovementSpeed = 0;
            m_agentAnimations.SetFloat(AnimationFloatType.Speed, 0);
            m_agentAnimations.SetTrigger(AnimationTriggerType.Hit);
        }

        public override void Exit()
        {
            return;
        }

        protected override void StateUpdate(float deltaTime)
        {
            return;
        }
    }
}