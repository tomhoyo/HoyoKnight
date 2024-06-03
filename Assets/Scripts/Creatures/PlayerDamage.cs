using UnityEngine;
using Assets.Scripts.StringConstant;
using UnityEngine.Events;

namespace Assets.Scripts.Creatures
{
    internal class PlayerDamage : Damageable
    {

        //Unity Event
        public UnityEvent<float> EventHealthModified;
        public UnityEvent<float> EventMaxHealthModified;
        public UnityEvent<GameObject> EventRegisterLastWeaponBeenAttackBy;

        public override float MaxHealth
        {
            get { return base.MaxHealth; }
            set
            {
                base.MaxHealth = value;
                EventMaxHealthModified?.Invoke(base.MaxHealth);
            }
        }

        public override float Health
        {
            get { return base.Health; }
            set
            {
                base.Health = value;
                EventHealthModified?.Invoke(base.Health);
            }
        }

        private new void Start()
        {
            //HealthBar
            GameObject HealthBar = GameObject.Find(GameObjectsName.HEALTHBAR);
            if (HealthBar != null)
            {
                EventHealthModified.AddListener(HealthBar.GetComponent<HealthBar>().UpdateHealthBar);
                EventMaxHealthModified.AddListener(HealthBar.GetComponent<HealthBar>().UpdateMaxHealthBar);
            }
            
            //KnockBack
            EventRegisterLastWeaponBeenAttackBy.AddListener(gameObject.GetComponent<PlayerController>().RegisterLastWeaponBeenAttackBy);


            MaxHealth = CreatureData.MaxHealth;
            IsAlive = CreatureData.IsAlive;
            invincibilityTimer = CreatureData.InvincibilityTimer;
            Health = MaxHealth;
        }

        public override void Hit(GameObject weapon, float damage)
        {
            base.Hit(weapon, damage);
            EventRegisterLastWeaponBeenAttackBy?.Invoke(weapon);

        }
    }
}
