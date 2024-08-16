using UnityEngine;

namespace Tips.Part_5_End
{
    public class NavMeshNPCDeathState : State
    {
        private GameObject m_agent;

        public NavMeshNPCDeathState(GameObject agent)
        {
            m_agent = agent;
        }

        public override void Enter()
        {
            m_agent.GetComponent<Collider>().enabled = false;
            m_agent.GetComponent<Animator>().enabled = false;
            m_agent.GetComponent<NavMeshEnemyAI>().IsDead = true;
        }

        public override void Exit()
        {
            return;
        }

        protected override void StateUpdate(float deltaTime)
        {
            return;
        }
    }
}