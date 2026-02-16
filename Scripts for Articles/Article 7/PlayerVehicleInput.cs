using UnityEngine;
using UnityEngine.InputSystem;

namespace Tips.Part_7_End
{
    /// <summary>
    /// Player-backed implementation of IVehicleInput.
    /// Reads values from Unity Input System actions and exposes them as
    /// Throttle/Steering/Handbrake.
    /// </summary>
    public class PlayerVehicleInput : MonoBehaviour, IVehicleInput
    {
        [SerializeField]
        private InputActionReference m_moveActionRef, m_handBrakeActionRef;
        public float Throttle { get; private set; }
        public float Steering { get; private set; }
        public bool Handbrake { get; private set; }

        public void Sample()
        {
            Vector2 movementInput = m_moveActionRef.action.ReadValue<Vector2>();
            Throttle = Mathf.Clamp(movementInput.y, -1f, 1f);
            Steering = Mathf.Clamp(movementInput.x, -1f, 1f);

            Handbrake = m_handBrakeActionRef.action.IsPressed();
        }
    }
}
