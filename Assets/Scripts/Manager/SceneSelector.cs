using System;
using Assets.Scripts.StringConstant;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    public UnityEvent EventLoadMainMenu;

    public void StartGame()
    {
        PlayScene(ScenesName.TESTGAMEPLAY);
    }

    public void MainMenu()
    {
        EventLoadMainMenu?.Invoke();
        PlayScene(ScenesName.MAINMENU);
    }

    public void LoadLobby()
    {
        SceneManager.LoadScene(ScenesName.LOBBY);

    }

    public void PlayScene(String scene)
    {
        SceneManager.LoadScene(scene);

    }
}
