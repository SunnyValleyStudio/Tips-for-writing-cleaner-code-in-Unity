﻿using UnityEngine;

namespace Tips.Part_5_End
{
    /// <summary>
    /// Interface for providing movement input for an agent.
    /// This interface abstracts the movement input to decouple the input system from the agent's movement logic.
    /// </summary>
    public interface IAgentMovementInput
    {
        public Vector2 MovementInput { get; }
        public bool SprintInput { get; }
    }
}