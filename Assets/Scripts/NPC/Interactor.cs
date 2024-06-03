using Assets.Scripts.InputActions;
using Assets.Scripts.StringConstant;
using UnityEngine;

namespace Assets.Scripts.Creatures
{
    [RequireComponent(typeof(Collider2D))]

    internal class Interactor : MonoBehaviour
    {

        private Interactable _interactedObject;
        public Interactable InteractedObject {  get { return _interactedObject; } set { _interactedObject = value; } }

        private void Start()
        {
            GameObject InputManager = GameObject.Find(GameObjectsName.INPUTMANAGER);
            if (InputManager != null)
            {
                InputListener InputListerner = InputManager.GetComponent<InputListener>();
                InputListerner.EventInteract.AddListener(Interact);
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            Interactable interactable = collision.GetComponent<Interactable>();

            if (interactable != null)
            {
                InteractedObject = interactable;
                interactable.Select();
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            Interactable interactable = collision.GetComponent<Interactable>();

            if (interactable != null)
            {
                InteractedObject.UnSelect();
                InteractedObject = null;
            }    
        }

        public void Interact()
        {
            if (_interactedObject != null)
            {
                InteractedObject.Interact();
            }
        }
    }
}
