using KBCore.Refs;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

namespace Platformer
{
    public class PlayerController : ValidatedMonoBehaviour
    {

        [Header("References")]
        [SerializeField, Self] CharacterController controller;
        [SerializeField, Anywhere] Animator animator;
        [SerializeField, Anywhere] CinemachineCamera freeLookVcam;
        [SerializeField, Anywhere] InputReader input;

        [Header("Settings")]
        [SerializeField] float moveSpeed = 6f;
        [SerializeField] float rotationSpeed = 5f;
        [SerializeField] float smoothTime = 0.2f; //For smooting animation

        const float Zerof = 0.0f;

        Transform mainCam;

        float currentSpeed;
        float velocity;

        // Animator parameters
        static readonly int Speed = Animator.StringToHash("Speed");

        void Awake()
        {
            mainCam = Camera.main.transform;
            freeLookVcam.Follow = transform;
            freeLookVcam.LookAt = transform;
            freeLookVcam.OnTargetObjectWarped(transform, transform.position - Vector3.forward);
        }

        void Update()
        {
            HandleMovement();
            HandleAnimation();
        }

        void HandleMovement()
        {
            var movementDirection = new Vector3(input.Direction.x, 0, input.Direction.y).normalized;
            // Rotate the player towards the camera direction
            var adjustedDirection = Quaternion.AngleAxis(mainCam.eulerAngles.y, Vector3.up) * movementDirection;

            if (adjustedDirection.magnitude > Zerof)
            {

                HandleRotation(adjustedDirection);
                HandleCharacterController(adjustedDirection);
                SmoothSpeed(adjustedDirection.magnitude);
            }
            else
            {
                SmoothSpeed(Zerof);
            }
        }

        void HandleAnimation()
        {
            animator.SetFloat(Speed, currentSpeed);
        }

        void SmoothSpeed(float value)
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, value, ref velocity, smoothTime);
        }

        void HandleRotation(Vector3 adjustedDirection)
        {
            var targetRotation = Quaternion.LookRotation(adjustedDirection);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.LookAt(transform.position + adjustedDirection);
        }

        void HandleCharacterController(Vector3 adjustedDirection)
        {
            //Move the player
            var adjustedMovement = adjustedDirection * moveSpeed * Time.deltaTime;
            controller.Move(adjustedMovement);
        }
    }
}
