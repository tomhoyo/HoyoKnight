using System;
using Assets.Scripts.Network;
using Assets.Scripts.StringConstant;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    private static SceneSelector instance = null;
    public static SceneSelector Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayScene(ScenesName.MAINMENU);
    }

    public void StartGame()
    {
        PlayScene(ScenesName.TESTGAMEPLAY);
    }

    public void MainMenu()
    {
        PlayScene(ScenesName.MAINMENU);
    }

    public void LoadLobby()
    {
        PlayScene(ScenesName.LOBBY);
    }

    public void PlayScene(String scene)
    {
        SceneManager.LoadScene(scene);
    }
}
