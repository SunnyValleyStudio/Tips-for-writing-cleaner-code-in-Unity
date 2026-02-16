using UnityEngine;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// Uses Physics.OverlapSphere to detect objects in front of the Agent
    /// </summary>
    public class SphereDetector : MonoBehaviour
    {
        [SerializeField] private float m_detectionRange = 1.0f;
        [SerializeField] private float m_detectionRadius = 0.5f;
        [SerializeField] private float m_height = 1.0f;
        [SerializeField] private LayerMask m_detectionLayer;

        public Color GizmoColor { get; set; } = Color.blue;

        /// <summary>
        /// Detects all the colliders intersecting the sphere
        /// </summary>
        /// <returns></returns>
        public Collider[] DetectObjects()
        {
            Collider[] results = Physics.OverlapSphere(GetDetectionPosition(), m_detectionRadius, m_detectionLayer);
            return results;
        }

        /// <summary>
        /// Returns center of the detection sphere
        /// </summary>
        /// <returns></returns>
        public Vector3 GetDetectionPosition()
        {
            return transform.position + Vector3.up * m_height + transform.forward * m_detectionRange;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = GizmoColor;
            Gizmos.DrawWireSphere(GetDetectionPosition(), m_detectionRadius);
        }
    }
}