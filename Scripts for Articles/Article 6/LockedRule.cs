using PlasticPipe.PlasticProtocol.Messages;
using Tips.Part_6_End;
using UnityEngine;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Access rule that starts locked and requires a key in the interactor's
    /// Inventory to authorize. Optionally consumes the key on use,
    /// and unlocks itself after the first successful authorization.
    /// </summary>
    public class LockedRule : MonoBehaviour, IAccessRule
    {
        [SerializeField]
        private bool m_consumeKey = false;
        [SerializeField]
        private bool m_isLocked = true;

        public bool TryAuthorize(GameObject interactor)
        {
            // Already unlocked? Always allow.
            if (m_isLocked == false)
                return true;
            // Require an Inventory with a key.
            Inventory inventory = interactor.GetComponent<Inventory>();
            if (inventory == null)
                return false;
            if (inventory.HasKey == false)
                return false;
            // After a successful authorization remove the key and unlock the door
            if (m_consumeKey)
                inventory.RemoveKey();
                m_isLocked = false;
            return true;
        }
    }
}
