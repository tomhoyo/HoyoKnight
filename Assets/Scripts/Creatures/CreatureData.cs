using UnityEngine;

namespace Assets.Scripts.Creatures
{
    [CreateAssetMenu(fileName = "CreatureData", menuName = "ScriptableObject/Creature Data")]
    internal class CreatureData : ScriptableObject
    {
        [SerializeField]
        private float _maxHealth;
        public float MaxHealth { get { return _maxHealth; } private set { _maxHealth = value; } }

        [SerializeField]
        private bool _isAlive;
        public bool IsAlive { get { return _isAlive; } private set { _isAlive = value; } }

        [SerializeField]
        private float _invincibilityTimer;
        public float InvincibilityTimer { get { return _invincibilityTimer; } private set { _invincibilityTimer = value; } }

    }
}
