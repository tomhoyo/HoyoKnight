using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Creatures;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    internal class Airblow : Projectile
    {
        private float damage = 1;
        private float backlashForce = 15f;

        public Vector2 direction;


        public override void Effect(Collider2D collision)
        {
            Hitable damageable = collision.GetComponent<Hitable>();

            if (damageable != null)
            {
                damageable.Hit(gameObject, damage);
                Backlash();
            }
        }

        private void Backlash()
        {
            backLash?.Invoke(backlashForce, direction);
        }
    }
}
