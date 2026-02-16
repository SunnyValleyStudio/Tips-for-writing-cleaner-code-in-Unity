namespace Tips.Part_6_Start
{
    /// <summary>
    /// Abstraction of Jump Input. We are creating a new interface following the Interface Segregation Principle.
    /// We do this in order to avoid modifying the IAgentMovementInput since if any other script 
    /// that implements this interface it will break when we add a new property to it.
    /// </summary>
    public interface IAgentJumpInput
    {
        public bool JumpInput { get; }
    }
}

