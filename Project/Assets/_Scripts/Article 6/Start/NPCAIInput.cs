using UnityEngine;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// Implements an NPC Input that makes the NPC follow a predefined path.
    /// Feel free to browse the code but AI is not the focus of this project.
    /// </summary>
    public class NPCAIInput : MonoBehaviour, IAgentMovementInput
    {
        //NPC will follow a predefined path
        [SerializeField]
        private Transform[] m_waypoints;
        [SerializeField, Min(0.5f)]
        private float m_distanceThreshold = 1.0f;
        private int m_currentWaypointIndex = 0;

        public Vector2 MovementInput { get; private set; }
        public bool SprintInput { get; private set; }

        private void Update()
        {
            if (m_waypoints.Length <= 0)
                return;
            FindClosestWaypoint();
            Vector3 movementDirection = FindDirectionToTheNextWaypoint();
            MovementInput = new Vector2(movementDirection.x, movementDirection.z);
        }

        private Vector3 FindDirectionToTheNextWaypoint()
        {
            Vector3 currentWaypoint = m_waypoints[m_currentWaypointIndex].position;
            currentWaypoint.y = transform.position.y;
            Vector3 movementDirection = (currentWaypoint - transform.position).normalized;
            return movementDirection;
        }

        private void FindClosestWaypoint()
        {
            float distance
                        = Vector3.Distance(transform.position, m_waypoints[m_currentWaypointIndex].position);
            if (distance < m_distanceThreshold)
            {
                m_currentWaypointIndex++;
                m_currentWaypointIndex
                    = m_currentWaypointIndex >= m_waypoints.Length ? 0 : m_currentWaypointIndex;
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (m_waypoints == null || m_waypoints.Length < 2)
                return;
            Gizmos.color = Color.magenta;
            for (int i = 0; i < m_waypoints.Length; i++)
            {
                if (m_waypoints[i] == null)
                    return;
                if (i == m_waypoints.Length - 1)
                    Gizmos.DrawLine(m_waypoints[i].position, m_waypoints[0].position);
                else
                    Gizmos.DrawLine(m_waypoints[i].position, m_waypoints[i + 1].position);
            }
        }
    }
}

