using System;
namespace Tips.Part_6_End
{
    /// <summary>
    /// Implementation of Interact Input following the Interface Segregation Principle. We don't use inheritance here because Movement and Interact input
    /// or any other input don't depend on each other.
    /// </summary>
    public interface IAgentInteractInput
    {
        event Action OnInteract;
    }
}
