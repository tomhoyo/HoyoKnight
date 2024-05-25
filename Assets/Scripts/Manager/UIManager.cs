using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public void OnExitGame(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            ExitGame();
        }
    }

    public void ExitGame()
    {
        
        #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
             Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        #endif

        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_STANDALONE)
            Application.Quit();
        #elif (UNITY_WEBGL)
            SceneManager.LoadScene("QuitScene");
        #endif
        
    }
}
