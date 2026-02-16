using UnityEngine;

namespace Tips.Part_6_Start
{
    /// <summary>
    /// Helper class that contains the movement logic that was repeated in Jump, Movement and Fall state. By extracting the AgentStats and this logic
    /// we have only one place to modify this code and the parameter that drives it. This is one of the most common refactoring tehnique to 
    /// reduce code duplication.
    /// </summary>
    public class MovementHelper
    {
        public float PerformMovement(IAgentMovementInput input, AgentStats agentStats, IAgentMover mover, float verticalVelocity)
        {
            float targetMovementSpeed = input.SprintInput ? agentStats.SprintSpeed : agentStats.MoveSpeed;
            targetMovementSpeed = input.MovementInput == Vector2.zero ? 0 : targetMovementSpeed;
            mover.Move(
                new Vector3(input.MovementInput.x, verticalVelocity, input.MovementInput.y), targetMovementSpeed);
            return targetMovementSpeed;
        }
    }
}
