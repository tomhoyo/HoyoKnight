using Assets.Scripts.StringConstant;
using UnityEngine;

namespace Assets.Scripts.Tool
{
    internal class DontDestroyOnLoad : MonoBehaviour
    {


        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

    }
}
