using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Creatures
{
    [RequireComponent(typeof(Collider2D))]

    internal class Interactable : MonoBehaviour
    {
        public UnityEvent EventSelect;
        public UnityEvent EventUnSelect;
        public UnityEvent EventInteract;


        public void Select()
        {
            EventSelect?.Invoke();
        }

        public void UnSelect()
        {
            EventUnSelect?.Invoke();
        }

        public void Interact()
        {
            EventInteract?.Invoke();
        }
    }
}
