
namespace Tips.Part_7_End
{
    /// <summary>
    /// Input contract for any vehicle controller (player, AI, replay, etc.).
    /// </summary>
    public interface IVehicleInput
    {
        bool Handbrake { get; }
        float Steering { get; }
        float Throttle { get; }
        void Sample();
    }

}