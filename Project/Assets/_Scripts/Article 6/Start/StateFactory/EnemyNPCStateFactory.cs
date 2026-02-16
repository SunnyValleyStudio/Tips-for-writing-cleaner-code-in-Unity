using System;
using UnityEngine;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// State Factory for the Enemy NPC
    /// </summary>
    public class EnemyNPCStateFactory : StateFactory
    {
        public EnemyNPCStateFactory(EnemyNPCStateFactoryData stateFactoryData) : base(stateFactoryData)
        {
        }

        public override State CreateState(Type stateType)
        {
            EnemyNPCStateFactoryData m_enemyStateFactoryData = (EnemyNPCStateFactoryData)m_stateFactoryData;
            State newState = null;
            if (stateType == typeof(AttackState))
            {
                newState = new AttackState(m_enemyStateFactoryData.AgentAnimations, m_enemyStateFactoryData.AgentMover, m_enemyStateFactoryData.AgentStats, m_enemyStateFactoryData.AgentGameObject, m_enemyStateFactoryData.HitDetector, 0.2f);
                newState.AddTransition(new DelayedTransition(2f, typeof(MovementState)));
            }
            else if (stateType == typeof(NavMeshNPCDeathState))
            {
                newState = new NavMeshNPCDeathState(m_enemyStateFactoryData.AgentGameObject);
            }
            else
            {
                newState = base.CreateState(stateType);
                if (stateType == typeof(MovementState))
                {
                    newState.AddTransition(new MoveAttackTransition(m_enemyStateFactoryData.AttackInput));
                }
            }
            newState.AddTransition(new NPCDeathTransition(m_enemyStateFactoryData.HealthComponent));
            return newState;
        }
    }

    public class EnemyNPCStateFactoryData : StateFactoryData
    {
        public IAgentAttackInput AttackInput { get; set; }

        public HitDetector HitDetector { get; set; }

        public GameObject AgentGameObject { get; set; }

        public Health HealthComponent { get; set; }
    }
}