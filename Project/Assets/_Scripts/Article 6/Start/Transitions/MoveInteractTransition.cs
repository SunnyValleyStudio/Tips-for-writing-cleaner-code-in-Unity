using System;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// Transitions between Movement State and Interact State by listening for Interact Input. 
    /// </summary>
    public class MoveInteractTransition : IEventTransitionRule
    {
        private IAgentInteractInput m_interactInput;
        private InteractionDetector m_detector;
        private bool m_interactFlag;
        public Type NextState => typeof(InteractState);

        public MoveInteractTransition(IAgentInteractInput interactInput, InteractionDetector detector)
        {
            m_interactInput = interactInput;
            m_detector = detector;
        }

        public bool ShouldTransition(float deltaTime)
        {
            return m_interactFlag;
        }

        public void Subscribe()
        {
            m_interactInput.OnInteract += HandleInteraction;
        }

        //We update this script to stop triggering the Interact state when there is nothing to interact with
        private void HandleInteraction()
        {
            m_interactFlag = m_detector.CurrentInteractable != null;
        }

        public void Unsubscribe()
        {
            m_interactInput.OnInteract -= HandleInteraction;
        }
    }
}
