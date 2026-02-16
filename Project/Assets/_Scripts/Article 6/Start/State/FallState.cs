using UnityEngine;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// Fall state triggers the Fall animation and applies gravity force to the character until we are again grounded.
    /// </summary>
    public class FallState : State
    {
        private IAgentMover m_mover;
        private AgentAnimations m_agentAnimations;
        private IAgentMovementInput m_input;
        private AgentStats m_agentStats;

        private float m_verticalVelocity;
        private float m_fallTimeoutDelta;
        private bool m_fallTransition = false;

        //MovementHelper allows us to remove code duplication that existed in Movement, Jump and Fall state so that we could move in those states
        MovementHelper m_movementHelper = new();

        public FallState(IAgentMover mover, AgentAnimations agentAnimations, IAgentMovementInput movementInput, AgentStats agentStats)
        {
            m_mover = mover;
            m_agentAnimations = agentAnimations;
            m_input = movementInput;
            m_agentStats = agentStats;
        }

        public override void Enter()
        {
            m_agentAnimations.SetBool(AnimationBoolType.Grounded, false);
            m_agentAnimations.SetTrigger(AnimationTriggerType.Fall);

        }

        public override void Exit()
        {
            m_agentAnimations.ResetTrigger(AnimationTriggerType.Fall);
            m_agentAnimations.SetBool(AnimationBoolType.Grounded, true);
        }

        protected override void StateUpdate(float deltaTime)
        {
            if (m_fallTransition == false && m_fallTimeoutDelta > 0)
            {
                m_fallTransition = true;
                m_fallTimeoutDelta = m_agentStats.FallTimeout;
            }
            else
            {
                m_fallTimeoutDelta -= Time.deltaTime;
                if (m_fallTimeoutDelta <= 0)
                {
                    m_agentAnimations.SetTrigger(AnimationTriggerType.Fall);
                }
                m_fallTransition = false;
            }
            m_verticalVelocity += m_agentStats.Gravity * Time.deltaTime;

            //float targetMovementSpeed = m_input.SprintInput ? m_agentStats.SprintSpeed : m_agentStats.MoveSpeed;
            //targetMovementSpeed = m_input.MovementInput == Vector2.zero ? 0 : targetMovementSpeed;
            //m_mover.Move(
            //    new Vector3(m_input.MovementInput.x, m_verticalVelocity, m_input.MovementInput.y), targetMovementSpeed);
            m_movementHelper.PerformMovement(m_input, m_agentStats, m_mover, m_verticalVelocity);

        }
    }
}
