using UnityEngine;

namespace Tips.Part_7_End
{

    /// <summary>
    /// Tunable parameters (“knobs”) for a vehicle. Kept in a ScriptableObject so
    /// designers can tweak values per-car without code changes and you can swap
    /// whole setups at runtime.
    /// </summary>
    [CreateAssetMenu(fileName = "CarStats", menuName = "Scriptable Objects/CarStats")]
    public class CarStats : ScriptableObject
    {
        public float MotorTorque = 1000f;
        public float BrakeTorque = 2000f;
        public float MaxSpeed = 50f;

        public float SteeringRange = 50f;
        public float SteeringRangeAtMaxSpeed = 10f;

        public float CentreOfGravityOffset = -1f;
    }
}
