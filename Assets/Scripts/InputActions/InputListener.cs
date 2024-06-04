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
        public UnityEvent EventInteract;


        private float _axisX, _axisY, _jumpInput;
        public float AxisX { get { return _axisX; } private set { _axisX = value; } }
        public  float AxisY { get { return _axisY; } private set { _axisY = value; } }
        public float JumpInput { get { return _jumpInput; } private set { _jumpInput = value; } }

        private static InputListener instance = null;
        public static InputListener Instance => instance;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(gameObject);
        }

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

            if (context.phase.Equals(InputActionPhase.Performed))
            {
                EventJump?.Invoke();
            }
        }

        public void OnUseWeapon(InputAction.CallbackContext context)
        {
            if (context.phase.Equals(InputActionPhase.Performed))
            {
                EventUseWeapon?.Invoke();
            }
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.phase.Equals(InputActionPhase.Performed))
            {
                EventDash?.Invoke();
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.phase.Equals(InputActionPhase.Performed))
            {
                EventInteract?.Invoke();
            }
        }
    }
}
