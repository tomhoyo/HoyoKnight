using Assets.Scripts.InputActions;
using Assets.Scripts.StringConstant;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.HUD
{
    internal class Initiator : MonoBehaviour
    {
        public UnityEvent EventInit;
        [SerializeField]
        public GameObject gameObject;

        private void Start()
        {
            GameObject gameObjectTag = GameObject.Find(gameObject.tag);

            if (gameObjectTag == null)
            {
                Debug.Log(gameObjectTag);
                EventInit?.Invoke();    
            }
        }
    }
}
