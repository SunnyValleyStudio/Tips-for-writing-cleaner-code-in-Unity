using UnityEngine;

namespace Tips.Part_6_End
{
    public interface IAgentMover
    {
        void Move(Vector3 input, float speed);
        Vector3 CurrentVelocity { get; }
    }
}