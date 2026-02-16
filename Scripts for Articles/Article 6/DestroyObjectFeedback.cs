using System;
using UnityEngine;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Feedback script: destroys the target object when the observed health reports death.
    /// </summary>
    public class DestroyObjectFeedback : MonoBehaviour
    {
        [SerializeField]
        private HealthDeathObserver m_healthDeathObserver;
        [SerializeField]
        private GameObject m_objectToDestroy;

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
            Destroy(m_objectToDestroy);
        }
    } 
}

