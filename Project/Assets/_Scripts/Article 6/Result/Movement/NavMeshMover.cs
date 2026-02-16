using UnityEngine;
using UnityEngine.AI;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Mover implementation that uses the NavMesh system to move the agent.
    /// We set the destination of the NavMeshAgent to the target position calculated
    /// based on the newest input. This makes it possible to reuse it with keyboard based input 
    /// or AI input provided by the NavMeshEnemyAI script.
    /// </summary>
    public class NavMeshMover : MonoBehaviour, IAgentMover
    {
        private NavMeshAgent m_navMeshAgent;

        public Vector3 CurrentVelocity => m_navMeshAgent.velocity;

        private void Awake()
        {
            m_navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void Move(Vector3 input, float speed)
        {
            m_navMeshAgent.speed = speed;

            Vector3 targetPosition =
                transform.position + new Vector3(input.x, 0, input.z);

            m_navMeshAgent.SetDestination(targetPosition);
        }

    }
}