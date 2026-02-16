namespace Tips.Part_6_Start
{
    /// <summary>
    /// States that toggles weapon between hand and back of the character.
    /// It inherits from MovementState so that we can keep moving while drawing the weapon.
    /// </summary>
    public class DrawWeaponState : MovementState
    {
        private WeaponHelper m_weaponHelper;

        public DrawWeaponState(WeaponHelper weaponHelper, IAgentMover mover, AgentAnimations agentAnimations, IAgentMovementInput input, GroundedDetector groundedDetector, AgentStats agentStats) : base(mover, groundedDetector, agentAnimations, input, agentStats)
        {
            m_weaponHelper = weaponHelper;
        }

        public override void Enter()
        {
            base.Enter();
            m_weaponHelper.ToggleWeapon(!m_weaponHelper.IsWeaponHolstered);
        }
    }
}