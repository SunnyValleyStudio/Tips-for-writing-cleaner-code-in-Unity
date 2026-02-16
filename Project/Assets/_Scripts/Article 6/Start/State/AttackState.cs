using System.Collections.Generic;
using UnityEngine;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// Attack state where the agent will play the attack animation.
    /// We need m_mover to stop the movement of the agent before we play 
    /// the attack animation.
    /// </summary>
    public class AttackState : State
    {
        private AgentAnimations m_agentAnimations;
        private IAgentMover m_mover;
        private AgentStats m_agentStats;
        private GameObject m_agent;
        private HitDetector m_hitDetector;
        /// <summary>
        /// m_detectionDelay allows us to integrate attack animation with the
        /// code that calls TakeDamage(..) on the IDamagable objects.
        /// </summary>
        private float m_detectionDelay;
        private float m_currentTime = 0;
        public AttackState(AgentAnimations agentAnimations, IAgentMover mover, AgentStats agentStats, GameObject agent, HitDetector hitDetector, float detectionDelay)
        {
            m_agentAnimations = agentAnimations;
            m_mover = mover;
            m_agentStats = agentStats;
            m_hitDetector = hitDetector;
            m_detectionDelay = detectionDelay;
            //We need the GameObject to be able to call TakeDamage(..)
            //- to add it to the DamageData as "Sender"
            m_agent = agent;
        }

        public override void Enter()
        {
            m_mover.Move(Vector3.zero, 0);
            m_agentStats.AnimationMovementSpeed = 0;
            m_agentAnimations.SetFloat(AnimationFloatType.Speed, 0);
            //We trigger the animation first and wait for a specified
            //delay time in the StateUpdate method
            m_agentAnimations.SetTrigger(AnimationTriggerType.Attack);
        }

        public override void Exit()
        {
            return;
        }

        /// <summary>
        /// We wait for the specified delay time before we call 
        /// PerformDetection and call TakeDamage(..) on the 
        /// detected IDamagable components.
        /// </summary>
        /// <param name="deltaTime"></param>
        protected override void StateUpdate(float deltaTime)
        {
            if (m_currentTime < 0)
                return;
            m_currentTime += Time.deltaTime;
            if (m_currentTime >= m_detectionDelay)
            {
                m_currentTime = -1;
                Dictionary<Collider, List<IDamagable>> result = m_hitDetector.PerformDetection();
                foreach (var collider in result.Keys)
                {
                    foreach (var damageable in result[collider])
                    {
                        damageable.TakeDamage(new DamageData() { Sender = m_agent, DamageAmount = 1 });
                    }
                }
            }
        }
    }
}