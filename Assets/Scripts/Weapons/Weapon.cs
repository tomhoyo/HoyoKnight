using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.StringConstant;
using UnityEngine.InputSystem;
using UnityEngine;
using Assets.Scripts.Creatures;
using Assets.Scripts.InputActions;

namespace Assets.Scripts.Weapons
{
    internal class Weapon : MonoBehaviour
    {
        [SerializeField] private TouchingDirections _direction;
        [SerializeField] private Animator _animator;
        [SerializeField] private InputListener _inputListener;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] public WeaponData WeaponData;

        private float _lastTimeMainAction = 0f;
        public float LastTimeMainAction { get { return _lastTimeMainAction; } private set { _lastTimeMainAction = value; } }

        public void OnUseWeapon()
        {
            if (LastTimeMainAction < Time.time - WeaponData.WeaponCooldown)
            {
                LastTimeMainAction = Time.time;
                PlayWeaponAnimation();
            }
        }

        public void PlayWeaponAnimation()
        {
            if (!_direction.IsGrounded && _inputListener.AxisY == -1)
            {
                _animator.SetTrigger(AnimationString.MAINACTIONDOWN);
            }
            else if (_inputListener.AxisY == 1)
            {
                _animator.SetTrigger(AnimationString.MAINACTIONUP);
            }
            else if (_spriteRenderer.flipX)
            {
                _animator.SetTrigger(AnimationString.MAINACTIONLEFT);
            }
            else
            {
                _animator.SetTrigger(AnimationString.MAINACTIONRIGHT);
            }
        }
    }
}
