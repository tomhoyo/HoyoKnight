
using System;
using Assets.Scripts.StringConstant;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Creatures
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]

    public class CreatureController : MonoBehaviour
    {
        public Rigidbody2D _rb;

        public Animator _animator;

        public SpriteRenderer _spriteRenderer;

        //direction
        public const float MaxSpeed = 18;
        private int _sense = 1;
        public int Sense
        {
            get { return _sense; }
            set 
            { 
                _sense = value;

                if (_sense == -1)
                {
                    _spriteRenderer.flipX = true;
                }
                else if (_sense == 1)
                {
                    _spriteRenderer.flipX = false;
                }
            }
        }

        public bool CanFlipSprite
        {
            get { return _animator.GetBool(AnimationString.canFlipSprite); }
            set {_animator.SetBool(AnimationString.canFlipSprite, value); }
        }

        public bool CanMove
        {
            get 
            {
                return _animator.GetBool(AnimationString.CANMOVE); 
            }
            set 
            { 
                _animator.SetBool(AnimationString.CANMOVE, value);
                CanFlipSprite = value;
            }
        }

        public bool IsAlive
        {
            get { return _animator.GetBool(AnimationString.ISALIVE); }
            set { _animator.SetBool(AnimationString.ISALIVE, value); }
        }

        public void Awake()
        {
          /*_rb = GetComponent<Rigidbody2D>();
          _animator = GetComponent<Animator>();
          _spriteRenderer = GetComponent<SpriteRenderer>();*/
        }

        public void AllowToMove()
        {
            CanMove = true;
        }


        /*
         *  Animations
         */
        public void SetAnimatorParameters(float horizontalMovement)
        {
            /*
             * Flip the sprite to right or left
             */
            if(CanFlipSprite)
            {
                if (horizontalMovement < -0.3)
                {
                    Sense = -1;
                }
                else if (horizontalMovement > 0.3)
                {
                    Sense = 1;
                }
            }
            

            /*
            * define "HorzontalSpeed" parameter
            */
            _animator.SetFloat(AnimationString.HORIZONTALSPEED, Mathf.Abs(horizontalMovement));

            /*
             * define "VerticalSpeed" parameter
             */
            _animator.SetFloat(AnimationString.VERTICALSPEED, _rb.velocity.y);
        }
    }
}
