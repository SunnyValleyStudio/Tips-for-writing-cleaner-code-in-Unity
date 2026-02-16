using System;
using UnityEngine;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// Abstract definition of an agent that can move, can be animated,
    /// can access input and detect if it is grounded.
    /// We also have here the basic State Machine implementation that we can
    /// extend because it is a virtual protected method.
    /// </summary>
    public abstract class Agent : MonoBehaviour
    {
        // We use protected access modifier to
        // allow the derived classes to access
        protected IAgentMover m_mover;

        protected IAgentMovementInput m_input;

        protected GroundedDetector m_groundDetector;

        protected AgentAnimations m_agentAnimations;

        protected State m_currentState;

        protected AgentStats m_agentStats;

        protected StateFactory _stateFactory;

        // We make all methods protected virtual so that we can override them
        // in the derived classes
        protected virtual void Awake()
        {
            m_input = GetComponent<IAgentMovementInput>();

            m_groundDetector = GetComponent<GroundedDetector>();
            m_agentAnimations = GetComponent<AgentAnimations>();
            m_mover = GetComponent<IAgentMover>();
            m_agentStats = GetComponent<AgentStats>();

            //Instead of StateFactory(...) method we will now use the StateFactory object]
            //to create states
            _stateFactory = new StateFactory(
                new StateFactoryData
                {
                    AgentStats = m_agentStats,
                    MovementInput = m_input,
                    GroundDetector = m_groundDetector,
                    AgentAnimations = m_agentAnimations,
                    AgentMover = m_mover
                });
        }

        protected virtual void Start()
        {
            TransitionToState(typeof(MovementState));
        }

        /// <summary>
        /// Method to transition to a new state. It would be wise to keep it
        /// private as this logic is tied to State Machine implementation. 
        /// There is hardly any reason why derived classes would need to override
        /// it.
        /// </summary>
        /// <param name="stateType"></param>
        private void TransitionToState(Type stateType)
        {
            State newState = _stateFactory.CreateState(stateType);
            if (m_currentState != null)
            {
                m_currentState.Exit();
                m_currentState.OnTransition -= TransitionToState;
            }
            m_currentState = newState;
            //Debug.Log($"Entering {stateType}");
            m_currentState.OnTransition += TransitionToState;
            m_currentState.Enter();
        }

        protected virtual void Update()
        {
            if (m_currentState != null)
                m_currentState.Update(Time.deltaTime);
        }

        protected virtual void FixedUpdate()
        {
            m_groundDetector.GroundedCheck();
            m_agentAnimations.SetBool(AnimationBoolType.Grounded, m_groundDetector.Grounded);
        }

    }
}

