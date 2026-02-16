using System;
using UnityEngine;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// StateFactory for Player. It adds JumpState and other states specific to the
    /// Player character. It also expects to receive PlayerStateFactoryData as
    /// without it it can't initialize the Player specific states.
    /// It inherits from StateFactory so that we can reuse the base implementation
    /// of StateFactory.
    /// </summary>
    public class PlayerStateFactory : StateFactory
    {
        public PlayerStateFactory(PlayerStateFactoryData stateFactoryData) : base(stateFactoryData)
        {
        }

        public override State CreateState(Type stateType)
        {
            PlayerStateFactoryData m_playerStateFactoryData = (PlayerStateFactoryData)m_stateFactoryData;
            State newState = null;
            if (stateType == typeof(JumpState))
            {
                newState = new JumpState(m_playerStateFactoryData.AgentMover, m_playerStateFactoryData.AgentAnimations, m_playerStateFactoryData.MovementInput, m_playerStateFactoryData.AgentStats);
                newState.AddTransition(new JumpFallTransition(m_playerStateFactoryData.AgentMover));
            }
            else if (stateType == typeof(InteractState))
            {
                newState = new InteractState(m_playerStateFactoryData.AgentAnimations, m_playerStateFactoryData.InteractDetector);
                newState.AddTransition(new InteractMoveTransition());
            }
            else if (stateType == typeof(DrawWeaponState))
            {
                newState = new DrawWeaponState(m_playerStateFactoryData.WeaponHelper, m_playerStateFactoryData.AgentMover, m_playerStateFactoryData.AgentAnimations, m_playerStateFactoryData.MovementInput, m_playerStateFactoryData.GroundDetector, m_playerStateFactoryData.AgentStats);
                newState.AddTransition(new DelayedTransition(0.2f, typeof(MovementState)));
            }
            else if (stateType == typeof(AttackState))
            {
                newState = new AttackState(m_playerStateFactoryData.AgentAnimations, m_playerStateFactoryData.AgentMover, m_playerStateFactoryData.AgentStats, m_playerStateFactoryData.AgentGameObject, m_playerStateFactoryData.HitDetector, 0.03f);
                newState.AddTransition(new DelayedTransition(0.35f, typeof(MovementState)));
            }
            else if (stateType == typeof(GetHitState))
            {
                newState = new GetHitState(m_playerStateFactoryData.AgentAnimations, m_playerStateFactoryData.AgentMover, m_playerStateFactoryData.AgentStats);
                newState.AddTransition(new DelayedTransition(0.32f, typeof(MovementState)));
            }
            else
            {
                newState = base.CreateState(stateType);
                if (stateType == typeof(MovementState))
                {
                    newState.AddTransition(new JumpTransition(m_playerStateFactoryData.JumpInput));
                    if (m_playerStateFactoryData.WeaponHelper.HasWeapon == false || m_playerStateFactoryData.WeaponHelper.IsWeaponHolstered)
                    {
                        newState.AddTransition(new MoveInteractTransition(m_playerStateFactoryData.InteractInput, m_playerStateFactoryData.InteractDetector));
                    }
                    if (m_playerStateFactoryData.WeaponHelper.HasWeapon && m_playerStateFactoryData.WeaponHelper.IsWeaponHolstered == false)
                    {
                        newState.AddTransition(new MoveAttackTransition(m_playerStateFactoryData.AttackInput));
                    }
                    if (m_playerStateFactoryData.WeaponHelper.HasWeapon)
                    {
                        newState.AddTransition(new MoveDrawWeaponTransition(m_playerStateFactoryData.ToggleWeapon));
                    }
                    newState.AddTransition(new GetHitTransition(m_playerStateFactoryData.AgentHealth));
                }
            }

            return newState;
        }
    }

    /// <summary>
    /// We inherit from StateFactoryData to add Player specific data only while
    /// reusing the fields already defined inside the StateFactoryData.
    /// </summary>
    public class PlayerStateFactoryData : StateFactoryData
    {
        public IAgentJumpInput JumpInput { get; set; }
        public IAgentInteractInput InteractInput { get; set; }
        public IAgentToggleWeaponInput ToggleWeapon { get; set; }
        public IAgentAttackInput AttackInput { get; set; }
        public InteractionDetector InteractDetector { get; set; }
        public HitDetector HitDetector { get; set; }
        public WeaponHelper WeaponHelper { get; set; }
        public GameObject AgentGameObject { get; set; }

        public Health AgentHealth { get; set; }
    }
}