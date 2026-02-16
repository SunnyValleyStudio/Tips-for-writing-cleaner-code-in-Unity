using UnityEngine;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Enemy agent class that will extend the Agent implementation with the
    /// AttackSate and that will make the Agent depend on IAgentAttackInput so
    /// that we can trigger the new state.
    /// </summary>
    public class EnemyAgent : Agent
    {
        [SerializeField]
        private IAgentAttackInput m_attackInput;

        [SerializeField]
        private HitDetector m_hitDetector;

        private Health m_health;

        protected override void Awake()
        {
            base.Awake();
            m_attackInput = GetComponent<IAgentAttackInput>();
            m_hitDetector = GetComponent<HitDetector>();
            m_health = GetComponent<Health>();

            _stateFactory = new EnemyNPCStateFactory(
                new EnemyNPCStateFactoryData
                {
                    AgentStats = m_agentStats,
                    MovementInput = m_input,
                    GroundDetector = m_groundDetector,
                    AgentAnimations = m_agentAnimations,
                    AgentMover = m_mover,
                    AttackInput = m_attackInput,
                    HitDetector = m_hitDetector,
                    AgentGameObject = gameObject,
                    HealthComponent = m_health
                });
        }

    }
}