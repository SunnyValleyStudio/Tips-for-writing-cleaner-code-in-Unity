using System;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Transitions to Wave state only based on the input event. We could make it more abstract to
    /// a "InputTransition". We could do that by passing public Action delegate into the constructor 
    /// like "public 'InputTransition(Action OnInput)'
    /// </summary>
    public class MoveWaveTransition : IEventTransitionRule
    {
        private IAgentWaveInput m_waveInput;
        private bool m_waveFlag = false;
        public Type NextState => typeof(WaveState);

        public MoveWaveTransition(IAgentWaveInput waveInput)
        {
            m_waveInput = waveInput;
        }

        public bool ShouldTransition(float deltaTime)
        {
            return m_waveFlag;
        }

        public void Subscribe()
        {
            m_waveInput.OnWaveInput += HandleWaveInput;
        }

        private void HandleWaveInput()
        {
            m_waveFlag = true;
        }

        public void Unsubscribe()
        {
            m_waveInput.OnWaveInput -= HandleWaveInput;
        }
    }
}