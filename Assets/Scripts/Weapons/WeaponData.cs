using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObject/Weapon Data")]
    internal class WeaponData : ScriptableObject
    {
        [SerializeField]
        public float WeaponCooldown = 0.0f; //hv
    }
}
