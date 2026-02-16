using UnityEngine;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// PlayerAgent represents our Players behavior. We make it depend on
    /// the Jump input and Interaction input and detector.
    /// </summary>
    public class PlayerAgent : Agent
    {
        private IAgentJumpInput m_jumpInput;

        private IAgentInteractInput m_interactInput;

        private IAgentAttackInput m_attackInput;

        private InteractionDetector m_interactDetector;

        [SerializeField]
        private WeaponHelper m_weaponHelper;

        private IAgentToggleWeaponInput m_toggleWeaponInput;

        [SerializeField]
        private HitDetector m_hitDetector;

        private Health m_health;

        protected override void Awake()
        {
            base.Awake();
            m_jumpInput = GetComponent<IAgentJumpInput>();
            m_interactInput = GetComponent<IAgentInteractInput>();
            m_interactDetector = GetComponent<InteractionDetector>();
            m_weaponHelper = GetComponent<WeaponHelper>();
            m_toggleWeaponInput = GetComponent<IAgentToggleWeaponInput>();
            m_attackInput = GetComponent<IAgentAttackInput>();
            m_hitDetector = GetComponent<HitDetector>();
            m_health = GetComponent<Health>();

            _stateFactory = new PlayerStateFactory(
                new PlayerStateFactoryData
                {
                    AgentStats = m_agentStats,
                    MovementInput = m_input,
                    GroundDetector = m_groundDetector,
                    AgentAnimations = m_agentAnimations,
                    AgentMover = m_mover,
                    JumpInput = m_jumpInput,
                    InteractInput = m_interactInput,
                    InteractDetector = m_interactDetector,
                    ToggleWeapon = m_toggleWeaponInput,
                    AttackInput = m_attackInput,
                    WeaponHelper = m_weaponHelper,
                    AgentGameObject = gameObject,
                    HitDetector = m_hitDetector,
                    AgentHealth = m_health
                });
        }

        protected override void Update()
        {
            base.Update();
            m_interactDetector.DetectInteractable();
        }
    }
}