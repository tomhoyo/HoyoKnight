using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "ScriptableObject/Projectle Data")]
    internal class ProjectileData : ScriptableObject
    {
        [SerializeField]
        private float _damage;
        public float Damage { get { return _damage; } set { _damage = value; } }

        [SerializeField]
        public float _backlashForce;
        public float BacklashForce { get { return _backlashForce; } set { _backlashForce = value; } }
    }
}
