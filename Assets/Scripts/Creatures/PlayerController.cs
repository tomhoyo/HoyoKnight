

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Assets.Scripts.StringConstant;
using System.Runtime.CompilerServices;
using Assets.Scripts.Weapons;


namespace Assets.Scripts.Creatures
{
    [RequireComponent(typeof(TouchingDirections))]   
    public class PlayerController : CreatureController
    {
        public Vector2 _frameVelocity;

        //collision detection
        private TouchingDirections _direction;  

        //input
        private float axisX, axisY, jumpInput;

        //direction
        private float lastDashTime = 0f;
        private float dashTimer = 0f;

        //dash
        private bool isDashing = false;
        private const float airDashPower = 50;
        private const float airDashTimer = 0.2f; //animation ref
        private const float airDashCooldown = 0.6f; //hv
        private float actionLastDashTime = 0f;

        //wall jump
        private const float wallDashPower = 18f;
        private const float wallJumpForce = 50f;
        private const float wallDashTimer = 0.1f;
        private float lastWalledTime = -1;
        private const float wallCoyoteTime = 0.2f;


        //Backlash
        private const float backlashDashTimer = 0.1f;
        private const float PogoJumpForce = 80;

        //KnockBack
        private GameObject LastWeaponBeenAttackBy;


        //MainAction
        private const float mainActionCooldown = 0.41f; //hv
        private float lastTimeMainAction = 0f;

        //jump
        private bool _endedJumpEarly = false;
        private bool _jumpToConsume = false;
        private bool _isHighJumping = false;
        private const float jumpForce = 50;
        private const float jumpDeltaTime = 100;
        private const float jumpEndedEarlyDeltaTime = 350;

        //Double jump
        private bool CanDoubleJump = true;

        //jump buffering
        private const float jumpBufferTime = 0.2f;
        private float lastJumpTime = -1;

        //coyotes system
        private const float groundCoyoteTime = 0.1f;
        private float lastGroundedTime = -1;

        //gravity
        private bool enableGravity = true;
        private const float FallDeltaTime = 320;
        private const float FreeMaxFallSpeed = 30;
        private const float WallRideMaxFallSpeed = 20;
        private const float GroundingForce = 0;
        private const float CeilingBoundingForce = 1;

        private new void Awake()
        {
            //base.Awake();
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
            if (!_direction.IsGrounded) _endedJumpEarly = false;
            else lastGroundedTime = Time.time;

            if(_direction.IsLeftWalled || _direction.IsRightWalled) lastWalledTime = Time.time;

            if (_direction.IsCeiling) _endedJumpEarly = true;

            if (_direction.IsGrounded || _direction.IsLeftWalled || _direction.IsRightWalled) CanDoubleJump = true;
        }

        public void Dash()
        {
            isDashing = true;
            lastDashTime = Time.time;
            actionLastDashTime = Time.time;
            dashTimer = airDashTimer;
            enableGravity = false;
            _frameVelocity.y = 0;

            _animator.SetTrigger(AnimationString.ISDASHING);

            if (IsWallRiding()) Sense = GetWallRideSide();
            _frameVelocity.x = airDashPower * Sense;
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (actionLastDashTime < Time.time - airDashCooldown) Dash();
        }

        public void OnHorizontalMove(InputAction.CallbackContext context)
        {
            Debug.Log("OnHorizontalMove");

            axisX = context.ReadValue<float>();
        }

        public void OnVerticalMove(InputAction.CallbackContext context)
        {
            axisY = context.ReadValue<float>();
        }

        private void HandleDirection()
        {
            if (IsDashing())
            {
                if (IsDashOver())
                { 
                    isDashing = false;
                    enableGravity = true;
                }
            }
            else
            {
                _frameVelocity.x = axisX * MaxSpeed;
            }
        }

        private bool IsDashOver()
        {
            return lastDashTime < Time.time - dashTimer;
        }

        private bool IsDashing()
        {
            return isDashing;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            jumpInput = context.ReadValue<float>();
            if (jumpInput == 1)
            {
                if (!IsJumping()) lastJumpTime = Time.time;
                _jumpToConsume = true;
            }
        }

        private void HandleJump()
        {
            if (!_endedJumpEarly && !_direction.IsGrounded && !IsHeldingJumpInput() && _rb.velocity.y > 0) _endedJumpEarly = true;

            if (!_jumpToConsume && !HasBufferedJump()) return;

            if (_direction.IsGrounded || CanUseGroundCoyote()) Jump(jumpForce);

            if ((IsWallRiding() || CanUseWallCoyote()) && !IsDashing() && !IsJumping() && !_direction.IsGrounded && _jumpToConsume)  WallJump();

            else if (!_direction.IsGrounded && !IsWallRiding() && _jumpToConsume && CanDoubleJump)
            {
                CanDoubleJump = false;
                Jump(jumpForce);
            }

            _jumpToConsume = false;
        }

        private void Jump(float force)
        {
            _frameVelocity.y = force;
            _endedJumpEarly = false;
            lastGroundedTime -= groundCoyoteTime;
            _isHighJumping = true;
        }

        private void WallJump()
        {
            isDashing = true;
            lastDashTime = Time.time;
            dashTimer = wallDashTimer;
            lastWalledTime -= wallCoyoteTime;

            _frameVelocity.x = wallDashPower * GetWallRideSide();
            Jump(wallJumpForce);
        }

        private bool CanUseGroundCoyote()
        {
            bool canUseCoyote = (Time.time - lastGroundedTime <= groundCoyoteTime);
            return canUseCoyote;
        }

        private bool CanUseWallCoyote()
        {
            bool canUseCoyote = (Time.time - lastWalledTime <= wallCoyoteTime);
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
            return (Time.time - lastJumpTime <= jumpBufferTime);
        }

        private bool IsHeldingJumpInput()
        {
            return (jumpInput == 1);
        }

        private bool IsJumping()
        {
            return (_frameVelocity.y > 0f && !_endedJumpEarly);
        }

        private void HandleGravity()
        {
            if (_direction.IsGrounded && _frameVelocity.y <= 0f)
            {
                _frameVelocity.y = GroundingForce;
            }
            else if (_direction.IsCeiling)
            {
                _frameVelocity.y = -CeilingBoundingForce;
            }
            else if(IsJumping() && _isHighJumping) //grand saut
            {
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, 0, jumpDeltaTime * Time.fixedDeltaTime);

            }
            else if(IsJumping() && _endedJumpEarly) //petit saut
            {
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, 0, jumpEndedEarlyDeltaTime * Time.fixedDeltaTime);
            }
            else if(!IsDashing() && enableGravity) //fall 
            {
                float MaxFallSpeed = (IsWallRiding()) ? WallRideMaxFallSpeed: FreeMaxFallSpeed;
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -MaxFallSpeed, FallDeltaTime * Time.fixedDeltaTime);
            }
        }

        private void ApplyMovement() => _rb.velocity = _frameVelocity;


        public void OnMainAction(InputAction.CallbackContext context)
        {
            if (lastTimeMainAction < Time.time - mainActionCooldown)
            {
                lastTimeMainAction = Time.time;
                PlayMainActionAnimation();
            }
        }

        public void PlayMainActionAnimation()
        {
            if(!_direction.IsGrounded && axisY == -1)
            {
                _animator.SetTrigger(AnimationString.MAINACTIONDOWN);
            }
            else if(axisY == 1) 
            {
                _animator.SetTrigger(AnimationString.MAINACTIONUP);
            }
            else if(Sense == -1) 
            {
                _animator.SetTrigger(AnimationString.MAINACTIONLEFT);
            }
            else
            {
                _animator.SetTrigger(AnimationString.MAINACTIONRIGHT);
            }
        }

        public void Backlash(float backlashForce, Vector2 knockback)
        {
            if (knockback.x != 0) {
                _frameVelocity.x = (-knockback.x * backlashForce);
                isDashing = true;
                lastDashTime = Time.time;
                dashTimer = backlashDashTimer;
                enableGravity = false;
                CanFlipSprite = false;
            }
            else if (knockback.y == -1)
            {
                Jump(PogoJumpForce);
                _isHighJumping = false;
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
                isDashing = true;
                lastDashTime = Time.time;
                dashTimer = backlashDashTimer;
                enableGravity = false;
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
