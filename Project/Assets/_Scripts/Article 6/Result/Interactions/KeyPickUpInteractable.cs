using UnityEngine;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Interactable pickup: grants a key to the interactor's Inventory and destroys itself.
    /// </summary>
    public class KeyPickUpInteractable : MonoBehaviour, IInteractable
    {
        public void Interact(GameObject interactor)
        {
            Inventory inventory = interactor.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.PickUpKey();
                Destroy(gameObject);
                Debug.Log("Key picked up");
            }
        }
    }
}

