using UnityEngine;

namespace Tips.Part_6_Start
{
    public class AgentStats : MonoBehaviour
    {
        [field: SerializeField]
        public float MoveSpeed { get; private set; } = 2.0f;
        [field: SerializeField]
        public float SprintSpeed { get; private set; } = 5.335f;
        [field: SerializeField]
        public float Gravity { get; private set; } = -15.0f;
        [field: SerializeField]
        public float SpeedChangeRate { get; private set; } = 10.0f;
        [field: SerializeField]
        public float JumpHeight { get; private set; } = 1.2f;
        [field: SerializeField]
        public float FallTimeout { get; private set; } = 0.15f;
        public float AnimationMovementSpeed { get; set; }
    }
}