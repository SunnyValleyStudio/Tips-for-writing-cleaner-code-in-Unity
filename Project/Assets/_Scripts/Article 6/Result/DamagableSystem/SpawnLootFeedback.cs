using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tips.Part_6_End
{
    /// <summary>
    /// Feedback component that spawns configured loot prefabs when CurrentHealth
    /// drops to zero.
    /// </summary>
    public class SpawnLootFeedback : MonoBehaviour
    {
        [SerializeField]
        private HealthDeathObserver m_healthDeathObserver;

        //List of prefabs to spawn
        [SerializeField]
        private List<GameObject> m_itemsToSpawn;

        private void OnEnable()
        {
            m_healthDeathObserver.OnDied += ShowLoot;
        }

        private void OnDisable()
        {
            m_healthDeathObserver.OnDied -= ShowLoot;
        }
        private void ShowLoot()
        {
            // Spawn each configured prefab at this object's position.
            foreach (var item in m_itemsToSpawn)
            {
                if (item != null)
                {
                    Instantiate(item, transform.position, Quaternion.identity);
                }
            }
        }
    }
}