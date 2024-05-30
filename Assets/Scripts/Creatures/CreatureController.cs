
using Assets.Scripts.StringConstant;
using UnityEngine;

namespace Assets.Scripts.Creatures
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
    public class CreatureController : MonoBehaviour
    {
        private Rigidbody2D _rb;
        public Rigidbody2D Rb { get { return _rb; } set { _rb = value;  } }

        private Animator _animator;
        public Animator Animator { get { return _animator; } set { _animator = value; } }

        private SpriteRenderer _spriteRenderer;
        public SpriteRenderer SpriteRenderer { get { return _spriteRenderer; } set { _spriteRenderer = value; } }

        //direction
        private int _sense = 1;
        public int Sense
        {
            get { return _sense; }
            set 
            { 
                _sense = value;

                if (_sense == -1)
                {
                    SpriteRenderer.flipX = true;
                }
                else if (_sense == 1)
                {
                    SpriteRenderer.flipX = false;
                }
            }
        }

        public void Awake()
        {
            Rb = gameObject.GetComponent<Rigidbody2D>();
            Animator = gameObject.GetComponent<Animator>();
            SpriteRenderer = gameObject.GetComponent<SpriteRenderer>(); ;
        }
        

        public bool CanFlipSprite
        {
            get { return Animator.GetBool(AnimationString.canFlipSprite); }
            set {Animator.SetBool(AnimationString.canFlipSprite, value); }
        }

        public bool CanMove
        {
            get 
            {
                return Animator.GetBool(AnimationString.CANMOVE); 
            }
            set 
            { 
                Animator.SetBool(AnimationString.CANMOVE, value);
                CanFlipSprite = value;
            }
        }

        public bool IsAlive
        {
            get { return Animator.GetBool(AnimationString.ISALIVE); }
            set { Animator.SetBool(AnimationString.ISALIVE, value); }
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
            Animator.SetFloat(AnimationString.HORIZONTALSPEED, Mathf.Abs(horizontalMovement));

            /*
             * define "VerticalSpeed" parameter
             */
            Animator.SetFloat(AnimationString.VERTICALSPEED, Rb.velocity.y);
        }
    }
}
