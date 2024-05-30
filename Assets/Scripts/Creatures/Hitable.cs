using UnityEngine;

namespace Assets.Scripts.Creatures
{
    internal interface Hitable 
    {
        public void Hit(GameObject weapon, float damage);
    }
}
