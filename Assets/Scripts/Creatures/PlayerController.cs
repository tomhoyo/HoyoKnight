

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Assets.Scripts.StringConstant;
using Assets.Scripts.InputActions;


namespace Assets.Scripts.Creatures
{
    [RequireComponent(typeof(TouchingDirections))]   
    public class PlayerController : CreatureController
    {
        private Vector2 _frameVelocity;

        //KnockBack
        private GameObject _lastWeaponBeenAttackBy;
        public GameObject LastWeaponBeenAttackBy { get { return _lastWeaponBeenAttackBy; } private set { _lastWeaponBeenAttackBy = value; } }

        //collision detection
        private TouchingDirections _direction;

        //InputListener
        [SerializeField]
        private InputListener _inputListener;

        //Double jump
        private bool _canDoubleJump = true;
        public bool CanDoubleJump { get { return _canDoubleJump; } private set { _canDoubleJump = value; } }

        //direction
        private float _lastDashTime = 0f;
        public float LastDashTime { get { return _lastDashTime; } private set { _lastDashTime = value; } }
        private float _dashTimer = 0f;
        public float DashTimer { get { return _dashTimer; } private set { _dashTimer = value; } }
        private const float _maxSpeed = 18;


        //dash
        private const float _airDashPower = 50;
        private const float _airDashTimer = 0.2f; 
        private const float _airDashCooldown = 0.6f;
        private bool _isDashing = false;
        public bool IsDashing { get { return _isDashing; } private set { _isDashing = value; } }
        private float _actionLastDashTime = 0f;
        public float ActionLastDashTime { get { return _actionLastDashTime; } private set { _actionLastDashTime = value; } }

        //wall jump
        private const float _wallDashPower = 18f;
        private const float _wallJumpForce = 50f;
        private const float _wallDashTimer = 0.1f;
        private float _lastWalledTime = -1;
        public float LastWalledTime { get { return _lastWalledTime; } private set { _lastWalledTime = value; } }
        private const float _wallCoyoteTime = 0.2f;

        //Backlash
        private const float _backlashDashTimer = 0.1f;
        private const float _pogoJumpForce = 80;

        //jump
        private bool _endedJumpEarly = false;
        public bool EndedJumpEarly { get { return _endedJumpEarly; } private set { _endedJumpEarly = value; } }
        private bool _jumpToConsume = false;
        public bool JumpToConsume { get { return _jumpToConsume; } private set { _jumpToConsume = value; } }
        private bool _isHighJumping = false;
        public bool IsHighJumping { get { return _isHighJumping; } private set { _isHighJumping = value; } }
        private const float _jumpForce = 50;
        private const float _jumpDeltaTime = 100;
        private const float _jumpEndedEarlyDeltaTime = 350;

        //jump buffering
        private const float _jumpBufferTime = 0.2f;
        private float _lastJumpTime = -1;
        public float LastJumpTime { get { return _lastJumpTime; } private set { _lastJumpTime = value; } }

        //coyotes system
        private const float _groundCoyoteTime = 0.1f;
        private float _lastGroundedTime = -1;
        public float LastGroundedTime { get { return _lastGroundedTime; } private set { _lastGroundedTime = value; } }

        //gravity
        private bool _enableGravity = true;
        public bool EnableGravity { get { return _enableGravity; } private set { _enableGravity = value; } }
        private const float _fallDeltaTime = 320;
        private const float _freeMaxFallSpeed = 30;
        private const float _wallRideMaxFallSpeed = 20;
        private const float _groundingForce = 0;
        private const float _ceilingBoundingForce = 1;

        private void Awake()
        {
            _direction = GetComponent<TouchingDirections>();
        }

        public void FixedUpdate()
        {
            if (CanMove)
            {
                HandleCollision();
                HandleJump();
                HandleGravity();
                HandleDirection();                
            }
            else
            {
                _frameVelocity = new Vector2();
            }
            ApplyMovement();
        }

        public void Update()
        {
            SetAnimatorParameters(_rb.velocity.x);
        }

        public void HandleCollision()
        {
            if (!_direction.IsGrounded) EndedJumpEarly = false;
            else LastGroundedTime = Time.time;

            if(_direction.IsLeftWalled || _direction.IsRightWalled) LastWalledTime = Time.time;

            if (_direction.IsCeiling) EndedJumpEarly = true;

            if (_direction.IsGrounded || _direction.IsLeftWalled || _direction.IsRightWalled) CanDoubleJump = true;
        }

        public void Dash()
        {
            IsDashing = true;
            LastDashTime = Time.time;
            ActionLastDashTime = Time.time;
            DashTimer = _airDashTimer;
            EnableGravity = false;
            _frameVelocity.y = 0;

            _animator.SetTrigger(AnimationString.ISDASHING);

            if (IsWallRiding()) Sense = GetWallRideSide();
            _frameVelocity.x = _airDashPower * Sense;
        }

        public void OnDash()
        {
            if (ActionLastDashTime < Time.time - _airDashCooldown) Dash();
        }

        private void HandleDirection()
        {
            if (IsDashing)
            {
                if (IsDashOver())
                {
                    IsDashing = false;
                    EnableGravity = true;
                }
            }
            else
            {
                _frameVelocity.x = _inputListener.AxisX * _maxSpeed;
            }
        }

        private bool IsDashOver()
        {
            return LastDashTime < Time.time - DashTimer;
        }

        public void OnJump()
        {
                if (!IsJumping()) LastJumpTime = Time.time;
                JumpToConsume = true;
        }

        private void HandleJump()
        {
            if (!EndedJumpEarly && !_direction.IsGrounded && !IsHeldingJumpInput() && _rb.velocity.y > 0) EndedJumpEarly = true;

            if (!JumpToConsume && !HasBufferedJump()) return;

            if (_direction.IsGrounded || CanUseGroundCoyote()) Jump(_jumpForce);

            if ((IsWallRiding() || CanUseWallCoyote()) && !IsDashing && !IsJumping() && !_direction.IsGrounded && JumpToConsume)  WallJump();

            else if (!_direction.IsGrounded && !IsWallRiding() && JumpToConsume && CanDoubleJump)
            {
                CanDoubleJump = false;
                Jump(_jumpForce);
            }

            JumpToConsume = false;
        }

        private void Jump(float force)
        {
            _frameVelocity.y = force;
            EndedJumpEarly = false;
            LastGroundedTime -= _groundCoyoteTime;
            IsHighJumping = true;
        }

        private void WallJump()
        {
            IsDashing = true;
            LastDashTime = Time.time;
            DashTimer = _wallDashTimer;
            LastWalledTime -= _wallCoyoteTime;

            _frameVelocity.x = _wallDashPower * GetWallRideSide();
            Jump(_wallJumpForce);
        }

        private bool CanUseGroundCoyote()
        {
            bool canUseCoyote = (Time.time - LastGroundedTime <= _groundCoyoteTime);
            return canUseCoyote;
        }

        private bool CanUseWallCoyote()
        {
            bool canUseCoyote = (Time.time - LastWalledTime <= _wallCoyoteTime);
            return canUseCoyote;
        }

        private int GetWallRideSide()
        {
            //Wall riding on right
            if(_direction.IsRightWalled) return -1;

            //Wall riding on left
            if (_direction.IsLeftWalled) return 1;

            //No riding
            return 0;
        }

        private bool IsWallRiding()
        {
            return Convert.ToBoolean(GetWallRideSide());
        }

        private bool HasBufferedJump()
        {
            return (Time.time - LastJumpTime <= _jumpBufferTime);
        }

        private bool IsHeldingJumpInput()
        {
            return (_inputListener.JumpInput == 1);
        }

        private bool IsJumping()
        {
            return (_frameVelocity.y > 0f && !EndedJumpEarly);
        }

        private void HandleGravity()
        {
            if (_direction.IsGrounded && _frameVelocity.y <= 0f)
            {
                _frameVelocity.y = _groundingForce;
            }
            else if (_direction.IsCeiling)
            {
                _frameVelocity.y = -_ceilingBoundingForce;
            }
            else if(IsJumping() && IsHighJumping) //grand saut
            {
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, 0, _jumpDeltaTime * Time.fixedDeltaTime);

            }
            else if(IsJumping() && EndedJumpEarly) //petit saut
            {
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, 0, _jumpEndedEarlyDeltaTime * Time.fixedDeltaTime);
            }
            else if(!IsDashing && EnableGravity) //fall 
            {
                float MaxFallSpeed = (IsWallRiding()) ? _wallRideMaxFallSpeed: _freeMaxFallSpeed;
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -MaxFallSpeed, _fallDeltaTime * Time.fixedDeltaTime);
            }
        }

        private void ApplyMovement() => _rb.velocity = _frameVelocity;

        public void Backlash(float backlashForce, Vector2 knockback)
        {
            if (knockback.x != 0) {
                _frameVelocity.x = (-knockback.x * backlashForce);
                IsDashing = true;
                LastDashTime = Time.time;
                DashTimer = _backlashDashTimer;
                EnableGravity = false;
                CanFlipSprite = false;
            }
            else if (knockback.y == -1)
            {
                Jump(_pogoJumpForce);
                IsHighJumping = false;
                CanDoubleJump = true;
            }

            ApplyMovement();
        }

        public void KnockBack()
        {
            if (LastWeaponBeenAttackBy != null)
            {
                int direction = (LastWeaponBeenAttackBy.transform.position.x < gameObject.transform.position.x) ? -1 : 1;
                _frameVelocity = new Vector2(50 * direction, 50);
                IsDashing = true;
                LastDashTime = Time.time;
                DashTimer = _backlashDashTimer;
                EnableGravity = false;
                CanFlipSprite = false;
                ApplyMovement();

                LastWeaponBeenAttackBy = null;
            }
        }

        public void RegisterLastWeaponBeenAttackBy(GameObject weapon)
        {
            LastWeaponBeenAttackBy = weapon;
        }

    }

}
