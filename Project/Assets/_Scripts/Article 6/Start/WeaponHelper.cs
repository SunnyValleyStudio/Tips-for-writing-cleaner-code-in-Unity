using UnityEngine;

namespace Tips.Part_6_Start
{

    /// <summary>
    /// Helper script to visualize the result of our WeaponPickUpInteraction by enabling the weapon object on the back of our avatar.
    /// </summary>
    public class WeaponHelper : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_weapon;
        [SerializeField]
        private GameObject m_playerHandWeapon;
        /// <summary>
        /// Bool flag to check if the weapon is holstered or not.
        /// </summary>
        public bool IsWeaponHolstered { get; private set; } = false;
        /// <summary>
        /// Helper bool flag to ensures that we can't pick up another weapon
        /// </summary>
        public bool HasWeapon { get; set; }
        /// <summary>
        /// Toggles the weapon object between back and hand of the character.
        /// </summary>
        /// <param name="val"></param>
        public void ToggleWeapon(bool val)
        {
            m_weapon.SetActive(val);
            m_playerHandWeapon.SetActive(!val);
            IsWeaponHolstered = val;
        }
    }
}