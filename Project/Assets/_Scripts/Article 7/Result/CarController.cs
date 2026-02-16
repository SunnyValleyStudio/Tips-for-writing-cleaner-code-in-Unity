using UnityEngine;

namespace Tips.Part_7_End
{
    /// <summary>
    /// Vehicle coordinator: gathers input, delegates motion to CarEngine, and relays state to feedback. 
    /// Acts as a data router between components so behavior stays modular and isolated.
    /// </summary>
    public class CarController : MonoBehaviour
    {
        [SerializeField]
        private CarStats m_carStats;
        [SerializeField]
        private BrakeLights m_breakLights;
        [SerializeField]
        private SimpleEngineAudio m_engineAudio;
        [SerializeField]
        private SimpleSkidAudio m_skidAudio;

        private WheelControl[] m_wheels;
        private Rigidbody m_rb;

        private IVehicleInput m_input;

        private CarEngine m_carEngine;

        private void Awake()
        {
            m_input = GetComponent<IVehicleInput>();
            m_rb = GetComponent<Rigidbody>();

            // Get all wheel components attached to the car
            m_wheels = GetComponentsInChildren<WheelControl>();
            // Compose the car engine with the required references.
            m_carEngine = new CarEngine(m_wheels, m_rb);
        }

        void Start()
        {
            // One-time physical setup: adjust center of mass for stability.
            // could be extracted to a separate script
            Vector3 centerOfMass = m_rb.centerOfMass;
            centerOfMass.y += m_carStats.CentreOfGravityOffset;
            m_rb.centerOfMass = centerOfMass;
        }

        private void Update()
        {
            // Read input at frame-rate so physics can use the latest intent in FixedUpdate
            m_input.Sample();
        }

        void FixedUpdate()
        {
            // 1) Drive the car using current input and stats (physics step).
            m_carEngine.Drive(m_carStats, m_input.Throttle, m_input.Steering, m_input.Handbrake);
            // 2) Feedback driven by authoritative data from CarEngine/Rigidbody
            m_breakLights.ToggleBrakeLights(m_input.Handbrake);
            m_engineAudio.UpdateAudio(m_carEngine.Speed01, m_carEngine.IsReversing);
            m_skidAudio.SampleSlip(m_rb.linearVelocity, transform.forward, m_carEngine.Speed01, Time.fixedDeltaTime);
        }
    }
}