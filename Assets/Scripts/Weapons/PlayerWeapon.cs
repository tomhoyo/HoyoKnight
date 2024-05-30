using Assets.Scripts.InputActions;
using Assets.Scripts.StringConstant;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    internal class PlayerWeapon : Weapon
    {
        private new void Start()
        {
            base.Start();
            InputListener.EventUseWeapon.AddListener(OnUseWeapon);
        }

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
            if (!Direction.IsGrounded && InputListener.AxisY == -1)
            {
                Animator.SetTrigger(AnimationString.MAINACTIONDOWN);
            }
            else if (InputListener.AxisY == 1)
            {
                Animator.SetTrigger(AnimationString.MAINACTIONUP);
            }
            else if (SpriteRenderer.flipX)
            {
                Animator.SetTrigger(AnimationString.MAINACTIONLEFT);
            }
            else
            {
                Animator.SetTrigger(AnimationString.MAINACTIONRIGHT);
            }
        }
    }
}
