using UnityEngine;

namespace Tips.Part_6_End
{
    /// <summary>
    /// InteractState integrates the Interaction System that we are introducing with our Sate Pattern. It has some addition "slowDownDelay" to allow
    /// the movement animation to slow down gradually rather than making the character stop moving immediatelly. It also helps us to palay the Interaction
    /// animation a bit before we trigger the animation. We could also use an AnimationEvent to trigger the interaction at a specific moment.
    /// </summary>
    public class InteractState : State
    {
        private AgentAnimations m_agentAnimations;
        private InteractionDetector m_interactionDetector;
        private float m_slowDownDelay = 0.3f;
        private float m_delayTemp;
        private float m_startAnimationSpeed;

        private float m_interactDelay = 0.3f;
        private bool m_interactionFinishedFlag = false;
        public InteractState(AgentAnimations agentAnimations, InteractionDetector interactionDetector)
        {
            m_agentAnimations = agentAnimations;
            m_delayTemp = m_slowDownDelay;
            m_interactionDetector = interactionDetector;
        }

        public override void Enter()
        {
            m_agentAnimations.SetTrigger(AnimationTriggerType.Interact);
            m_startAnimationSpeed = m_agentAnimations.GetFloat(AnimationFloatType.Speed);
            Debug.Log("Interaction State: Interacting!");
        }

        public override void Exit()
        {
            m_agentAnimations.ResetTrigger(AnimationTriggerType.Interact);
        }

        protected override void StateUpdate(float deltaTime)
        {
            if (m_delayTemp >= 0)
            {
                m_agentAnimations.SetFloat(AnimationFloatType.Speed, m_startAnimationSpeed * m_delayTemp / m_slowDownDelay);
                m_delayTemp -= deltaTime;
            }

            if (m_interactionFinishedFlag)
                return;

            if (m_interactDelay <= 0)
            {
                if (m_interactionDetector.CurrentInteractable != null)
                {
                    m_interactionDetector.CurrentInteractable.Interact(m_interactionDetector.gameObject);
                }
                m_interactionFinishedFlag = true;
            }
            else
            {
                m_interactDelay -= deltaTime;
            }

        }
    }
}