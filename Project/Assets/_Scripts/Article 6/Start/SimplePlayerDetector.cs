using UnityEngine;
using UnityEngine.Events;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// Simple detector that uses a preassigned reference to player Transform to
    /// measure the distance between the player and the object and to decide
    /// if the player is detected or not.
    /// </summary>
    public class SimplePlayerDetector : MonoBehaviour
    {
        /// <summary>
        /// Reference to the player object
        /// </summary>
        [SerializeField]
        private Transform m_playerObject;

        /// <summary>
        /// Toggle to disable the detector
        /// </summary>
        [SerializeField]
        private bool m_isOn = true;

        /// <summary>
        /// Detection radius
        /// </summary>
        [SerializeField]
        private float m_detectionRadius = 5.0f;


        private bool m_playerDetected = false;

        /// <summary>
        /// Event to modify the Target of the NavMeshEnemyAI
        /// </summary>
        public UnityEvent<Transform> OnDetectionUpdate;

        private void Update()
        {
            if (m_playerObject == null || !m_isOn)
            {
                if (m_playerDetected)
                {
                    OnDetectionUpdate?.Invoke(null);
                    m_playerDetected = false;
                }
                return;
            }

            //Checks the distance between the player and the object
            if (Vector3.Distance(m_playerObject.position, transform.position) < m_detectionRadius)
            {
                Vector3 directionToPlayer = (m_playerObject.position - transform.position).normalized;
                if (m_playerDetected)
                    return;
                m_playerDetected = true;
                OnDetectionUpdate?.Invoke(m_playerObject);
            }
            //Resets the detection result
            else if (m_playerDetected)
            {
                OnDetectionUpdate?.Invoke(null);
                m_playerDetected = false;
            }
        }

        /// <summary>
        /// Draws the detection sphere showing if we have detected the Player or not
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            if (m_detectionRadius > 0)
            {
                Color c = Color.blue;
                if (m_playerDetected)
                    c = Color.red;
                c.a = 0.3f;
                Gizmos.color = c;
                Gizmos.DrawSphere(transform.position, m_detectionRadius);
            }
        }
    }
}