using UnityEngine;
using KBCore.Refs;
using System;
using System.Collections;
using Unity.Cinemachine;
namespace Platformer
{
    public class CameraManager : ValidatedMonoBehaviour
    {

        [SerializeField, Anywhere] InputReader input;
        [SerializeField, Anywhere] CinemachineCamera freeLookVCam;

        [Header("Settings")]

        [SerializeField, Range(0.5f, 3f)] float speedMultiplier;

        bool isRMBPressed;
        bool isDeviceMouse;
        bool cameraMovementLock;


        void OnEnable()
        {
            input.Look += OnLook;
            input.EnableMouseControlCamara += OnEnableMouseControlCamera;
            //input.DisableMouseControlCamara += OnDisableMouseControlCamera;
        }

        private void OnEnableMouseControlCamera()
        {
            isRMBPressed = true;

            //Lock the cursor o the center of the screen and hide it
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            StartCoroutine(DisableMouseForFrame());
        }

        private void OnDisableMouseControlCamera()
        {
            isRMBPressed = false;

            //Lock the cursor o the center of the screen and hide it
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //freeLookVCam.
        }

        IEnumerator DisableMouseForFrame()
        {
            isDeviceMouse = true;
            yield return new WaitForEndOfFrame(); // Wait for the next frame
            cameraMovementLock = false;

        }

        private void OnLook(Vector2 arg0, bool arg1)
        {
            throw new NotImplementedException();
        }

        void OnDisable()
        {
            input.Look -= OnLook;
            input.EnableMouseControlCamara -= OnEnableMouseControlCamera;
            //input.DisableMouseControlCamara -= OnDisableMouseControlCamera;
        }

        
    }
}
