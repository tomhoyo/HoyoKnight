using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObject/Weapon Data")]
    internal class WeaponData : ScriptableObject
    {
        [SerializeField]
        private float _weaponCooldown = 0.0f;
        public float WeaponCooldown { get { return _weaponCooldown;  } private set { _weaponCooldown = value;  } }
    }
}
