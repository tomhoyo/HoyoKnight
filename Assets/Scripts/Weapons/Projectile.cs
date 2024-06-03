using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [RequireComponent(typeof(Collider2D))]
    internal class Projectile : MonoBehaviour
    {
        [SerializeField]
        public ProjectileData _projectileData;
        public ProjectileData ProjectileData { get { return _projectileData; } private set { _projectileData = value; } }


        private GameObject _weapon;
        public GameObject Weapon { get { return _weapon; } private set { _weapon = value; } }

        private GameObject _weaponUser;
        public GameObject WeaponUser { get { return _weaponUser; } private set { _weaponUser = value; } }


        private void Start()
        {
            Weapon = gameObject.transform.parent.gameObject;
            WeaponUser = Weapon.transform.parent.gameObject;
        }

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
