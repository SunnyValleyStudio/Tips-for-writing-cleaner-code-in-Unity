using UnityEngine;

namespace Tips.Part_7_End
{
    /// <summary>
    /// Skid/slide audio feedback. Estimates slip from the angle between the car's
    /// movement direction and its forward direction, then maps slip + normalized
    /// speed (0..1) to a smoothly changing volume/pitch on a looping AudioSource.
    /// Presentation-only: it does not read input or modify physics.
    /// </summary>
    public class SimpleSkidAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource m_skidSound;

        [Header("Loudness & Pitch")]
        [SerializeField, Range(0f, 1f)] private float m_maxVolume = 0.8f;
        [SerializeField] private float m_minPitch = 0.9f;
        [SerializeField] private float m_maxPitch = 1.3f;

        [Header("Speed window (normalized 0..1)")]
        [SerializeField, Range(0f, 1f)] private float m_minSpeed01ForSkid = 0.05f; // start reacting
        [SerializeField, Range(0f, 1f)] private float m_maxSpeed01ForSkid = 0.30f; // full loudness at ~0.3

        [Header("Smoothing")]
        [SerializeField] private float m_smoothTransition = 10f;

        float m_volume, m_pitch;

        void Start()
        {
            if (!m_skidSound) return;
            m_skidSound.loop = true;
            m_skidSound.volume = 0f;
            m_skidSound.Play(); // keep playing; we never pause
        }

        public void SampleSlip(Vector3 velocity, Vector3 forward, float speed01, float deltaTime)
        {
            if (!m_skidSound)
                return;

            if (speed01 < m_minSpeed01ForSkid)
            {
                FadeTo(0f, deltaTime);
                Apply();
                return;
            }

            float speed = velocity.magnitude;
            Vector3 velocityDirection = velocity / speed;
            Vector3 forwardDirection = forward.normalized;

            // Treat reverse as aligned (straight reverse => no skid)
            float cosAligned = Mathf.Abs(Vector3.Dot(velocityDirection, forwardDirection));           // 1 aligned, 0 sideways
            float angle = Mathf.Acos(Mathf.Clamp(cosAligned, -1f, 1f));      // 0..π/2

            // Slip angle → 0..1 (ignore tiny angles to avoid constant hiss)
            const float minDeg = 2f, maxDeg = 30f;
            float angle01 = Mathf.InverseLerp(minDeg * Mathf.Deg2Rad, maxDeg * Mathf.Deg2Rad, angle);

            // Final target loudness
            float speedWeight = Mathf.InverseLerp(m_minSpeed01ForSkid, m_maxSpeed01ForSkid, speed01);
            float targetLoudness = Mathf.Clamp01(angle01 * speedWeight);

            // Smooth volume & pitch
            float time = 1f - Mathf.Exp(-m_smoothTransition * deltaTime);
            m_volume = Mathf.Lerp(m_volume, targetLoudness, time);
            m_pitch = Mathf.Lerp(m_pitch, Mathf.Lerp(m_minPitch, m_maxPitch, speedWeight), time);

            Apply();
        }

        void FadeTo(float target, float dt)
        {
            float t = 1f - Mathf.Exp(-m_smoothTransition * dt);
            m_volume = Mathf.Lerp(m_volume, target, t);
        }

        void Apply()
        {
            float volume = Mathf.Clamp01(m_volume) * m_maxVolume;
            m_skidSound.volume = volume;
            m_skidSound.pitch = m_pitch;
        }
    }
}
