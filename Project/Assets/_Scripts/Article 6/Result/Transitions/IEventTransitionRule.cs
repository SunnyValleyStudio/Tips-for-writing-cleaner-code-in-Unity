namespace Tips.Part_6_End
{
    /// <summary>
    /// Defines an abstraction of a Transition Rule that can listen to events. We use Inheritance here since a rule still needs to define its check and
    /// a state to transition to. So inheritance allows us to avoid code duplication. Working with events is tricky as if we forget to unassign our method
    /// it will cause an Exception that can be tricky to debug. That is why we need Unsubscribe and Unsubscribe methods. 
    /// </summary>
    public interface IEventTransitionRule : ITransitionRule
    {
        void Subscribe();
        void Unsubscribe();
    }
}