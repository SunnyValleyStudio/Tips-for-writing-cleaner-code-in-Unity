using UnityEngine;
using UnityEngine.InputSystem;

using PlayerInput = UnityEngine.InputSystem.PlayerInput;

namespace Tips.Partm_1m_Start
{
    /// <summary>
    /// Monolithic script that defines a character controller for a player character.
    /// </summary>
    public class PlayerMonolithic : MonoBehaviour
    {
        [Header("Movement Parameters")]
        public float MoveSpeed = 2.0f;
        public float SprintSpeed = 5.335f;
        public float RotationSmoothTime = 0.12f;
        public float SpeedChangeRate = 10.0f;
        public float FallTimeout = 0.15f;

        private CharacterController m_controller;
        private PlayerInput m_input;
        private float m_speed;
        private float m_targetRotation = 0.0f;
        private float m_rotationVelocity;
        private float m_verticalVelocity;
        private float m_fallTimeoutDelta;

        [Header("Grounded Check")]
        public float Gravity = -15.0f;
        public bool Grounded = true, StairsGrounded = true;
        public float GroundedOffset = 0.21f, StairOffset = 0.07f;
        public float GroundedRadius = 0.28f;
        public LayerMask GroundLayers;

        [Header("Camera")]
        public GameObject CinemachineCameraTarget;
        public float TopCameraLimit = 70.0f;
        public float BottomCameraLimit = -30.0f;

        private GameObject m_mainCamera;
        private float m_cinemachineTargetYaw;
        private float m_cinemachineTargetPitch;
        private const float m_cameraRotationThreshold = 0.01f;

        [Header("Animations")]
        public string AnimationSpeedFloat;
        public string AnimationGroundedBool;
        public string AnimationFallTrigger;

        private Animator m_animator;
        private float m_animationMovementSpeed;

        //Input
        private Vector2 m_movementInput, m_lookInput;
        private bool m_isSprintingInput;


        //Getting reference to Unity specific objects
        private void Awake()
        {
            // get a reference to our main camera
            if (m_mainCamera == null)
            {
                m_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
            m_animator = GetComponent<Animator>();
            m_controller = GetComponent<CharacterController>();
            m_input = GetComponent<PlayerInput>();
        }

        //Connecting to the InputSystem
        private void OnEnable()
        {
            m_input.actions["Player/Move"].performed += OnMove;
            m_input.actions["Player/Move"].canceled += OnMove;
            m_input.actions["Player/Look"].performed += OnLook;
            m_input.actions["Player/Look"].canceled += OnLook;
            m_input.actions["Player/Sprint"].performed += OnSprint;
        }

        //Disconnecting from the InputSystem
        private void OnDisable()
        {
            m_input.actions["Player/Move"].performed -= OnMove;
            m_input.actions["Player/Move"].canceled -= OnMove;
            m_input.actions["Player/Look"].performed -= OnLook;
            m_input.actions["Player/Look"].canceled -= OnLook;
            m_input.actions["Player/Sprint"].performed -= OnSprint;
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            m_lookInput = context.ReadValue<Vector2>();

        }

        private void OnMove(InputAction.CallbackContext context)
        {
            m_movementInput = context.ReadValue<Vector2>();
        }

        private void OnSprint(InputAction.CallbackContext context)
        {
            m_isSprintingInput = context.ReadValueAsButton();

        }

        //All the logic connected with movement happens in the Update
        private void Update()
        {
            //Applying gravity force and controlling FALL animation
            if (Grounded == false)
            {
                m_verticalVelocity += Gravity * Time.deltaTime;
                m_fallTimeoutDelta -= Time.deltaTime;
                if (m_fallTimeoutDelta <= 0 && StairsGrounded == false)
                {
                    m_animator.SetTrigger(AnimationFallTrigger);
                }
            }
            else
            {
                m_verticalVelocity = 0;
                m_fallTimeoutDelta = FallTimeout;
                m_animator.ResetTrigger(AnimationFallTrigger);
            }

            CharacterMovementCalculation();
            RotationCalculation();

            Vector3 targetDirection = Quaternion.Euler(0.0f, m_targetRotation, 0.0f) * Vector3.forward;

            //move the character controller
            m_controller.Move(targetDirection.normalized * (m_speed * Time.deltaTime) +
                             new Vector3(0.0f, m_verticalVelocity, 0.0f) * Time.deltaTime);

            //play animations
            m_animator.SetFloat(AnimationSpeedFloat, m_animationMovementSpeed);
        }

        //Calculating the Avatar rotation base don the MainCamera and PlayerInput
        private void RotationCalculation()
        {
            // normalize input direction
            Vector3 inputDirection = new Vector3(m_movementInput.x, 0.0f, m_movementInput.y).normalized;

            //Rotation Code
            if (m_movementInput != Vector2.zero)
            {
                m_targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                  m_mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, m_targetRotation, ref m_rotationVelocity,
                    RotationSmoothTime);

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }
        }

        //Calculating the movement speed and controlling the MOVEMENT animation
        private void CharacterMovementCalculation()
        {
            float targetSpeed = m_isSprintingInput ? SprintSpeed : MoveSpeed;


            if (m_movementInput == Vector2.zero)
                targetSpeed = 0.0f;

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(m_controller.velocity.x, 0.0f, m_controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = m_movementInput.magnitude;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                m_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                m_speed = Mathf.Round(m_speed * 1000f) / 1000f;
            }
            else
            {
                m_speed = targetSpeed;
            }

            m_animationMovementSpeed = Mathf.Lerp(m_animationMovementSpeed, targetSpeed, Time.deltaTime * SpeedChangeRate);
            if (m_animationMovementSpeed < 0.01f)
                m_animationMovementSpeed = 0f;
        }

        //Camera movement in the LateUpdate for more consistent results
        private void LateUpdate()
        {
            CameraRotation();
        }

        private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (m_lookInput.sqrMagnitude >= m_cameraRotationThreshold)
            {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = 1.0f;

                m_cinemachineTargetYaw += m_lookInput.x * deltaTimeMultiplier;
                m_cinemachineTargetPitch += m_lookInput.y * deltaTimeMultiplier;
            }

            // clamp our rotations so our values are limited 360 degrees
            m_cinemachineTargetYaw = ClampAngle(m_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            m_cinemachineTargetPitch = ClampAngle(m_cinemachineTargetPitch, BottomCameraLimit, TopCameraLimit);

            // Cinemachine will follow this target
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(m_cinemachineTargetPitch,
                m_cinemachineTargetYaw, 0.0f);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        //Grounded checks that allows us to swap between FALL and MOVEMENT animation / behavior
        private void FixedUpdate()
        {
            Grounded = GroundedCheck(GroundedOffset);
            StairsGrounded = GroundedCheck(StairOffset);
            m_animator.SetBool(AnimationGroundedBool, Grounded);
        }

        //Spherecasting downwards to detect if we are grounded
        private bool GroundedCheck(float groundedOffset)
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + groundedOffset,
                transform.position.z);
            return Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);
        }

        //Visualizaton of the Grounded check
        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y + GroundedOffset, transform.position.z),
                GroundedRadius);
        }
    }
}

