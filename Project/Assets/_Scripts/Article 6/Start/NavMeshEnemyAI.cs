using System;
using UnityEngine;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// This is the High Level script defining the behavior patter of the Enemy npc. It will move towards the target and attack it
    /// when it reaches it. It will also wait for a delay before attacking again.
    /// </summary>
    public class NavMeshEnemyAI : MonoBehaviour, IAgentAttackInput
    {
        private NavMeshMovementInput m_navMeshMovementInput;
        [field: SerializeField]
        public Transform Target { get; set; }

        [SerializeField]
        private float m_attackDelay = 5.0f;
        private float m_currentDelay = 0;

        [SerializeField]
        private float m_stoppingDistance = 0.6f;

        public bool IsDead { get; set; }
        public event Action OnAttackInput;

        private void Awake()
        {
            m_navMeshMovementInput = GetComponent<NavMeshMovementInput>();
        }

        private void HandleTargetReached()
        {
            if (m_currentDelay <= 0)
            {
                m_currentDelay = m_attackDelay;
                m_navMeshMovementInput.SetTarget(null);
                OnAttackInput?.Invoke();
            }
        }

        private void Update()
        {
            if (IsDead)
            {
                m_navMeshMovementInput.SetTarget(null);
                return;
            }
            if (m_navMeshMovementInput == null)
            {
                return;
            }
            if (Target == null)
            {
                m_navMeshMovementInput.SetTarget(Target);
                return;
            }
            if (m_currentDelay > 0)
            {
                m_currentDelay -= Time.deltaTime;
                return;
            }
            if (Vector3.Distance(transform.position, Target.position) < m_stoppingDistance)
            {
                m_currentDelay = m_attackDelay;
                m_navMeshMovementInput.SetTarget(null);
                OnAttackInput?.Invoke();
                return;
            }
            m_navMeshMovementInput.SetTarget(Target);

        }
    }
}