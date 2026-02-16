using UnityEngine;

namespace Tips.Part_6_End
{
    /// <summary>
    /// This script makes the NPC rotate towards the movement direction
    /// </summary>
    public class NPCRotationStrategy : AgentRotationStrategy
    {
        protected override float RotationStrategy(Vector3 inputDirection)
            => Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
    }
}