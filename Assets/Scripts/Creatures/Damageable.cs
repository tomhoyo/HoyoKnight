using Assets.Scripts.StringConstant;
using UnityEngine;

namespace Assets.Scripts.Creatures
{
    [RequireComponent(typeof(Animator))]
    internal class Damageable : MonoBehaviour, Hitable
    {
        [SerializeField]
        private CreatureData _creatureData;
        public CreatureData CreatureData { get { return _creatureData; } set { _creatureData = value; } }

        private Animator _animator;
        public Animator Animator { get { return _animator; } set { _animator = value; } }


        [SerializeField]
        private float _maxHealth;
        public virtual float MaxHealth { get { return _maxHealth; } 
            set { 
                _maxHealth = value;
            }
        }
        [SerializeField]
        private float _health;
        public virtual float Health { get { return _health; } 
            set { 
                _health = value;
                if (Health <= 0) IsAlive = false;
            }
        }

        private bool _isAlive;
        public bool IsAlive { get { return _isAlive; } 
            set { 
                _isAlive = value;
                Animator.SetBool(AnimationString.ISALIVE, value);
            } 
        }

        private bool IsInvincible { 
            get 
            {
                if (lastTimeHit < Time.time - invincibilityTimer)
                {
                    lastTimeHit = 0.0f;
                    return false;
                }
                return true;
            } 
        }

        private float lastTimeHit = 0.0f;
        public float invincibilityTimer;

        private void Awake()
        {
            Animator = gameObject.GetComponent<Animator>();
        }

        public void Start()
        {
            MaxHealth = CreatureData.MaxHealth;
            IsAlive = CreatureData.IsAlive;
            invincibilityTimer = CreatureData.InvincibilityTimer;
            Health = MaxHealth;

        }

        public virtual void Hit(GameObject weapon, float damage)
        {

            if (IsAlive && !IsInvincible)
            {
                Health -= damage;
                lastTimeHit = Time.time;
                Animator.SetTrigger(AnimationString.TAKEHIT);
            }

        }    
    }
}
