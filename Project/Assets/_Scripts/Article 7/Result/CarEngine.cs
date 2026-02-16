using System;
using UnityEngine;

namespace Tips.Part_7_End
{
    /// <summary>
    /// Encapsulates movement math and wheel actuation.
    /// Consumes input (throttle/steer/handbrake) and CarStats to
    /// drive the provided WheelControl array via a single
    /// Rigidbody. Exposes simple telemetry (Speed, Speed01, IsReversing)
    /// for UI/FX. Kept non-MonoBehaviour to keep responsibilities focused and
    /// make the logic easier to test and reuse.
    /// </summary>
    public class CarEngine
    {
        public float Speed { get; private set; }
        //Normalized speed
        public float Speed01 { get; private set; }
        public bool IsReversing { get; private set; }


        private WheelControl[] m_wheels;
        private Rigidbody m_rb;

        public CarEngine(WheelControl[] wheels, Rigidbody rb)
        {
            m_wheels = wheels;
            m_rb = rb;
        }

        public void Drive(CarStats stats, float throttleInput, float steerInput
            , bool handbrakeInput)
        {
            // Calculate current speed along the car's forward axis
            float forwardSpeed = Vector3.Dot(m_rb.transform.forward, m_rb.linearVelocity);
            // We need the absolute value to (1) normalize speed (e.g., compute Speed01)
            // and (2) drive magnitude-based tuning (torque/steer taper). The original sign
            // of forwardSpeed is used separately to detect reversing.
            float forwardSpeedAbs = Mathf.Abs(forwardSpeed);
            Speed = forwardSpeedAbs;
            Speed01 = Mathf.InverseLerp(0f, stats.MaxSpeed, forwardSpeedAbs);

            IsReversing = forwardSpeed < 0;

            // Reduce motor torque and steering at high speeds for better handling
            float motorTorque = Mathf.Lerp(stats.MotorTorque, 0f, Speed01);
            float steerRange = Mathf.Lerp(stats.SteeringRange, stats.SteeringRangeAtMaxSpeed, Speed01);

            // Determine if the player is accelerating or trying to reverse
            bool isMoving = forwardSpeedAbs > 0.1f;
            bool sameDirection = !isMoving || Mathf.Sign(throttleInput) == Mathf.Sign(forwardSpeed);

            foreach (var w in m_wheels)
            {
                // Apply steering to wheels that support steering
                if (w.steerable)
                {
                    w.WheelCollider.steerAngle = steerInput * steerRange;
                }

                // Apply torque to motorized wheels
                float motor = 0f, brake = 0f;
                if (handbrakeInput)
                {
                    if (w.motorized)
                        brake = stats.BrakeTorque;
                }
                else if (sameDirection)
                {
                    if (w.motorized)
                        motor = throttleInput * motorTorque;
                }
                else
                {
                    brake = Mathf.Abs(throttleInput) * stats.BrakeTorque;
                }

                w.WheelCollider.motorTorque = motor;
                w.WheelCollider.brakeTorque = brake;
            }
        }
    }
}