using Assets.Scripts.Creatures;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    internal class StaticProjectile : Projectile
    {
       

        public override void Effect(Collider2D collision)
        {
            Hitable hitable = collision.GetComponent<Hitable>();

            if (hitable != null)
            {
                hitable.Hit(gameObject, ProjectileData.Damage);
            }
        }

    }
}
