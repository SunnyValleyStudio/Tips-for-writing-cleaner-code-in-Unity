using UnityEngine;

namespace Tips.Part_3_End
{
    public class SwitchInteractable : MonoBehaviour, IInteractable
    {
        private bool m_isSwitched = false;
        [SerializeField]
        private Animator m_animator;
        [SerializeField]
        private string m_animationTriggerName = "Activate";

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
        }

        public void Interact(GameObject interactor)
        {
            if (m_isSwitched == true)
                return;
            m_isSwitched = true;
            m_animator.SetTrigger(m_animationTriggerName);
        }
    }
}

