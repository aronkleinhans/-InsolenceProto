using UnityEngine;
using Insolence.SaveUtility;

namespace Insolence.KinematicCharacterController
{
    public class KinematicPlayerControls : MonoBehaviour
    {
        public PlayerCharacterCamera OrbitCamera;
        public Transform CameraFollowPoint;
        public KineCharacterController Character;

        private const string MouseXInput = "Mouse X";
        private const string MouseYInput = "Mouse Y";
        private const string MouseScrollInput = "Mouse ScrollWheel";
        private const string HorizontalInput = "Horizontal";
        private const string VerticalInput = "Vertical";

        [Header("Movement")]
        [SerializeField] float speed = 6f;
        [SerializeField] float rotationSmoothTime = 0.2f;
        [SerializeField] float jumpHeight = 5f;
        [SerializeField] float dashSpeed = 18f;
        [SerializeField] float dashTime = 1f;

        private float targetAngle;
        private float velocityY;
        private float currentAngle;
        private float currentAngleVelocity;
        private Vector3 rotatedMovement = Vector3.zero;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.None;
            //Get Character GameObject with player tag at start
            Character = SaveUtils.GetPlayer().GetComponent<KineCharacterController>();

            // find CameraFollowPoint gameobject and set it's transform to CameraFollowPoint
            CameraFollowPoint = GameObject.Find("CameraFollowPoint").transform;

            // Tell camera to follow transform
            OrbitCamera.SetFollowTransform(CameraFollowPoint);

            // Ignore the character's collider(s) for camera obstruction checks
            OrbitCamera.IgnoredColliders.Clear();
            OrbitCamera.IgnoredColliders.AddRange(Character.GetComponentsInChildren<Collider>());
        }

        private void Update()
        {
            if (OrbitCamera == null || CameraFollowPoint == null)
            {
                Debug.Log("resetting controls");

                Character = SaveUtils.GetPlayer().GetComponent<KineCharacterController>();

                // find CameraFollowPoint gameobject and set it's transform to CameraFollowPoint
                CameraFollowPoint = GameObject.Find("CameraFollowPoint").transform;

                // Tell camera to follow transform
                OrbitCamera.SetFollowTransform(CameraFollowPoint);
            }
            if (Input.GetMouseButtonDown(1))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                Cursor.lockState = CursorLockMode.None;
            }


            HandleCharacterInput();
        }

        private void LateUpdate()
        {

            // Handle rotating the camera along with physics movers
            if (OrbitCamera.RotateWithPhysicsMover && Character.Motor.AttachedRigidbody != null)
            {
                OrbitCamera.PlanarDirection = Character.Motor.AttachedRigidbody.GetComponent<PhysicsMover>().RotationDeltaFromInterpolation * OrbitCamera.PlanarDirection;
                OrbitCamera.PlanarDirection = Vector3.ProjectOnPlane(OrbitCamera.PlanarDirection, Character.Motor.CharacterUp).normalized;
            }

            HandleCameraInput();
        }

        private void HandleCameraInput()
        {
            // Create the look input vector for the camera
            float mouseLookAxisUp = GetAxisCustom(MouseYInput);
            float mouseLookAxisRight = GetAxisCustom(MouseXInput);
            Vector3 lookInputVector = new Vector3(mouseLookAxisRight, mouseLookAxisUp, 0f);

            // Prevent moving the camera while the cursor isn't locked
            //if (Cursor.lockState != CursorLockMode.Locked)
            //{
            //    lookInputVector = Vector3.zero;
            //}

            // Input for zooming the camera (disabled in WebGL because it can cause problems)
            float scrollInput = -Input.GetAxis(MouseScrollInput);
#if UNITY_WEBGL
        scrollInput = 0f;
#endif

            // Apply inputs to the camera
            OrbitCamera.UpdateWithInput(Time.deltaTime, scrollInput, lookInputVector);

        }

        private void HandleCharacterInput()
        {
            PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

            // Build the CharacterInputs struct
            characterInputs.MoveAxisForward = Input.GetAxisRaw(VerticalInput);
            characterInputs.MoveAxisRight = Input.GetAxisRaw(HorizontalInput);
            characterInputs.CameraRotation = OrbitCamera.Transform.rotation;
            characterInputs.JumpDown = Input.GetKeyDown(KeyCode.Space);
            characterInputs.CrouchDown = Input.GetKeyDown(KeyCode.C);
            characterInputs.CrouchUp = Input.GetKeyUp(KeyCode.C);
            characterInputs.DashDown = Input.GetKeyDown(KeyCode.LeftShift);
            

            // Apply inputs to character
            Character.SetInputs(ref characterInputs);
        }


        private float GetAxisCustom(string axisName)
        {
            if (axisName == "Mouse X")
            {
                if (Input.GetMouseButton(1))
                {
                    return Input.GetAxis("Mouse X");
                }
                else
                {
                    return 0;
                }
            }
            else if (axisName == "Mouse Y")
            {
                if (Input.GetMouseButton(1))
                {
                    return Input.GetAxis("Mouse Y");
                }
                else
                {
                    return 0;
                }
            }
            return Input.GetAxis(axisName);
        }
    }
}