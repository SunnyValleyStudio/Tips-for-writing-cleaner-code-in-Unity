using UnityEngine;

namespace Tips.Part_4_Start
{
    public class WeaponPickUpInteraction : MonoBehaviour, IInteractable
    {
        /// <summary>
        /// Defines an interaction where object can be picked up and is destroyed
        /// afterwards
        /// </summary>
        public void Interact(GameObject interactor)
        {
            WeaponHelper weaponHelper;
            if (weaponHelper = interactor.GetComponent<WeaponHelper>())
            {
                weaponHelper.ToggleWeapon(true);
            }
            Destroy(gameObject);
        }
    }

}