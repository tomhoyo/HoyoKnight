using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Creatures;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    internal class Spike: Projectile
    {
        private float attackDamage = 1;
        private Vector2 knockback = Vector2.zero;

        public override void Effect(Collider2D collision)
        {
            Damageable damageable = collision.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.Hit(gameObject, attackDamage);
            }
        }
    }
}
