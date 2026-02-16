using UnityEngine;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Defines a policy that decides whether an interaction is authorized
    /// </summary>
    public interface IAccessRule
    {
        bool TryAuthorize(GameObject interactor);
    }
}
