using UnityEngine;

namespace Tips.Part_7_End
{
    /// <summary>
    /// Engine sound feedback. Blends three looping AudioSources (idle, forward, reverse)
    /// based on a normalized engine load (0..1) and reversing flag provided by the controller.
    /// Presentation-only: this component never reads input or physics; it just maps
    /// engine state -> volumes/pitches for the audio loops.
    /// </summary>
    public class SimpleEngineAudio : MonoBehaviour
    {
        [Tooltip("What audio clip should play when the kart does nothing?")]
        [SerializeField]
        private AudioSource m_idleSound;
        [Tooltip("What audio clip should play when the kart moves around?")]
        [SerializeField]
        private AudioSource m_runningSound;
        [Tooltip("Maximum Volume the running sound will be at full speed")]
        [SerializeField, Range(0.1f, 1.0f)]
        private float m_runningSoundMaxVolume = 1.0f;
        [Tooltip("Maximum Pitch the running sound will be at full speed")]
        [SerializeField, Range(0.1f, 2.0f)]
        private float m_runningSoundMaxPitch = 1.0f;
        [Tooltip("What audio clip should play when the kart moves in Reverse?")]
        [SerializeField]
        private AudioSource m_reverseSound;
        [Tooltip("Maximum Volume the Reverse sound will be at full Reverse speed")]
        [SerializeField, Range(0.1f, 1.0f)]
        private float m_reverseSoundMaxVolume = 0.5f;
        [Tooltip("Maximum Pitch the Reverse sound will be at full Reverse speed")]
        [SerializeField, Range(0.1f, 2.0f)]
        private float m_reverseSoundMaxPitch = 0.6f;

        private float m_engineLoad01;
        private bool m_isReversing;

        private void Start()
        {
            m_idleSound.loop = true;
            m_idleSound.volume = 0;
            m_idleSound.Play();
            m_runningSound.loop = true;
            m_runningSound.volume = 0;
            m_runningSound.Play();
            m_reverseSound.loop = true;
            m_reverseSound.volume = 0;
            m_reverseSound.Play();
        }
        void Update()
        {
            m_idleSound.volume = Mathf.Lerp(0.6f, 0.0f, m_engineLoad01);

            if (m_isReversing)
            {
                // In reverse
                m_runningSound.volume = 0.0f;
                m_reverseSound.volume = Mathf.Lerp(0.1f, m_reverseSoundMaxVolume, m_engineLoad01);
                m_reverseSound.pitch = Mathf.Lerp(0.1f, m_reverseSoundMaxPitch, m_engineLoad01 + (Mathf.Sin(Time.time) * .1f));
            }
            else
            {
                // Moving forward
                m_reverseSound.volume = 0.0f;
                m_runningSound.volume = Mathf.Lerp(0.1f, m_runningSoundMaxVolume, m_engineLoad01);
                m_runningSound.pitch = Mathf.Lerp(0.3f, m_runningSoundMaxPitch, m_engineLoad01 + (Mathf.Sin(Time.time) * .1f));
            }


        }

        public void UpdateAudio(float engineLoad01, bool isReversing)
        {
            m_engineLoad01 = engineLoad01;
            m_isReversing = isReversing;
        }
    }
}
