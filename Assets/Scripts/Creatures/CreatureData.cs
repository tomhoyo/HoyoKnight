using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Creatures
{
    [CreateAssetMenu(fileName = "CreatureData", menuName = "ScriptableObject/Creature Data")]
    internal class CreatureData : ScriptableObject
    {
        public float MaxHealth;
        public bool IsAlive;
        public float InvincibilityTimer;
    }
}
