using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Assets.Scripts.InputActions
{
    internal class InputListener : MonoBehaviour
    {
        public UnityEvent EventJump;
        public UnityEvent EventDash;
        public UnityEvent EventUseWeapon;


        private float _axisX, _axisY, _jumpInput;
        public float AxisX { get { return _axisX; } private set { _axisX = value; } }
        public float AxisY { get { return _axisY; } private set { _axisY = value; } }
        public float JumpInput { get { return _jumpInput; } private set { _jumpInput = value; } }

        public void OnHorizontalMove(InputAction.CallbackContext context)
        {
            AxisX = context.ReadValue<float>();
        }

        public void OnVerticalMove(InputAction.CallbackContext context)
        {
            AxisY = context.ReadValue<float>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            JumpInput = context.ReadValue<float>();

            if (JumpInput == 1)
            {
                EventJump?.Invoke();
            }
        }

        public void OnUseWeapon(InputAction.CallbackContext context)
        {
            EventUseWeapon?.Invoke();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            EventDash?.Invoke();
        }
    }
}
