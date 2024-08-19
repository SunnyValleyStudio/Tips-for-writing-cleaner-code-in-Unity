using UnityEngine;

namespace Tips.Part_5_Start
{
    /// <summary>
    /// Defines an interaction where object can be picked up and is destroyed
    /// afterwards
    /// Affects the WeaponHelper to make it aware if the Agent has a weapon or not.
    /// Right now it only supports a single weapon type.
    /// </summary>
    public class WeaponPickUpInteraction : MonoBehaviour, IInteractable
    {
        public void Interact(GameObject interactor)
        {
            WeaponHelper weaponHelper;
            if (weaponHelper = interactor.GetComponent<WeaponHelper>())
            {
                if (weaponHelper.HasWeapon)
                    return;

                weaponHelper.ToggleWeapon(true);
                weaponHelper.HasWeapon = true;
            }
            Destroy(gameObject);
        }
    }

}