using System;
using System.Collections.Generic;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Abstract definition of a State. Allows us to separate the Agents behaviors from the Agent script.
    /// Most State Patterns requires at least 3 methods: OnEnter, OnExit and Update. Using OnTransitionEvent
    /// allows us to separate State from knowing anything about the Agent itself and how it Transitions between
    /// different states.
    /// </summary>
    public abstract class State
    {
        protected List<ITransitionRule> _transitionRules = new();

        public event Action<Type> OnTransition;
        public abstract void Enter();

        //Update method will be called by oru Agent script. This allows us to run additional logic like ShouldTransition check
        //before the specific StateUpdate() is called (which is a protected, abstract method that each state implements on its own.
        public void Update(float deltaTime)
        {
            if (ShouldTransition(deltaTime))
                return;
            StateUpdate(deltaTime);
        }

        protected abstract void StateUpdate(float deltaTime);

        public abstract void Exit();
        private bool ShouldTransition(float deltaTime)
        {
            foreach (ITransitionRule rule in _transitionRules)
            {
                if (rule.ShouldTransition(deltaTime))
                {
                    foreach (ITransitionRule transition in _transitionRules)
                    {
                        //Since State class still depends on abstraction we can add this check here.
                        //We could instead implement another Strategy Pattern to handle this logic without us
                        //having to modify this code later if we need to add another type of transition
                        if (transition is IEventTransitionRule eventTransition)
                        {
                            eventTransition.Unsubscribe();
                        }
                    }
                    OnTransition?.Invoke(rule.NextState);
                    return true;
                }
            }
            return false;
        }
        public void AddTransition(ITransitionRule rule)
        {
            _transitionRules.Add(rule);
            //This logic ensures that each transition subscribes to the events that it depends on.
            //It automates the process but it also required us to modify this logic. This shows that we could improve this design.
            if (rule is IEventTransitionRule eventTransition)
            {
                eventTransition.Subscribe();
            }
        }
    }

}