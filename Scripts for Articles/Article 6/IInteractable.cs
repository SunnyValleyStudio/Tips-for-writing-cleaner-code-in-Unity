using UnityEngine;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Interface defining the interactable item
    /// </summary>
    public interface IInteractable
    {
        void Interact(GameObject interactor);
    }
}

