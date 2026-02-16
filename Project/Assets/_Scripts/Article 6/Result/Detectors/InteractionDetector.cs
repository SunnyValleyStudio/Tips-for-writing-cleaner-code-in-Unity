using UnityEngine;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Uses OverlapSphere to detect IInteractable objects with a collider / trigger in front of the Agent
    /// </summary>
    public class InteractionDetector : MonoBehaviour
    {
        [SerializeField]
        private SphereDetector m_sphereDetector;

        public IInteractable CurrentInteractable { get; private set; }
        private Highlight m_currentHighlight;

        public void DetectInteractable()
        {
            Collider[] result = m_sphereDetector.DetectObjects();
            if (result.Length > 0)
            {
                // Get the IInteractable and Highlight components from the hit object
                IInteractable interactable = result[0].GetComponent<IInteractable>();
                Highlight highlight = result[0].GetComponent<Highlight>();

                if (interactable is IActiveInteractable activeInteractable && activeInteractable.IsInteractionActive == false)
                {
                    ClearCurrentInteractable();
                    return;
                }


                if (interactable != CurrentInteractable)
                {
                    ClearCurrentInteractable();

                    CurrentInteractable = interactable;
                    m_currentHighlight = highlight;

                    if (m_currentHighlight != null)
                    {
                        m_currentHighlight.EnableHighlight();
                    }
                }
            }
            else
            {
                ClearCurrentInteractable();
            }
        }

        private void ClearCurrentInteractable()
        {
            if (m_currentHighlight != null)
            {
                m_currentHighlight.DisableHighlight();
                m_currentHighlight = null;
            }

            CurrentInteractable = null;
        }
    }
}
