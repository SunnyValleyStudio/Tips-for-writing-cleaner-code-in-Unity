using UnityEngine;

namespace Tips.Part_4_End
{
    /// <summary>
    /// MovementState happens when we are grounded and we want to stand idle or move around. There is some code duplication between Movement, Jump and Fall state.
    /// I will address that in Article 3
    /// </summary>
    public class MovementState : State
    {
        private IAgentMover m_mover;
        private GroundedDetector m_groundedDetector;
        private AgentAnimations m_agentAnimations;
        private IAgentMovementInput m_input;
        private AgentStats m_agentStats;

        private float m_verticalVelocity;
        private float m_animationMovementSpeed;

        //MovementHelper allows us to remove code duplication that existed in Movement, Jump and Fall state so that we could move in those states
        MovementHelper m_movementHelper = new();

        //One downside is a lengthy constructor. We could create a new object that encapsulates some of those.
        public MovementState(IAgentMover mover, GroundedDetector groundedDetector, AgentAnimations agentAnimations, IAgentMovementInput movementInput, AgentStats agentStats)
        {
            m_mover = mover;
            m_groundedDetector = groundedDetector;
            m_agentAnimations = agentAnimations;
            m_input = movementInput;
            m_agentStats = agentStats;
        }

        public override void Enter()
        {
            m_agentAnimations.SetBool(AnimationBoolType.Grounded, m_groundedDetector.Grounded);
        }

        public override void Exit()
        {
            return;
        }

        protected override void StateUpdate(float deltaTime)
        {

            if (m_groundedDetector.Grounded == false)
            {
                m_verticalVelocity += m_agentStats.Gravity * Time.deltaTime;
            }
            else
            {
                m_verticalVelocity = 0;
            }


            //float targetMovementSpeed = m_input.SprintInput ? m_agentStats.SprintSpeed : m_agentStats.MoveSpeed;
            //targetMovementSpeed = m_input.MovementInput == Vector2.zero ? 0 : targetMovementSpeed;
            //m_mover.Move(new Vector3(m_input.MovementInput.x, m_verticalVelocity, m_input.MovementInput.y), targetMovementSpeed);
            float targetMovementSpeed = m_movementHelper.PerformMovement(m_input, m_agentStats, m_mover, m_verticalVelocity);

            m_animationMovementSpeed = Mathf.Lerp(m_animationMovementSpeed, targetMovementSpeed, Time.deltaTime * m_agentStats.SpeedChangeRate);
            if (m_animationMovementSpeed < 0.01f)
                m_animationMovementSpeed = 0f;

            //play animations
            m_agentAnimations.SetFloat(AnimationFloatType.Speed, m_animationMovementSpeed);

        }
    }
}

