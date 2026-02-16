using UnityEngine;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Access rule that always authorizes. Useful as the default "unrestricted" policy,
    /// preserving existing behavior while allowing easy future extension.
    /// </summary>
    public class UnlockedRule : MonoBehaviour, IAccessRule
    {
        public bool TryAuthorize(GameObject interactor)
        {
            return true;
        }
    }
}
