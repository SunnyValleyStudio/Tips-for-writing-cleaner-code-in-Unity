using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// More advanced script that uses Post Processing to create a hit effect
    /// We use Vignette to create a red overlay on the screen when 
    /// the player takes damage
    /// </summary>
    public class PlayerTakeDamageEffect : MonoBehaviour
    {
        [SerializeField]
        Volume m_volume;
        Vignette m_vignette;

        [SerializeField]
        private float m_damageDuration = 0.5f;
        private float m_currentTime = 0;
        /// <summary>
        /// We use Animation Curve to define how transparency of the vignette 
        /// should change over time
        /// </summary>
        [SerializeField]
        private AnimationCurve m_curve;

        private void Start()
        {
            //This is how we can access Vignette from the Volume in URP
            m_volume.profile.TryGet<Vignette>(out m_vignette);
        }

        public void TriggerHitEffect()
        {
            m_currentTime = 0;
            //Intensity is the parameter that controls how
            //visible the vignette is
            m_vignette.intensity.Override(0f);
            StartCoroutine(HitCoroutine());
        }

        /// <summary>
        /// We use coroutine to animate the intensity of our vignette
        /// </summary>
        /// <returns></returns>
        private IEnumerator HitCoroutine()
        {
            while (m_currentTime <= m_damageDuration)
            {
                m_currentTime += Time.deltaTime;
                float currentValue = Mathf.Clamp01(m_currentTime / m_damageDuration);
                m_vignette.intensity.Override(m_curve.Evaluate(currentValue));
                yield return null;
            }
            m_vignette.intensity.Override(0f);
        }
    }
}