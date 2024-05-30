using Assets.Scripts.Creatures;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    internal class PlayerProjectile : StaticProjectile
    {
        public Vector2 direction;

        public override void Effect(Collider2D collision)
        {
            base.Effect(collision);
            Hitable hitable = collision.GetComponent<Hitable>();

            if (hitable != null)
            {
                Backlash();
            }
        }

        private void Backlash()
        {
            WeaponUser.GetComponent<PlayerController>().Backlash(ProjectileData.BacklashForce, direction);
        }

    }
}
