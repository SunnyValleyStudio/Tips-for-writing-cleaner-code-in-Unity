using System;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// Implementation of Wave Input following the Interface Segregation Principle. We don't use inheritance here because Movement and Wave input
    /// or any other input don't depend on each other.
    /// </summary>
    public interface IAgentWaveInput
    {
        event Action OnWaveInput;
    }

}