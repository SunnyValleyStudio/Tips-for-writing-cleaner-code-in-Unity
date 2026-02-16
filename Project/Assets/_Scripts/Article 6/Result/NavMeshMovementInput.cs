using UnityEngine;
using UnityEngine.AI;

namespace Tips.Part_6_End
{
    /// <summary>
    /// This script uses the NavMesh system to calculate the path to the target and send the direction to the closest
    /// corner of this path as Vector2
    /// </summary>
    public class NavMeshMovementInput : MonoBehaviour, IAgentMovementInput
    {
        [SerializeField]
        private Transform m_target;
        private NavMeshPath m_path;
        private int m_currentPathIndex;

        [SerializeField]
        private float m_pointReachedDistance = 0.3f;

        public Vector2 MovementInput { get; private set; }
        public bool SprintInput { get; private set; }

        private void Awake()
        {
            m_path = new NavMeshPath();
        }

        public void SetTarget(Transform target)
        {
            m_target = target;
        }

        private void Update()
        {
            if (m_target != null)
            {
                CalculatePath();
            }
            else if (m_path.corners.Length > 0)
            {
                m_path = new NavMeshPath();
            }
            UpdateMovementInput();
        }

        private void CalculatePath()
        {
            NavMesh.CalculatePath(transform.position, m_target.position, NavMesh.AllAreas, m_path);
            m_currentPathIndex = 1;
        }

        private void UpdateMovementInput()
        {
            if (m_path.corners.Length == 0)
            {
                MovementInput = Vector2.zero;
                return;
            }

            if (m_currentPathIndex < m_path.corners.Length)
            {
                Vector3 nextCorner = m_path.corners[m_currentPathIndex];
                Vector3 direction = (nextCorner - transform.position).normalized;

                // Convert the direction to 2D plane
                MovementInput = new Vector2(direction.x, direction.z);

                // Check if the agent is close enough to the next corner
                if (Vector3.Distance(transform.position, nextCorner) < m_pointReachedDistance)
                {
                    m_currentPathIndex++;
                }
            }
            else
            {
                MovementInput = Vector2.zero;
            }
        }

        public void OnDrawGizmosSelected()
        {
            if (Application.isPlaying && m_path != null)
            {
                for (int i = 0; i < m_path.corners.Length; i++)
                {
                    //draw path between corners
                    if (i + 1 < m_path.corners.Length)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawLine(m_path.corners[i], m_path.corners[i + 1]);
                    }
                }
            }
        }
    }
}