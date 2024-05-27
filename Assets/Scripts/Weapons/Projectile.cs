using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Weapons
{
    [RequireComponent(typeof(Collider2D))]
    internal class Projectile : MonoBehaviour
    {
        public UnityEvent<float, Vector2> backLash;

        public ProjectileData projectileData;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Effect(collision);
        }

        public virtual void Effect(Collider2D collision)
        {
            throw new NotImplementedException(gameObject.name + " Projectile as no projectile.effect method implemented");
        }
    }
}
