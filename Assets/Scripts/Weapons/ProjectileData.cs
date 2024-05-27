using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "ScriptableObject/Projectle Data")]
    internal class ProjectileData : ScriptableObject
    {
        public float damage;
        public float backlashForce;
    }
}
