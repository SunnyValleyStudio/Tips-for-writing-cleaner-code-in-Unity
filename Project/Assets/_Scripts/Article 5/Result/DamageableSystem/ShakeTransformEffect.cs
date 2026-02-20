using System.Collections;
using UnityEngine;

namespace Tips.Part_5_End
{
    /// <summary>
    /// Shakes the Tree objects by moving its transform position
    /// </summary>
    public class ShakeTransformEffect : MonoBehaviour, IDamageable
    {
        [SerializeField] private AnimationCurve shakeCurve;
        [SerializeField] private float shakeDuration = .5f;
        [SerializeField] private float shakeIntensity = .2f;

        private Vector3 originalPosition;
        private Coroutine shakeCoroutine;

        private void Awake()
        {
            originalPosition = transform.position;
        }

        public void Shake()
        {
            if (shakeCoroutine != null)
            {
                StopCoroutine(shakeCoroutine);
            }
            shakeCoroutine = StartCoroutine(ShakeCoroutine());
        }

        /// <summary>
        /// We use coroutine to perform the shake. To apply easing we use
        /// the AnimationCurve
        /// </summary>
        /// <returns></returns>
        private IEnumerator ShakeCoroutine()
        {
            float elapsedTime = 0f;

            while (elapsedTime < shakeDuration)
            {
                elapsedTime += Time.deltaTime;
                float strength = shakeCurve.Evaluate(elapsedTime / shakeDuration);
                transform.position = originalPosition + Random.insideUnitSphere * strength * shakeIntensity;
                transform.position = new Vector3(transform.position.x, originalPosition.y, transform.position.z);
                yield return null;
            }

            transform.position = originalPosition;
        }

        public void TakeDamage(DamageData damageData)
        {
            Shake();
        }
    }
}