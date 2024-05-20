using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tips.Part_3_Start
{
    public class JumpTransition : ITransitionRule
    {
        public Type NextState => typeof(JumpState);
        public float m_jumpTimeout = 0.20f;

        private IAgentJumpInput m_jumpInput;

        public JumpTransition(IAgentJumpInput jumpInput)
        {
            m_jumpInput = jumpInput;
        }

        public bool ShouldTransition(float deltaTime)
        {
            if (m_jumpTimeout <= 0 && m_jumpInput.JumpInput)
                return true;
            m_jumpTimeout -= deltaTime;
            return false;
        }
    }
}