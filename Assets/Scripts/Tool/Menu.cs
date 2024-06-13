using UnityEngine;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    public void OnMenuChangeState(InputAction.CallbackContext context)
    {
        if (context.phase.Equals(InputActionPhase.Performed))
        {
            MenuChangeState();
        }
    }

    public void OnMenuChangeState()
    {
        MenuChangeState();
    }

    private void MenuChangeState()
    {
        if (gameObject.activeSelf)
        {
            HidePanel();
        }
        else
        {
            DisplayPanel();
        }
    }

    public void DisplayPanel()
    {
        gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }
    
}
