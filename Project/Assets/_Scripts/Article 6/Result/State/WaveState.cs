namespace Tips.Part_6_End
{
    /// <summary>
    /// InteractState integrates the Interaction System that we are introducing with our Sate Pattern. Like the InteractState it has a slowDownDelay
    /// that makes the movement animation to slow down gradually rather than making the character stop moving immediatelly. 
    /// </summary>
    public class WaveState : State
    {
        private AgentAnimations m_agentAnimations;
        private float m_slowDownDelay = 0.3f;
        private float m_delayTemp;
        private float m_startAnimationSpeed;
        public WaveState(AgentAnimations agentAnimations)
        {
            m_agentAnimations = agentAnimations;
            m_delayTemp = m_slowDownDelay;
        }

        public override void Enter()
        {
            m_agentAnimations.SetTrigger(AnimationTriggerType.Wave);
            m_startAnimationSpeed = m_agentAnimations.GetFloat(AnimationFloatType.Speed);
        }

        public override void Exit()
        {
            m_agentAnimations.ResetTrigger(AnimationTriggerType.Wave);
        }

        protected override void StateUpdate(float deltaTime)
        {
            if (m_delayTemp >= 0)
            {
                m_agentAnimations.SetFloat(AnimationFloatType.Speed, m_startAnimationSpeed * m_delayTemp / m_slowDownDelay);
                m_delayTemp -= deltaTime;
            }
        }
    }
}
