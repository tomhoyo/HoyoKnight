using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Creatures
{
    internal interface Hitable 
    {
        public void Hit(GameObject weapon, float damage);
    }
}
