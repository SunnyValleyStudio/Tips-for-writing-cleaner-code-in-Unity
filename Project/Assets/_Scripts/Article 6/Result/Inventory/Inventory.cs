using UnityEngine;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Minimal, example inventory for Article 6:
    /// tracks whether the player currently holds a key.
    /// </summary>
    public class Inventory : MonoBehaviour
    {
        [SerializeField]
        private bool m_hasKey = false;
        public bool HasKey => m_hasKey;

        public void PickUpKey()
        {
            m_hasKey = true;
        }

        public void RemoveKey()
        {
            m_hasKey = false;
        }
    }
}

