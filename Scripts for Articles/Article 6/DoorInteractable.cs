using UnityEngine;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Door interaction entry point: authorizes via an IAccessRule and,
    /// if allowed and not animating, asks DoorController to toggle state.
    /// </summary>
    public class DoorInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private DoorController m_doorController;
        private IAccessRule m_accessRule;
        private void Awake()
        {
            // Resolve the access policy from co-located components.
            // If none is provided, fall back to an "always allow" rule
            // to preserve existing door behavior.
            m_accessRule = GetComponent<IAccessRule>();
            if (m_accessRule == null)
                m_accessRule = gameObject.AddComponent<UnlockedRule>();
        }
        public void Interact(GameObject interactor)
        {
            if (m_accessRule.TryAuthorize(interactor) == false)
                return;
            if (m_doorController.AnimationDone)
            {
                m_doorController.Toggle();
            }
        }
    }
}
