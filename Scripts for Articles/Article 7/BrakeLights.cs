using UnityEngine;

namespace Tips.Part_7_End
{
    /// <summary>
    /// Visual brake feedback. Toggles an emission color on the assigned renderer
    /// when braking is active. Uses MaterialPropertyBlock to avoid instantiating
    /// or duplicating materials at runtime (more performant & batch-friendly).
    /// </summary>
    public class BrakeLights : MonoBehaviour
    {
        [SerializeField]
        private Renderer m_renderer;

        private MaterialPropertyBlock m_emissionPropertyBlock;
        private int m_intensity = 5;
        private bool m_currentState = false;
        private void Start()
        {
            // Cache a single property block instance for repeated use
            m_emissionPropertyBlock = new();
        }

        /// <summary>
        /// Turn the brake lights on/off.
        /// </summary>
        public void ToggleBrakeLights(bool state)
        {
            if (m_currentState == state)
                return;
            m_currentState = state;
            // Read-modify-write the property block to set emission color
            m_renderer.GetPropertyBlock(m_emissionPropertyBlock);
            // on = bright red, off = no emission
            Color color = state ? Color.red * m_intensity : Color.clear;
            m_emissionPropertyBlock.SetColor("_EmissionColor", color);
            m_renderer.SetPropertyBlock(m_emissionPropertyBlock);
        }
    }

}