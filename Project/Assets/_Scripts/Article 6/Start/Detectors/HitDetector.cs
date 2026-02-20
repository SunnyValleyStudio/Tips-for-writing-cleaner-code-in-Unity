using System.Collections.Generic;
using UnityEngine;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// Script that will detect all the IDamagable components and
    /// return them and the collider as a Dictionary<Collider, List<IDamagable>></IDamagable>
    /// </summary>
    public class HitDetector : MonoBehaviour
    {
        [SerializeField]
        private SphereDetector m_sphereDetector;

        public Dictionary<Collider, List<IDamageable>> PerformDetection()
        {
            Dictionary<Collider, List<IDamageable>> hitObjects = new Dictionary<Collider, List<IDamageable>>();
            Collider[] result = m_sphereDetector.DetectObjects();
            if (result.Length > 0)
            {
                foreach (var collider in result)
                {
                    List<IDamageable> damagables = new(collider.GetComponents<IDamageable>());
                    if (damagables.Count > 0)
                        hitObjects.Add(collider, damagables);
                }
            }
            return hitObjects;
        }
    }
}