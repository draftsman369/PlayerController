using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Platformer
{

    [CreateAssetMenu(fileName = "InputReader", menuName = "Platformer/InputReader")]
    
    public class InputReader : ScriptableObject, InputSystem_Actions.IPlayerActions
    {

        public event UnityAction<Vector2> Move = delegate { };
        public event UnityAction<Vector2, bool> Look = delegate { };

        public event UnityAction EnableMouseControlCamara = delegate { };
        public event UnityAction DisableMouseControlCamera = delegate { };


        InputSystem_Actions inputActions;

        public Vector3 Direction => (Vector3)inputActions.Player.Move.ReadValue<Vector2>();

        private void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new InputSystem_Actions();
                inputActions.Player.SetCallbacks(this);
            }

            inputActions.Enable();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            //Noop
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            //Noop
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            //Noop
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            //Noop
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            Look.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
        }

        private bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse";


        public void OnMove(InputAction.CallbackContext context)
        {
            Move.Invoke(context.ReadValue<Vector2>());
        }

        public void OnNext(InputAction.CallbackContext context)
        {
            //Noop
        }

        public void OnPrevious(InputAction.CallbackContext context)
        {
            //Noop
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            //Noop
        }

        public void OnMouseControlCamera(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    EnableMouseControlCamara.Invoke();
                    break;
                case InputActionPhase.Canceled:
                    DisableMouseControlCamera.Invoke();
                    break;
            }
        }
    }
}
