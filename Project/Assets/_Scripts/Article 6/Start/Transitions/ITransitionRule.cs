using System;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// Abstract definition of a Transition so a condition for moving between different states. 
    /// ShouldTransition() represents this condition and NextState property defined the destination
    /// state if the condition returns true
    /// </summary>
    public interface ITransitionRule
    {
        bool ShouldTransition(float deltaTime);
        Type NextState { get; }
    }

}