using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.StringConstant;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Creatures
{
    [RequireComponent(typeof(Animator))]
    internal class Damageable : MonoBehaviour, Hitable
    {
        [SerializeField]
        private CreatureData creatureData;
        public Animator animator;
        public UnityEvent<float> EventHealthModified;
        public UnityEvent<float> EventMaxHealthModified;
        public UnityEvent<GameObject> EventRegisterLastWeaponBeenAttackBy;

        private float _maxHealth;
        public float MaxHealth { get { return _maxHealth; } 
            set { 
                _maxHealth = value;
                EventMaxHealthModified?.Invoke(_maxHealth);
            }
        }

        private float _health;
        public float Health { get { return _health; } 
            set { 
                _health = value;
                if (Health <= 0) IsAlive = false;
                EventHealthModified?.Invoke(Health);

            }
        }

        private bool _isAlive;
        public bool IsAlive { get { return _isAlive; } 
            set { 
                _isAlive = value;
                animator.SetBool(AnimationString.ISALIVE, value);
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
        private float invincibilityTimer;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            MaxHealth = creatureData.MaxHealth;
            IsAlive = creatureData.IsAlive;
            invincibilityTimer = creatureData.InvincibilityTimer;

            Health = MaxHealth;
        }

        public void Hit(GameObject weapon, float damage)
        {

            if (IsAlive && !IsInvincible)
            {
                Health -= damage;
                lastTimeHit = Time.time;
                animator.SetTrigger(AnimationString.TAKEHIT);
            }

            EventRegisterLastWeaponBeenAttackBy?.Invoke(weapon);
        }      
    }
}
