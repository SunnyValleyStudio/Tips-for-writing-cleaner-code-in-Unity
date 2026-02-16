using UnityEngine;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Raycasts towards the ground to detect if we are grounded or not. We use an extra "StairsGrounded" flag
    /// to prevent character from triggering Fall state when going down the stairs with some success.
    /// </summary>
    public class GroundedDetector : MonoBehaviour
    {
        [field: SerializeField]
        public bool Grounded { get; private set; }
        [field: SerializeField]
        public bool StairsGrounded { get; private set; }

        [Header("Grounded Check")]
        [SerializeField] private float m_groundedOffset = 0.14f, m_stairOffset = 0.02f;
        [SerializeField] private float m_groundedRadius = 0.28f;

        [SerializeField] private LayerMask m_groundLayers;

        public void GroundedCheck()
        {
            Grounded = GroundedCheck(m_groundedOffset);
            StairsGrounded = GroundedCheck(m_stairOffset);
        }
        private bool GroundedCheck(float groundedOffset)
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + groundedOffset,
                transform.position.z);
            return Physics.CheckSphere(spherePosition, m_groundedRadius, m_groundLayers,
                QueryTriggerInteraction.Ignore);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y + m_groundedOffset, transform.position.z),
                m_groundedRadius);
        }
    }
}
