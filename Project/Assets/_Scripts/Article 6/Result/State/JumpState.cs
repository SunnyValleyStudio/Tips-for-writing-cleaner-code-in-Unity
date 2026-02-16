using UnityEngine;

namespace Tips.Part_6_End
{
    /// <summary>
    /// JumpState triggers the Jump animation. In OnEnter it calculates the Jump vertical velocity. It is applied in the Update for consistency.
    /// This might cause some issues as some transition will need to wait until the Update runs before they can check the Y velocity. For example
    /// JumpFallTransition checks this value so we need to give it a small delay before it runs it check.
    /// </summary>
    public class JumpState : State
    {
        private AgentAnimations m_agentAnimations;
        private IAgentMovementInput m_input;
        private IAgentMover m_mover;
        private AgentStats m_agentStats;

        private float m_verticalVelocity;

        //MovementHelper allows us to remove code duplication that existed in Movement, Jump and Fall state so that we could move in those states
        MovementHelper m_movementHelper = new();

        public JumpState(IAgentMover mover, AgentAnimations agentAnimations, IAgentMovementInput input, AgentStats agentStats)
        {
            m_agentAnimations = agentAnimations;
            m_input = input;
            m_mover = mover;
            m_agentStats = agentStats;
        }

        public override void Enter()
        {
            m_verticalVelocity = Mathf.Sqrt(m_agentStats.JumpHeight * -2f * m_agentStats.Gravity);
            m_agentAnimations.SetTrigger(AnimationTriggerType.Jump);
            m_agentAnimations.SetBool(AnimationBoolType.Grounded, false);
        }

        public override void Exit()
        {
            m_agentAnimations.ResetTrigger(AnimationTriggerType.Jump);
        }

        protected override void StateUpdate(float deltaTime)
        {
            m_verticalVelocity += m_agentStats.Gravity * Time.deltaTime;

            //float targetMovementSpeed = m_input.SprintInput ? m_agentStats.SprintSpeed : m_agentStats.MoveSpeed;
            //targetMovementSpeed = m_input.MovementInput == Vector2.zero ? 0 : targetMovementSpeed;
            //m_mover.Move(
            //    new Vector3(m_input.MovementInput.x, m_verticalVelocity, m_input.MovementInput.y), targetMovementSpeed);
            m_movementHelper.PerformMovement(m_input, m_agentStats, m_mover, m_verticalVelocity);
        }
    }
}