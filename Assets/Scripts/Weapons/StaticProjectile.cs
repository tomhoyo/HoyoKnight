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
    internal class StaticProjectile : Projectile
    {

        public Vector2 direction;

        public override void Effect(Collider2D collision)
        {
            Hitable hitable = collision.GetComponent<Hitable>();

            if (hitable != null)
            {
                hitable.Hit(gameObject, projectileData.damage);
                Backlash();
            }
        }

        private void Backlash()
        {
            backLash?.Invoke(projectileData.backlashForce, direction);
        }
    }
}
