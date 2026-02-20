using UnityEngine;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Feedback script: enables a target object when the observed health reports death.
    /// </summary>
    public class ShowBrokenObjectFeedback : MonoBehaviour
    {
        [SerializeField]
        private HealthDeathObserver m_healthDeathObserver;
        [SerializeField]
        private GameObject m_objectToEnable;
        private void OnEnable()
        {
            m_healthDeathObserver.OnDied += PlayFeedback;
        }

        private void OnDisable()
        {
            m_healthDeathObserver.OnDied -= PlayFeedback;
        }

        private void PlayFeedback()
        {
            m_objectToEnable.SetActive(true);
        }
    }
}

